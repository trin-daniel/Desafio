import { CardContent, CardItem, CardTitle } from "./styles";

interface CardProps {
  title: string;
  value: string;
}

export const Card = (props: CardProps) => {
  return (
    <CardItem>
      <CardTitle>{props.title}</CardTitle>
      <CardContent>{props.value}</CardContent>
    </CardItem>
  );
};
