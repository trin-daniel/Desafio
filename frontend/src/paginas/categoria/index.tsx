import { FiGrid, FiInbox } from "react-icons/fi";
import Button from "../../componentes/button/index.tsx";
import { FormWrapper } from "../../componentes/form/styles.ts";
import Input from "../../componentes/input/index.tsx";
import type { Option } from "../../componentes/select";
import Select from "../../componentes/select";
import React from "react";
import { Table, type Column } from "../../componentes/table/index.tsx";
import { PageTitle } from "../../componentes/page-title/index.tsx";
import { toast } from "react-toastify";

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

interface Categoria {
  id: string;
  descricao: string;
  finalidade: string;
}

export default function PaginaCategoria() {
  const api = "http://localhost:5051/api/v1";

  const [descricao, setDescricao] = React.useState("");
  const [finalidade, setFinalidade] = React.useState("");
  const [erros, setErros] = React.useState<
    Record<"descricao" | "finalidade", string[]>
  >({
    descricao: [],
    finalidade: [],
  });
  const [categorias, setCategorias] = React.useState<Categoria[]>([]);
  const [carregando, setCarregando] = React.useState(false);

  const validarFormulario = () => {
    const erros: Record<"descricao" | "finalidade", string[]> = {
      descricao: [],
      finalidade: [],
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
    if (!finalidade) {
      erros.finalidade.push("Selecione uma finalidade.");
    }
    return erros;
  };

  const registrarCategoria = async (event: React.FormEvent) => {
    event.preventDefault();

    const erros = validarFormulario();
    if (Object.values(erros).some((arr) => arr.length > 0)) {
      setErros(erros);
      return;
    }

    const data = { descricao, finalidade };

    try {
      const response = await fetch(`${api}/categorias`, {
        method: "post",
        headers: { "content-type": "application/json" },
        body: JSON.stringify(data),
      });

      if (response.status === 201) {
        toast.success("A categoria foi adicionada com sucesso.");
        setDescricao("");
        setFinalidade("");
        setErros({ descricao: [], finalidade: [] });
        obterCategorias();
        return;
      }

      if (response.status === 409) {
        const parsedError = (await response.json()) as ProblemDetails;
        toast.error(parsedError.detail);
        return;
      }
    } catch (error) {
      toast.error("Erro ao registrar categoria.");
      console.error("Erro ao registrar categoria:", error);
    }
  };

  const obterCategorias = React.useCallback(async () => {
    setCarregando(true);
    try {
      const response = await fetch(`${api}/categorias`);
      if (response.status === 204) {
        setCategorias([]);
        return;
      }
      const data = (await response.json()) as Categoria[];
      setCategorias(data);
    } catch (err) {
      console.error("Erro ao recuperar categorias.", err);
    } finally {
      setCarregando(false);
    }
  }, [api]);

  React.useEffect(() => {
    obterCategorias();
  }, [obterCategorias]);

  const columns: Column<Categoria>[] = [
    { header: "Descrição", accessor: "descricao" },
    { header: "Finalidade", accessor: "finalidade" },
  ];

  return (
    <>
      <PageTitle>Categoria</PageTitle>
      <FormWrapper onSubmit={registrarCategoria}>
        <Input
          id="descricao"
          type="text"
          label="Descrição"
          value={descricao}
          icon={FiInbox}
          errors={erros.descricao}
          onChange={(event) => setDescricao(event.target.value)}
        />
        <Select
          id="finalidade"
          name="finalidade"
          label="Finalidade"
          errors={erros.finalidade}
          options={options}
          icon={FiGrid}
          value={finalidade}
          onChange={(event) => setFinalidade(event.target.value)}
        />
        <Button type="submit">Adicionar</Button>
      </FormWrapper>
      <Table<Categoria>
        columns={columns}
        data={categorias}
        loading={carregando}
        emptyMessage="Nenhuma categoria foi encontrada."
      />
    </>
  );
}
