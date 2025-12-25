import { Title } from "./styles";

export const PageTitle = (props: React.PropsWithChildren) => {
  return <Title>{props.children}</Title>;
};
