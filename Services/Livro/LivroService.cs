using BibliotecaApi.Data;
using BibliotecaApi.Dto.Autor;
using BibliotecaApi.Dto.Livro;
using BibliotecaApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaApi.Services.Livro;

public class LivroService : ILivroInterface
{
    private readonly AppDbContext _context;

    public LivroService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ResponseModel<LivroModel>> BuscarLivroPorId(int idLivro)
    {
        ResponseModel<LivroModel> resposta = new ResponseModel<LivroModel>();
        try
        {
            var livro = await _context.Livros.FirstOrDefaultAsync(livroBanco => livroBanco.Id == idLivro);

            if(livro == null)
            {
                resposta.Mensagem = $"Nenhum livro com id = {idLivro} foi encontrado!";
                return resposta;
            }

            resposta.Dados = livro;
            resposta.Mensagem = $"O livro id = {idLivro} foi encontrado com sucesso!";

            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<LivroModel>>> BuscarLivrosPorIdAutor(int idAutor)
    {
        ResponseModel<List<LivroModel>> resposta = new ResponseModel<List<LivroModel>>();
        try
        {
            var livros = await _context.Livros
                .Include(p => p.Autor)
                .Where(livroBanco => livroBanco.Autor.Id == idAutor)
                .AsNoTracking()
                .ToListAsync();

            if (livros.Count == 0)
            {
                resposta.Mensagem = $"O autor com id = {idAutor} não possui nenhum livro cadastrado!";
                return resposta;
            }

            resposta.Dados = livros;
            resposta.Mensagem = $"O(s) livro(s) do autor id = {idAutor} foi/foram encontrado(s) com sucesso!";

            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<LivroModel>>> CriarLivro(LivroCriacaoDto livroCriacaoDto)
    {
        ResponseModel<List<LivroModel>> resposta = new ResponseModel<List<LivroModel>>();
        try
        {
            var autor = await _context.Autores.FirstOrDefaultAsync(autorBanco => autorBanco.Id == livroCriacaoDto.idAutor);

            if(autor == null)
            {
                resposta.Mensagem = $"O autor de id = {livroCriacaoDto.idAutor} não foi encontrado";
                return resposta;
            }

            var livro = new LivroModel()
            {
                Titulo = livroCriacaoDto.Titulo,
                Autor = autor
            };

            _context.Add(livro);
            await _context.SaveChangesAsync();

            var livros = await _context.Livros.ToListAsync();

            resposta.Dados = livros;
            resposta.Mensagem = "Livro cadastrado com sucesso!";

            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<LivroModel>>> EditarLivro(int idLivro, LivroCriacaoDto livroCriacaoDto)
    {
        ResponseModel<List<LivroModel>> resposta = new ResponseModel<List<LivroModel>>();
        try
        {
            var autor = await _context.Autores.AsNoTracking().FirstOrDefaultAsync(autorBanco => autorBanco.Id == livroCriacaoDto.idAutor);

            if (autor == null)
            {
                resposta.Mensagem = $"O autor de id = {livroCriacaoDto.idAutor} não foi encontrado";
                return resposta;
            }

            var livro = await _context.Livros.AsNoTracking().FirstOrDefaultAsync(livroBanco => livroBanco.Id == idLivro);

            if (livro == null)
            {
                resposta.Mensagem = $"O livro de id = {idLivro} não foi encontrado";
                return resposta;
            }

            var livroMoficado = new LivroModel()
            {
                Id = idLivro,
                Titulo = livroCriacaoDto.Titulo,
                Autor = autor
            };

            _context.Entry(livroMoficado).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            var livros = await _context.Livros.AsNoTracking().ToListAsync();

            resposta.Dados = livros;
            resposta.Mensagem = "Livro cadastrado com sucesso!";

            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<LivroModel>>> ListarLivros()
    {
        ResponseModel<List<LivroModel>> resposta = new ResponseModel<List<LivroModel>>();
        try
        {
            var livros = await _context.Livros.ToListAsync();

            resposta.Dados = livros;
            resposta.Mensagem = "Todos os livros foram coletados!";

            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<LivroModel>>> RemoverLivro(int idLivro)
    {
        ResponseModel<List<LivroModel>> resposta = new ResponseModel<List<LivroModel>>();
        try
        {
            var livro = await _context.Livros.FirstOrDefaultAsync(i => i.Id == idLivro);

            if (livro == null)
            {
                resposta.Mensagem = $"Não existe um livro com id = {idLivro}";
                return resposta;
            }

            _context.Livros.Remove(livro);
            await _context.SaveChangesAsync();

            var livros = await _context.Livros.ToListAsync();

            resposta.Dados = livros;
            resposta.Mensagem = "Livro deletado com sucesso!";

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
