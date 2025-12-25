import React from "react";
import { ButtonWrapper, ButtonIcon } from "./styles.ts";

interface ButtonProps extends React.ButtonHTMLAttributes<HTMLButtonElement> {
  children: React.ReactNode;
}

const Button = (props: ButtonProps) => {
  return (
    <ButtonWrapper {...props}>
      <ButtonIcon>+</ButtonIcon>
      {props.children}
    </ButtonWrapper>
  );
};
export default Button;
