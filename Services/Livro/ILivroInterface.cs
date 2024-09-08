using BibliotecaApi.Dto.Autor;
using BibliotecaApi.Dto.Livro;
using BibliotecaApi.Models;

namespace BibliotecaApi.Services.Livro;

public interface ILivroInterface
{
    Task<ResponseModel<List<LivroModel>>> ListarLivros();
    Task<ResponseModel<LivroModel>> BuscarLivroPorId(int idLivro);
    Task<ResponseModel<List<LivroModel>>> BuscarLivrosPorIdAutor(int idAutor);
    Task<ResponseModel<List<LivroModel>>> CriarLivro(LivroCriacaoDto livroCriacaoDto);
    Task<ResponseModel<List<LivroModel>>> EditarLivro(int idLivro, LivroCriacaoDto livroCriacaoDto);
    Task<ResponseModel<List<LivroModel>>> RemoverLivro(int idLivro);
}
