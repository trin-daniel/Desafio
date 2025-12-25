import styled from "styled-components";

export const ButtonWrapper = styled.button`
  display: flex;
  padding: 1rem 1.5rem;
  max-width: 138px;
  align-items: center;
  justify-content: center;
  border-radius: 0.5rem;
  background: #00a991;
  align-self: center;
  color: #003b33;
  border: 2px solid #00a991;
  text-transform: uppercase;
  font-size: 1rem;
  font-weight: 500;
  font-family: "Inter", sans-serif;
  margin-bottom: 3.5rem;
  &:hover {
    color: #00a991;
    background: #003b33;
    border-color: #003b33;
    transition: ease-in-out 0.2s;
  }
`;

export const ButtonIcon = styled.span`
  font-size: 1rem;
`;
