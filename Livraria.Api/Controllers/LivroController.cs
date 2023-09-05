using Livraria.Domain.Commands.Livro.Input;
using Livraria.Domain.Commands.Livro.Output;
using Livraria.Domain.Interfaces.Commands;
using Livraria.Domain.Interfaces.Handler;
using Livraria.Domain.Interfaces.Repositories;
using Livraria.Domain.Query;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace LivrariaAPI.Api.Controllers
{
    [Consumes("application/json")]
    [Produces("application/json")]
    [ApiController]
    public class LivroController : ControllerBase
    {
        private readonly ILivroRepository _repositorio;
        private readonly ILivroHandler _handler;

        public LivroController(ILivroRepository repositorio, ILivroHandler handler)
        {
            _repositorio = repositorio;
            _handler = handler;
        }

        /// <summary>
        /// Livros
        /// </summary>                
        /// <remarks><h2><b>Lista todos os Livros.</b></h2></remarks>
        /// <response code="200">OK Request</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [Route("v1/livros")]
        public IEnumerable<LivroQueryResult> Livros()
        {
            return _repositorio.Listar();
        }

        /// <summary>
        /// Livro
        /// </summary>                
        /// <remarks><h2><b>Consulta o Livro.</b></h2></remarks>
        /// <param name="id">Parâmetro requerido id do Livro</param>
        /// <response code="200">OK Request</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [Route("v1/livros/{id}")]
        public LivroQueryResult Livro(Guid id)
        {
            return _repositorio.ObterPorId(id);
        }

        /// <summary>
        /// Incluir Livro 
        /// </summary>                
        /// <remarks><h2><b>Incluir novo Livro na base de dados.</b></h2></remarks>
        /// <param name="command">Parâmetro requerido command de Insert</param>
        /// <response code="200">OK Request</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPost]
        [Route("v1/livros")]
        public ICommandResult LivroNovo([FromBody] AdicionarLivroCommand command)
        {
            var result = (AdicionarLivroCommandResult)_handler.Handle(command);
            return result;
        }

        /// <summary>
        /// Alterar Livro
        /// </summary>        
        /// <remarks><h2><b>Alterar Livro na base de dados.</b></h2></remarks>
        /// <param name="id">Parâmetro requerido id do Livro</param>        
        /// <param name="command">Parâmetro requerido command de Update</param>
        /// <response code="200">OK Request</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPut]
        [Route("v1/livros/{id}")]
        public ICommandResult LivroAlterar(Guid id, [FromBody] AtualizarLivroCommand command)
        {
            command.Id = id;
            var result = (AtualizarLivroCommandResult)_handler.Handle(command);
            return result;
        }

        /// <summary>
        /// Excluir Livro
        /// </summary>                
        /// <remarks><h2><b>Excluir Livro na base de dados.</b></h2></remarks>
        /// <param name="id">Parâmetro requerido id do Livro</param>        
        /// <response code="200">OK Request</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal Server Error</response>
        [HttpDelete]
        [Route("v1/livros/{id}")]
        public ICommandResult LivroExcluir(Guid id)
        {
            var command = new ApagarLivroCommand() { Id = id };
            var result = (ApagarLivroCommandResult)_handler.Handle(command);
            return result;
        }
    }
}