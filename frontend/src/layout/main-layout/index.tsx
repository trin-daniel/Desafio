import * as ReactRouter from "react-router";
import Navbar from "../../componentes/navbar/index.tsx";
import { Container } from "./index.ts";
import { ToastContainer } from "react-toastify";

export default function MainLayout() {
  return (
    <>
      <Navbar />
      <Container>
        <ToastContainer />
        <ReactRouter.Outlet />
      </Container>
    </>
  );
}
