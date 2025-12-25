import styled from "styled-components";

export const Wrapper = styled.div`
  width: 100%;
  display: flex;
  justify-content: center;
  align-items: center;
  svg {
    font-size: 1.5rem;
    color: #003b33;
    transform-origin: 50% 50%;
    transform: rotate();
    animation: rotate 1s linear infinite;
  }
  @keyframes rotate {
    from {
      transform: rotate(0deg);
    }
    to {
      transform: rotate(360deg);
    }
  }
`;
