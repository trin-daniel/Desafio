import React from "react";
import {
  ErrorItem,
  ErrorList,
  IconWrapper,
  Label,
  SelectContainer,
  StyledSelect,
  Wrapper,
} from "./styles";
import type { IconType } from "react-icons";

export interface Option {
  value: string;
  label: string;
  disabled?: boolean;
}

interface SelectProps extends React.SelectHTMLAttributes<HTMLSelectElement> {
  label?: string;
  errors?: string[];
  icon?: IconType;
  options: Option[];
}
const Select = ({
  label,
  options,
  errors,
  icon: Icon,
  ...props
}: SelectProps) => {
  const hasError = (errors && errors.length > 0) ?? false;
  return (
    <Wrapper>
      {label && <Label>{label}</Label>}
      <SelectContainer $hasError={hasError}>
        {Icon && (
          <IconWrapper $hasError={hasError}>
            <Icon size={24} />
          </IconWrapper>
        )}
        <StyledSelect id={props.id} {...props}>
          <option value="">Selecione...</option>
          {options.map((opt) => (
            <option key={opt.value} value={opt.value}>
              {opt.label}
            </option>
          ))}
        </StyledSelect>
      </SelectContainer>
      {errors && errors.length > 0 && (
        <ErrorList>
          {errors.map((err, index) => (
            <ErrorItem key={index}>{err}</ErrorItem>
          ))}
        </ErrorList>
      )}
    </Wrapper>
  );
};
export default Select;
