import {
  NavbarWrapper,
  NavbarLink,
  Logo,
  LinksContainer,
  Header,
} from "./styles.ts";
import * as FeatherIcons from "react-icons/fi";

export default function Navbar() {
  return (
    <Header>
      <NavbarWrapper>
        <Logo>Defi</Logo>
        <LinksContainer>
          <NavbarLink to="/pessoas">
            <FeatherIcons.FiUser />
            Pessoa
          </NavbarLink>
          <NavbarLink to="/categorias">
            <FeatherIcons.FiGrid />
            Categoria
          </NavbarLink>
          <NavbarLink to="/transacoes">
            <FeatherIcons.FiDollarSign />
            Transação
          </NavbarLink>
          <NavbarLink to="/relatorios">
            <FeatherIcons.FiArchive />
            Relatório
          </NavbarLink>
        </LinksContainer>
      </NavbarWrapper>
    </Header>
  );
}
