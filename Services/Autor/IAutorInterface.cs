using BibliotecaApi.Dto.Autor;
using BibliotecaApi.Models;

namespace BibliotecaApi.Services.Autor;

public interface IAutorInterface
{
    Task<ResponseModel<List<AutorModel>>> ListarAutores();
    Task<ResponseModel<AutorModel>> BuscarAutorPorId(int idAutor);
    Task<ResponseModel<AutorModel>> BuscarAutorPorIdLivro(int idLivro);
    Task<ResponseModel<List<AutorModel>>> CriarAutor(AutorCriacaoDto autorCriacaoDto);
    Task<ResponseModel<List<AutorModel>>> EditarAutor(int idAutor, AutorCriacaoDto autorCriacaoDto);
    Task<ResponseModel<List<AutorModel>>> RemoverAutor(int idAutor);




}
