using BibliotecaApi.Data;
using BibliotecaApi.Dto.Autor;
using BibliotecaApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaApi.Services.Autor;

public class AutorService : IAutorInterface
{
    private readonly AppDbContext _context;

    public AutorService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ResponseModel<AutorModel>> BuscarAutorPorId(int idAutor)
    {
        ResponseModel<AutorModel> resposta = new ResponseModel<AutorModel>();

        try
        {
            var autor = await _context.Autores.FirstOrDefaultAsync(i => i.Id == idAutor);

            if (autor == null)
            {
                resposta.Mensagem = $"Nenhum autor com id = {idAutor} foi encontrado!";
                return resposta;
            }

            resposta.Dados = autor;
            resposta.Mensagem = $"O autor id = {idAutor} foi encontrado com sucesso!";

            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<AutorModel>> BuscarAutorPorIdLivro(int idLivro)
    {
        ResponseModel<AutorModel> resposta = new ResponseModel<AutorModel>();

        try 
        {
            var livro = await _context.Livros.Include(p => p.Autor).AsNoTracking().FirstOrDefaultAsync(i => i.Id == idLivro);

            if (livro == null)
            {
                resposta.Mensagem = $"Nenhum livro de id = {idLivro} foi encontrado!";
                return resposta;
            }

            var autor = await _context.Autores.Include(p => p.Livros).AsNoTracking().FirstOrDefaultAsync(autorBanco => autorBanco.Id == livro.Autor.Id);

            resposta.Dados = autor;
            resposta.Mensagem = $"O autor do livro de id = {idLivro} retornado com sucesso!";

            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<AutorModel>>> CriarAutor(AutorCriacaoDto autorCriacaoDto)
    {
        ResponseModel<List<AutorModel>> resposta = new ResponseModel<List<AutorModel>>();
        try
        {
            var autor = new AutorModel()
            {
                Nome = autorCriacaoDto.Nome,
                Sobrenome = autorCriacaoDto.Sobrenome
            };

            _context.Autores.Add(autor);
            await _context.SaveChangesAsync();

            resposta.Dados = await _context.Autores.ToListAsync();
            resposta.Mensagem = "Autor registrado com sucesso!";

            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<AutorModel>>> ListarAutores()
    {
        ResponseModel<List<AutorModel>> resposta = new ResponseModel<List<AutorModel>>();
        try
        {
            var autores = await _context.Autores.ToListAsync();

            resposta.Dados = autores;
            resposta.Mensagem = "Todos os autores foram coletados!";

            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<AutorModel>>> EditarAutor(int idAutor, AutorCriacaoDto autorCriacaoDto)
    {
        ResponseModel<List<AutorModel>> resposta = new ResponseModel<List<AutorModel>>();
        try
        {
            var autor = await _context.Autores.AsNoTracking().FirstOrDefaultAsync(i => i.Id == idAutor);

            if(autor == null)
            {
                resposta.Mensagem = $"Não existe um autor com id = {idAutor}";
                return resposta;
            }

            var autorModificado = new AutorModel()
            {
                Id = idAutor,
                Nome = autorCriacaoDto.Nome,
                Sobrenome = autorCriacaoDto.Sobrenome
            };

            _context.Entry(autorModificado).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            var autores = await _context.Autores.AsNoTracking().ToListAsync();

            resposta.Dados = autores;
            resposta.Mensagem = "Autor editado com sucesso!";

            return resposta;

        }
        catch (Exception ex)
        {
            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<AutorModel>>> RemoverAutor(int idAutor)
    {
        ResponseModel<List<AutorModel>> resposta = new ResponseModel<List<AutorModel>>();
        try
        {
            var autor = await _context.Autores.FirstOrDefaultAsync(i => i.Id == idAutor);

            if (autor == null)
            {
                resposta.Mensagem = $"Não existe um autor com id = {idAutor}";
                return resposta;
            }

            _context.Autores.Remove(autor);
            await _context.SaveChangesAsync();

            var autores = await _context.Autores.ToListAsync();

            resposta.Dados = autores;
            resposta.Mensagem = "Autor deletado com sucesso!";

            return resposta;

        }
        catch (Exception ex)
        {
            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }
}
