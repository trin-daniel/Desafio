import React from "react";
import { Table, type Column } from "../../componentes/table";
import { Card } from "../../componentes/card";
import formatCurrency from "../../utils/format-currency";
import { CardWrapper } from "./styles";
import { PageTitle } from "../../componentes/page-title";

interface TotaisPorPessoa {
  pessoaId: string;
  pessoaNome: string;
  totalReceitas: number;
  totalDespesas: number;
  saldo: number;
}

interface RelatorioResponse {
  totaisPorPessoa: TotaisPorPessoa[];
  totalGeralReceitas: number;
  totalGeralDespesas: number;
  saldoLiquido: number;
}

export default function PaginaRelatorio() {
  const api = "http://localhost:5051/api/v1";
  const [relatorio, setRelatorio] = React.useState<RelatorioResponse>();
  const [carregando, setCarregando] = React.useState(false);

  const obterRelatorio = async () => {
    try {
      setCarregando(true);
      const response = await fetch(`${api}/relatorios`);
      const dados = await response.json();
      setRelatorio(dados);
    } catch (erro) {
      console.error("Erro ao obter relatório:", erro);
    } finally {
      setCarregando(false);
    }
  };

  React.useEffect(() => {
    obterRelatorio();
  }, []);

  const columns: Column<TotaisPorPessoa>[] = [
    { header: "Pessoa", accessor: "pessoaNome" },
    {
      header: "Receitas",
      accessor: "totalReceitas",
      cell: (row) => formatCurrency(row.totalReceitas),
    },
    {
      header: "Despesas",
      accessor: "totalDespesas",
      cell: (row) => formatCurrency(row.totalDespesas),
    },
    {
      header: "Saldo",
      accessor: "saldo",
      cell: (row) => formatCurrency(row.saldo),
    },
  ];

  const data = relatorio?.totaisPorPessoa ?? [];

  return (
    <>
      <PageTitle>Relatório</PageTitle>
      <CardWrapper>
        <Card
          title="Receita"
          value={formatCurrency(relatorio?.totalGeralReceitas ?? 0)}
        />
        <Card
          title="Despesa"
          value={formatCurrency(relatorio?.totalGeralDespesas ?? 0)}
        />
        <Card
          title="Saldo Líquido"
          value={formatCurrency(relatorio?.saldoLiquido ?? 0)}
        />
      </CardWrapper>

      <Table<TotaisPorPessoa>
        columns={columns}
        data={data}
        loading={carregando}
        emptyMessage="Nenhuma transação foi encontrada."
      />
    </>
  );
}
