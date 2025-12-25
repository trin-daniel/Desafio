namespace Backend.Domain.Entities;

/*
 * Definicao do moelo pesso
 * Por que ICollection em vez de IEnumerable?
 * Porque algumas propriedades estao sendo inicializadas explicitamente, enquanto outras?
 * Porque Id esta sendo inicializado no modelo de dados em vez de deixar o banco gerar automaticamente?
 * 
 */
public class Pessoa
{
    public Guid Id { get; private init; } = Guid.NewGuid();
    public required string Nome { get; set; }
    public int Idade { get; set; }
    public ICollection<Transacao> Transacoes { get; set; } = new List<Transacao>();
}