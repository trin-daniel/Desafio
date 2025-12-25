import { Loading } from "../loading";
import {
  ActionButton,
  TableCell,
  TableHead,
  TableHeader,
  TableRow,
  TableWrapper,
} from "./styles";
import type { IconType } from "react-icons";

export interface Column<T> {
  header: string;
  accessor: keyof T;
  cell?: (row: T) => React.ReactNode;
}

interface Action<T> {
  label?: string;
  onClick: (row: T) => void | Promise<void>;
}

interface TableProps<T> {
  columns: Column<T>[];
  data: T[];
  icon?: IconType;
  actions?: Action<T>[];
  loading: boolean;
  emptyMessage: string;
}

export function Table<T extends object>({
  columns,
  data,
  loading,
  emptyMessage = "Nenhum dado encontrado",
  icon: Icon,
  actions,
}: TableProps<T>) {
  return (
    <TableWrapper>
      <TableHead>
        <tr>
          {columns.map((col) => (
            <TableHeader key={String(col.accessor)}>{col.header}</TableHeader>
          ))}
          {actions && actions.length > 0 && <TableHeader>Ações</TableHeader>}
        </tr>
      </TableHead>
      <tbody>
        {loading ? (
          <TableRow>
            <TableCell colSpan={columns.length + (actions ? 1 : 0)}>
              <Loading />
            </TableCell>
          </TableRow>
        ) : data.length === 0 ? (
          <TableRow>
            <TableCell colSpan={columns.length + (actions ? 1 : 0)}>
              {emptyMessage}
            </TableCell>
          </TableRow>
        ) : (
          data.map((row, rowIndex) => (
            <TableRow key={rowIndex}>
              {columns.map((col) => (
                <TableCell key={String(col.accessor)}>
                  {col.cell ? col.cell(row) : String(row[col.accessor])}
                </TableCell>
              ))}
              {actions && (
                <TableCell>
                  {actions.map((action, index) => (
                    <ActionButton
                      key={index}
                      onClick={() => action.onClick(row)}
                    >
                      {Icon && <Icon size={18} />}
                    </ActionButton>
                  ))}
                </TableCell>
              )}
            </TableRow>
          ))
        )}
      </tbody>
    </TableWrapper>
  );
}
