import React from "react";
import { FormWrapper } from "./styles";

export const Form = (props: React.PropsWithChildren) => {
  return <FormWrapper>{props.children}</FormWrapper>;
};
