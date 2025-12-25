import { NavLink } from "react-router";
import styled from "styled-components";
export const Header = styled.header`
  width: 100%;
  height: 72px;
  background-color: #00a991;
`;

export const NavbarWrapper = styled.nav`
  max-width: 1040px;
  width: 100%;
  height: 72px;
  margin: 0 auto;
  /* padding: 0 1rem; */
  display: flex;
  align-items: center;
  justify-content: space-between;
`;

export const Logo = styled.div`
  font-size: 1.5rem;
  font-weight: 600;
  font-family: "Inter", sans-serif;
  text-transform: uppercase;
  color: #003b33;
`;

export const LinksContainer = styled.div`
  display: flex;
  gap: 2rem;
  justify-content: center;
  flex: 1;
`;

export const NavbarLink = styled(NavLink)`
  color: #003b33;
  font-size: 1rem;
  font-family: "Inter", sans-serif;
  font-weight: 500;
  text-decoration: none;
  display: flex;
  align-items: center;

  svg {
    font-size: 1.5rem;
    margin-right: 0.5rem;
    color: currentColor;
    transition: color 0.2s;
  }

  &:hover {
    color: #e6f6f4;
    transition: 0.2s;
  }
`;
