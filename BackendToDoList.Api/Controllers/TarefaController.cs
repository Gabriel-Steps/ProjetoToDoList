using BackendToDoList.Application.InputModels.Tarefa;
using BackendToDoList.Application.Repositories.TarefaRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackendToDoList.Api.Controllers
{
    [Route("api/tarefa"), ApiController, Authorize]
    public class TarefaController : ControllerBase
    {
        private readonly ITarefaRepository _repository;
        public TarefaController(ITarefaRepository repository)
        {
            _repository = repository;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(CreateTarefaDto model)
        {
            var created = await _repository.Create(model);
            if (!created)
                return BadRequest(new { status = false, mensagem = "Houve um erro ao criar a tarefa"});
            return Ok(new { status = true, mensagem = "Tarefa criada com sucesso"});
        }

        [HttpPut("concluir-tarefa/{id}")]
        public async Task<IActionResult> ConcluirTarefa(int id)
        {
            var updated = await _repository.ConcluirTarefa(id);
            if (!updated)
                return BadRequest(new { status = false, mensagem = "Houve um erro ao concluir a tarefa" });
            return Ok(new { status = true, mensagem = "Tarefa concluida com sucesso" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repository.Delete(id);
            if (!deleted)
                return BadRequest(new { status = false, mensagem = "Houve um erro ao deletar tarefa" });
            return Ok(new { status = true, mensagem = "Tarefa deletada com sucesso" });
        }

        [HttpGet("get-all-by-user/{id}")]
        public async Task<IActionResult> GetAllByUser(int id)
        {
            var tarefas = await _repository.GetAllByUser(id);
            return Ok(new { status = true, mensagem = "Tarefas obtidas com sucesso", tarefas});
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var tarefa = await _repository.GetById(id);
            if (tarefa == null)
                return BadRequest(new { status = false, mensagem = "Tarefa não encontrada" });
            return Ok(new { status = true, mensagem = "Tarefa encontrada com sucesso", tarefa });
        }
    }
}
