import styled from "styled-components";

export const CardItem = styled.div`
  max-width: 336px;
  width: 100%;
  height: 232px;
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;

  -webkit-box-shadow: 3px 2px 9px -3px rgba(0, 169, 145, 0.85);
  box-shadow: 3px 2px 9px -3px rgba(0, 169, 145, 0.85);
  @media (max-width: 800px) {
    max-width: 100%;
  }
`;

export const CardTitle = styled.h3`
  font-size: 2rem;
  font-family: "Inter", sans-serif;
  font-weight: 500;
  color: #003b33;
  text-transform: uppercase;
  margin: 0;
  padding: 1rem 0;
`;

export const CardContent = styled.span`
  font-size: 2rem;
  font-family: "Inter", sans-serif;
  font-weight: 500;
  color: #003b33;
  text-transform: uppercase;
`;
