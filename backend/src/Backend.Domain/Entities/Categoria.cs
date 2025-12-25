using Backend.Domain.Enums;

namespace Backend.Domain.Entities;

public class Categoria
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string Descricao { get; set; } = string.Empty;
    public TipoTransacao Finalidade { get; set; }
    public ICollection<Transacao> Transacaos { get; set; } = new List<Transacao>();
}