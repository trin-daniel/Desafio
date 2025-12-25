import styled from "styled-components";

export const Wrapper = styled.div`
  display: flex;
  flex-direction: column;
  margin-bottom: 2.5rem;
  width: 83%;
  &:first-child {
    margin-top: 3.5rem;
  }
`;

export const Label = styled.label`
  font-size: 1rem;
  font-family: "Inter", sans-serif;
  font-weight: 500;
  margin-bottom: 1rem;
  color: #003b33;
`;

export const InputContainer = styled.div<{ $hasError: boolean }>`
  display: flex;
  align-items: center;
  border: 2px solid ${({ $hasError }) => ($hasError ? "#CC0000" : "#B0E4DD")};
  border-radius: 0.25rem;
  padding: 0 1rem;
  height: 56px;
  background-color: #fff;
  &:focus-within {
    border-color: ${({ $hasError }) => ($hasError ? "#CC0000" : "#00A991")};
  }
`;

export const StyledInput = styled.input`
  flex: 1;
  height: 56px;
  border: none;
  outline: none;
  font-size: 1rem;
  color: #003b33;
  background: transparent;

  &::placeholder {
    color: #b0e4dd;
  }
`;

export const IconWrapper = styled.div<{ $hasError: boolean }>`
  margin-right: 1rem;
  display: flex;
  align-items: center;
  color: ${({ $hasError }) => ($hasError ? "#CC0000" : "#B0E4DD")};
`;
export const ErrorList = styled.ul`
  margin-top: 0.5rem;
  padding-left: 1rem;
  font-size: 1rem;
  color: #cc0000;
  display: flex;
  flex-direction: column;
  justify-content: center;
`;

export const ErrorItem = styled.li`
  padding: 4px 0;
  color: #cc0000;
  font-size: 1rem;
  font-family: "Inter", sans-serif;
  text-decoration: none;
  display: flex;
  align-items: center;
  gap: 0.5rem;

  &::before {
    content: "⚠";
    font-size: 1rem;
  }
`;
