namespace Almirante.Domain.Entities;

public class ObraDeArte
{
    public int Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Autor { get; set; } = string.Empty;
    public string UrlImagem { get; set; } = string.Empty;
}