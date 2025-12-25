import styled from "styled-components";

export const TableWrapper = styled.table`
  width: 100%;
  border-collapse: collapse;
  font-family: "Inter", sans-serif;
`;

export const TableHead = styled.thead`
  background-color: #00a991;
`;

export const TableHeader = styled.th`
  padding: 1rem 0;
  text-align: center;
  font-weight: 600;
  font-family: "Inter", sans-serif;
  color: #003b33;
`;

export const TableRow = styled.tr`
  &:nth-child(even) {
    background-color: #d9f2ef;
  }
`;

export const TableCell = styled.td`
  padding: 1rem 0;
  color: #003b33;
  text-align: center;
`;

export const ActionButton = styled.button`
  background: none;
  border: none;
  &:hover {
    transition: 0.2s;
    color: #cc0000;
  }
`;
