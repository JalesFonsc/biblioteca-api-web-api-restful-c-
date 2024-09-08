using BibliotecaApi.Dto.Autor;
using BibliotecaApi.Models;
using BibliotecaApi.Services.Autor;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AutorController : ControllerBase
{
    private readonly IAutorInterface _autorInterface;

    public AutorController(IAutorInterface autorInterface)
    {
        _autorInterface = autorInterface;
    }

    [HttpGet("ListarAutores")]
    public async Task<ActionResult<ResponseModel<List<AutorModel>>>> ListarAutores() {
        var autores = await _autorInterface.ListarAutores();

        return Ok(autores);
    }

    [HttpGet("BuscarAutorPorId/{idAutor:int}")]
    public async Task<ActionResult<ResponseModel<AutorModel>>> BuscarAutorPorId(int idAutor)
    {
        var autor = await _autorInterface.BuscarAutorPorId(idAutor);

        return Ok(autor);
    }

    [HttpGet("BuscarAutorPorIdLivro/{idLivro:int}")]
    public async Task<ActionResult<ResponseModel<AutorModel>>> BuscarAutorPorIdLivro(int idLivro)
    {
        var autor = await _autorInterface.BuscarAutorPorIdLivro(idLivro);

        return Ok(autor);
    }

    [HttpPost("CriarAutor")]
    public async Task<ActionResult<ResponseModel<List<AutorModel>>>> CriarAutor(AutorCriacaoDto autorCriacaoDto)
    {
        var autores = await _autorInterface.CriarAutor(autorCriacaoDto);

        return Ok(autores);
    }

    [HttpPut("EditarAutor/{idAutor:int}")]
    public async Task<ActionResult<ResponseModel<List<AutorModel>>>> EditarAutor(int idAutor, AutorCriacaoDto autorCriacaoDto)
    {
        var autores = await _autorInterface.EditarAutor(idAutor, autorCriacaoDto);

        return Ok(autores);
    }

    [HttpDelete("RemoverAutor/{idAutor:int}")]
    public async Task<ActionResult<ResponseModel<List<AutorModel>>>> RemoverAutor(int idAutor)
    {
        var autores = await _autorInterface.RemoverAutor(idAutor);

        return Ok(autores);
    }
}
