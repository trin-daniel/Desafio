import React, { type FormEvent } from "react";
import Input from "../../componentes/input/index.tsx";
import Select, { type Option } from "../../componentes/select";
import Button from "../../componentes/button/index.tsx";
import { FormWrapper } from "../../componentes/form/styles.ts";
import { PageTitle } from "../../componentes/page-title/index.tsx";
import { Table, type Column } from "../../componentes/table/index.tsx";
import { FiDollarSign, FiInbox } from "react-icons/fi";
import formatCurrency from "../../utils/format-currency.ts";
import { toast } from "react-toastify";

interface Categoria {
  id: string;
  descricao: string;
  finalidade: string;
}

interface Pessoa {
  id: string;
  nome: string;
  idade: number;
}

interface Transacao {
  id: string;
  descricao: string;
  valor: number;
  tipo: "Receita" | "Despesa" | "Ambos";
  categoriaId: string;
  categoriaNome: string;
  pessoaId: string;
  pessoaNome: string;
}

interface ProblemDetails {
  type: string;
  title: string;
  status: number;
  detail: string;
  instance: string;
}

const options: Option[] = [
  { value: "Receita", label: "Receita" },
  { value: "Despesa", label: "Despesa" },
  { value: "Ambos", label: "Ambos" },
];

export default function PaginaTransacao() {
  const api = "http://localhost:5051/api/v1";
  const [descricao, setDescricao] = React.useState("");
  const [valor, setValor] = React.useState("");
  const [finalidade, setFinalidade] = React.useState("");
  const [erros, setErros] = React.useState<
    Record<
      "descricao" | "valor" | "finalidade" | "categoriaId" | "pessoaId",
      string[]
    >
  >({
    descricao: [],
    valor: [],
    finalidade: [],
    categoriaId: [],
    pessoaId: [],
  });

  const [pessoaId, setPessoaId] = React.useState("");
  const [categoriaId, setCategoriaId] = React.useState("");
  const [carregando, setCarregando] = React.useState({
    categorias: false,
    pessoas: false,
    transacoes: false,
  });
  const [pessoas, setPessoas] = React.useState<Pessoa[]>([]);
  const [categorias, setCategorias] = React.useState<Categoria[]>([]);
  const [transacoes, setTransacoes] = React.useState<Transacao[]>([]);

  const obterCategorias = React.useCallback(async () => {
    setCarregando((prev) => ({ ...prev, categorias: true }));
    try {
      const response = await fetch(`${api}/categorias`);
      if (response.status === 204) {
        setCategorias([]);
        return;
      }
      const data = (await response.json()) as Categoria[];
      setCategorias(data);
    } catch (err) {
      toast.error("Erro ao recuperar categorias.");
      console.error(err);
    } finally {
      setCarregando((prev) => ({ ...prev, categorias: false }));
    }
  }, [api]);

  const obterPessoas = React.useCallback(async () => {
    setCarregando((prev) => ({ ...prev, pessoas: true }));
    try {
      const response = await fetch(`${api}/pessoas`);
      if (response.status === 204) {
        setPessoas([]);
        return;
      }
      const data = (await response.json()) as Pessoa[];
      setPessoas(data);
    } catch (err) {
      toast.error("Erro ao recuperar pessoas.");
      console.error(err);
    } finally {
      setCarregando((prev) => ({ ...prev, pessoas: false }));
    }
  }, [api]);

  const obterTransacoes = React.useCallback(async () => {
    setCarregando((prev) => ({ ...prev, transacoes: true }));
    try {
      const response = await fetch(`${api}/transacoes`);
      if (response.status === 204) {
        setTransacoes([]);
        return;
      }
      const data = (await response.json()) as Transacao[];
      const enriched = data.map((t) => {
        const categoria = categorias.find((c) => c.id === t.categoriaId);
        const pessoa = pessoas.find((p) => p.id === t.pessoaId);
        return {
          ...t,
          categoriaNome: categoria
            ? categoria.descricao
            : "Categoria desconhecida",
          pessoaNome: pessoa ? pessoa.nome : "Pessoa desconhecida",
        };
      });
      setTransacoes(enriched);
    } catch (err) {
      toast.error("Erro ao carregar transações.");
      console.error(err);
    } finally {
      setCarregando((prev) => ({ ...prev, transacoes: false }));
    }
  }, [api, categorias, pessoas]);

  const validarFormulario = () => {
    const erros: Record<
      "descricao" | "valor" | "finalidade" | "categoriaId" | "pessoaId",
      string[]
    > = {
      descricao: [],
      valor: [],
      finalidade: [],
      categoriaId: [],
      pessoaId: [],
    };
    if (!descricao.trim()) {
      erros.descricao.push("A descrição é obrigatória.");
    }
    if (descricao.trim().length < 4) {
      erros.descricao.push("A descrição deve conter pelo menos 4 caracteres.");
    }
    if (descricao.trim().length > 64) {
      erros.descricao.push("A descrição deve ter no máximo 64 caracteres.");
    }
    const valorNumerico = parseFloat(valor);
    if (!valor.trim() || isNaN(valorNumerico) || valorNumerico <= 0) {
      erros.valor.push("O valor deve ser um número maior que zero.");
    }
    if (valorNumerico > 999999) {
      erros.valor.push("O valor deve ser menor que 999.999.");
    }
    if (!finalidade) erros.finalidade.push("Selecione uma finalidade.");
    if (!categoriaId) erros.categoriaId.push("Selecione uma categoria.");
    if (!pessoaId) erros.pessoaId.push("Selecione uma pessoa.");

    const categoriaSelecionada = categorias.find((c) => c.id === categoriaId);
    if (
      categoriaSelecionada &&
      categoriaSelecionada.finalidade !== finalidade &&
      finalidade !== "Ambos"
    ) {
      erros.finalidade.push(
        "A finalidade da transação não corresponde à finalidade da categoria selecionada."
      );
    }

    return erros;
  };

  const cadastrarTransacao = async (event: FormEvent) => {
    event.preventDefault();
    const erros = validarFormulario();
    if (Object.values(erros).some((arr) => arr.length > 0)) {
      setErros(erros);
      return;
    }

    const transacao = {
      descricao,
      valor: Number(valor),
      tipo: finalidade,
      categoriaId,
      pessoaId,
    };
    try {
      const response = await fetch(`${api}/transacoes`, {
        method: "post",
        headers: { "content-type": "application/json" },
        body: JSON.stringify(transacao),
      });
      if (response.status === 201 || response.status === 200) {
        const output = await response.json();
        toast.success("Transação adicionada com sucesso.");
        const enriched = {
          ...output,
          categoriaNome:
            categorias.find((c) => c.id === output.categoriaId)?.descricao ||
            "Categoria desconhecida",
          pessoaNome:
            pessoas.find((p) => p.id === output.pessoaId)?.nome ||
            "Pessoa desconhecida",
        };
        setTransacoes((prev) => [...prev, enriched]);
        setDescricao("");
        setValor("");
        setFinalidade("");
        setCategoriaId("");
        setPessoaId("");
        setErros({
          descricao: [],
          valor: [],
          finalidade: [],
          categoriaId: [],
          pessoaId: [],
        });
      } else if (response.status === 422) {
        const parsedError = (await response.json()) as ProblemDetails;
        toast.error(parsedError.detail);
      } else {
        toast.error("Erro ao adicionar transação.");
      }
    } catch (error) {
      toast.error("Erro de comunicação com o servidor.");
      console.error(error);
    }
  };

  React.useEffect(() => {
    obterCategorias();
    obterPessoas();
  }, [obterCategorias, obterPessoas]);

  React.useEffect(() => {
    if (categorias.length && pessoas.length) {
      obterTransacoes();
    }
  }, [obterTransacoes, categorias, pessoas]);

  const categoriasOptions: Option[] = categorias.map((c) => ({
    value: c.id,
    label: c.descricao,
  }));
  const pessoasOptions: Option[] = pessoas.map((p) => ({
    value: p.id,
    label: p.nome,
  }));

  const columns: Column<Transacao>[] = [
    { header: "Descrição", accessor: "descricao" },
    { header: "Pessoa", accessor: "pessoaNome" },
    { header: "Categoria", accessor: "categoriaNome" },
    { header: "Tipo", accessor: "tipo" },
    {
      header: "Valor",
      accessor: "valor",
      cell: (row) => formatCurrency(row.valor),
    },
  ];

  return (
    <>
      <PageTitle>Transação</PageTitle>
      <FormWrapper onSubmit={cadastrarTransacao}>
        <Input
          id="descricao"
          label="Descrição"
          type="text"
          icon={FiInbox}
          errors={erros.descricao}
          value={descricao}
          onChange={(e) => setDescricao(e.target.value)}
        />
        <Input
          id="valor"
          label="Valor"
          type="number"
          errors={erros.valor}
          value={valor}
          icon={FiDollarSign}
          onChange={(e) => setValor(e.target.value)}
        />
        <Select
          label="Finalidade"
          id="finalidade"
          options={options}
          value={finalidade}
          errors={erros.finalidade}
          onChange={(e) => setFinalidade(e.target.value)}
        />
        <Select
          label="Categoria"
          id="categoria"
          options={categoriasOptions}
          errors={erros.categoriaId}
          value={categoriaId}
          onChange={(e) => setCategoriaId(e.target.value)}
        />
        <Select
          label="Pessoa"
          id="pessoas"
          options={pessoasOptions}
          errors={erros.pessoaId}
          value={pessoaId}
          onChange={(e) => setPessoaId(e.target.value)}
        />
        <Button type="submit">Adicionar</Button>
      </FormWrapper>

      <Table<Transacao>
        columns={columns}
        data={transacoes}
        loading={carregando.transacoes}
        emptyMessage="Nenhuma transação foi encontrada."
      />
    </>
  );
}
