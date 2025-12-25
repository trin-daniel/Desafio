import * as ReactRouter from "react-router";
import "./App.css";
import PaginaCategoria from "./paginas/categoria";
import PaginaPessoa from "./paginas/pessoa";
import PaginaTransacao from "./paginas/transacao";
import PaginaRelatorio from "./paginas/relatorio/index.tsx";
import MainLayout from "./layout/main-layout/index.tsx";
function App() {
  const router = ReactRouter.createBrowserRouter([
    {
      element: <MainLayout />,
      children: [
        {
          path: "/",
          element: <PaginaPessoa />,
        },
        {
          path: "/categorias",
          element: <PaginaCategoria />,
        },
        {
          path: "/pessoas",
          element: <PaginaPessoa />,
        },
        {
          path: "/transacoes",
          element: <PaginaTransacao />,
        },
        {
          path: "/relatorios",
          element: <PaginaRelatorio />,
        },
      ],
    },
  ]);
  return (
    <>
      <ReactRouter.RouterProvider router={router} />
    </>
  );
}

export default App;
