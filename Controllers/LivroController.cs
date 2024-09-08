using BibliotecaApi.Dto.Livro;
using BibliotecaApi.Models;
using BibliotecaApi.Services.Livro;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LivroController : ControllerBase
    {
        private ILivroInterface _livroInterface;

        public LivroController(ILivroInterface livroInterface)
        {
            _livroInterface = livroInterface;
        }

        [HttpGet("ListarLivros")]
        public async Task<ActionResult<ResponseModel<List<LivroModel>>>> ListarLivros()
        {
            var livros = await _livroInterface.ListarLivros();

            return livros;
        }

        [HttpGet("BuscarLivroPorId/{idLivro:int}")]
        public async Task<ActionResult<ResponseModel<LivroModel>>> BuscarLivroPorId(int idLivro)
        {
            var livro = await _livroInterface.BuscarLivroPorId(idLivro);

            return livro;
        }

        [HttpGet("BuscarLivrosPorIdAutor/{idAutor:int}")]
        public async Task<ActionResult<ResponseModel<List<LivroModel>>>> BuscarLivrosPorIdAutor(int idAutor)
        {
            var livros = await _livroInterface.BuscarLivrosPorIdAutor(idAutor);

            return livros;
        }

        [HttpPost("CriarLivro")]
        public async Task<ActionResult<ResponseModel<List<LivroModel>>>> CriarLivro(LivroCriacaoDto livroCriacaoDto)
        {
            var livros = await _livroInterface.CriarLivro(livroCriacaoDto);

            return livros;
        }

        [HttpPut("EditarLivro/{idLivro:int}")]
        public async Task<ActionResult<ResponseModel<List<LivroModel>>>> EditarLivro(int idLivro, LivroCriacaoDto livroCriacaoDto)
        {
            var livros = await _livroInterface.EditarLivro(idLivro, livroCriacaoDto);

            return livros;
        }

        [HttpDelete("RemoverLivro/{idLivro:int}")]
        public async Task<ActionResult<ResponseModel<List<LivroModel>>>> RemoverLivro(int idLivro)
        {
            var livros = await _livroInterface.RemoverLivro(idLivro);

            return livros;
        }
    }
}
