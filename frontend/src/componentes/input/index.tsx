import React from "react";
import {
  Wrapper,
  Label,
  StyledInput,
  InputContainer,
  IconWrapper,
  ErrorList,
  ErrorItem,
} from "./styles";
import type { IconType } from "react-icons";

interface InputProps extends React.InputHTMLAttributes<HTMLInputElement> {
  label?: string;
  icon?: IconType;
  errors?: string[];
}

const Input = ({ label, errors, id, icon: Icon, ...props }: InputProps) => {
  const hasError = (errors && errors.length > 0) ?? false;

  return (
    <Wrapper>
      <Label htmlFor={id}>{label}</Label>
      <InputContainer $hasError={hasError}>
        {Icon && (
          <IconWrapper $hasError={hasError}>
            <Icon size={24} />
          </IconWrapper>
        )}
        <StyledInput id={id} {...props} />
      </InputContainer>
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
export default Input;
