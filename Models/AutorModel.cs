namespace BibliotecaApi.Models;

public class AutorModel
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Sobrenome { get; set; }
    public ICollection<LivroModel> Livros { get; set; }
}
