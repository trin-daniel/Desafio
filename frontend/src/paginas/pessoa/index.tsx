import React, { type FormEvent } from "react";
import Button from "../../componentes/button/index.tsx";
import { FiUser, FiTrash, FiHash } from "react-icons/fi";
import InputText from "../../componentes/input/index.tsx";
import { Table, type Column } from "../../componentes/table/index.tsx";
import { FormWrapper } from "../../componentes/form/styles.ts";
import { PageTitle } from "../../componentes/page-title/index.tsx";
import { toast } from "react-toastify";

interface Pessoa {
  id: string;
  nome: string;
  idade: number;
}

interface ProblemDetails {
  type: string;
  title: string;
  status: number;
  detail: string;
  instance: string;
}

export default function PaginaPessoa() {
  const api = "http://localhost:5051/api/v1";
  const [nome, setNome] = React.useState("");
  const [idade, setIdade] = React.useState("");
  const [pessoas, setPessoas] = React.useState<Pessoa[]>([]);
  const [erro, setErro] = React.useState<Record<"nome" | "idade", string[]>>({
    nome: [],
    idade: [],
  });
  const [carregando, setCarregando] = React.useState(false);

  React.useEffect(() => {
    obterPessoas();
  }, []);

  async function obterPessoas() {
    setCarregando(true);
    try {
      const response = await fetch(`${api}/pessoas`);
      if (response.status === 204) {
        setPessoas([]);
        return;
      }
      setPessoas(await response.json());
    } catch (error) {
      toast.error("Erro ao carregar pessoas.");
      console.error(error);
    } finally {
      setCarregando(false);
    }
  }

  function validarFormulario() {
    const erros: Record<"nome" | "idade", string[]> = { nome: [], idade: [] };
    if (!nome.trim()) {
      erros.nome.push("O nome é obrigatório.");
    }
    if (nome.trim().length < 4) {
      erros.nome.push("O nome deve conter pelo menos 4 caracteres.");
    }
    if (nome.trim().length > 64) {
      erros.nome.push("O nome deve ter no máximo 64 caracteres.");
    }
    const idadeNum = Number(idade);
    if (!idade.trim() || isNaN(idadeNum)) {
      erros.idade.push("A idade é obrigatória e deve ser um número.");
    } else if (idadeNum <= 0) {
      erros.idade.push("A idade deve ser maior que zero.");
    }
    return erros;
  }

  async function registrarPessoa(event: FormEvent) {
    event.preventDefault();
    const erros = validarFormulario();
    setErro(erros);
    if (Object.values(erros).some((arr) => arr.length > 0)) return;

    const dados = { nome, idade: Number(idade) };
    try {
      const response = await fetch(`${api}/pessoas`, {
        method: "post",
        headers: { "content-type": "application/json" },
        body: JSON.stringify(dados),
      });
      if (response.status === 201) {
        toast.success("Pessoa adicionada com sucesso.");
        setNome("");
        setIdade("");
        setErro({ nome: [], idade: [] });
        obterPessoas();
      } else if (response.status === 409) {
        const parsedError = (await response.json()) as ProblemDetails;
        toast.error(parsedError.detail);
      } else {
        toast.error("Erro ao adicionar pessoa.");
      }
    } catch (error) {
      toast.error("Erro de comunicação com o servidor.");
      console.error(error);
    }
  }

  async function removerPorId(pessoa: Pessoa) {
    const anterior = pessoas;
    setPessoas((prev) => prev.filter((p) => p.id !== pessoa.id));
    try {
      const response = await fetch(`${api}/pessoas/${pessoa.id}`, {
        method: "delete",
      });
      if (response.status === 204) {
        toast.success("Pessoa removida com sucesso.");
      } else {
        setPessoas(anterior);
        toast.error("Não foi possível remover a pessoa.");
      }
    } catch (err) {
      console.error(err);
      setPessoas(anterior);
      toast.error("Erro de comunicação ao remover pessoa.");
    }
  }

  const columns: Column<Pessoa>[] = [
    { header: "Nome", accessor: "nome" },
    { header: "Idade", accessor: "idade" },
  ];

  return (
    <>
      <PageTitle>Pessoa</PageTitle>
      <FormWrapper onSubmit={registrarPessoa}>
        <InputText
          id="nome"
          type="text"
          label="Nome"
          icon={FiUser}
          value={nome}
          errors={erro.nome}
          onChange={(e) => setNome(e.target.value)}
        />
        <InputText
          id="idade"
          type="number"
          label="Idade"
          icon={FiHash}
          value={idade}
          errors={erro.idade}
          onChange={(e) => setIdade(e.target.value)}
        />
        <Button type="submit">Adicionar</Button>
      </FormWrapper>
      <Table<Pessoa>
        columns={columns}
        data={pessoas}
        icon={FiTrash}
        emptyMessage="Nenhuma pessoa foi encontrada."
        loading={carregando}
        actions={[{ onClick: removerPorId }]}
      />
    </>
  );
}
