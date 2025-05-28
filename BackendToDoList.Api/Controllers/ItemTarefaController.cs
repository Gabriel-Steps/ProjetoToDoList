using BackendToDoList.Application.InputModels.ItemTarefa;
using BackendToDoList.Application.Repositories.ItemTarefaRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackendToDoList.Api.Controllers
{
    [Route("api/item-tarefa"), ApiController, Authorize]
    public class ItemTarefaController : ControllerBase
    {
        private readonly IITemTarefaRepository _repository;
        public ItemTarefaController(IITemTarefaRepository repository)
        {
            _repository = repository;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(CreateItemTarefaDto model)
        {
            var created = await _repository.Create(model);
            if (!created)
                return BadRequest(new { status = false, mensagem = "Houve um erro ao criar o item" });
            return Ok(new { status = true, mensagem = "Item criado com sucesso" });
        }

        [HttpPut("concluir-item/{id}")]
        public async Task<IActionResult> ConcluirItemTarefa(int id)
        {
            var updated = await _repository.ConcluirTarefa(id);
            if (!updated)
                return BadRequest(new { status = false, mensagem = "Houve um erro ao concluir o item" });
            return Ok(new { status = true, mensagem = "Item concluido com sucesso" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repository.Delete(id);
            if (!deleted)
                return BadRequest(new { status = false, mensagem = "Houve um erro ao deletar o item" });
            return Ok(new { status = true, mensagem = "Item deletado com sucesso" });
        }

        [HttpGet("get-all-by-tarefa/{id}")]
        public async Task<IActionResult> GetAllByTarefa(int id)
        {
            var tarefas = await _repository.GetAllByTarefa(id);
            return Ok(new { status = true, mensagem = "Tarefas obtidas com sucesso", tarefas });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var tarefa = await _repository.GetById(id);
            if (tarefa == null)
                return BadRequest(new { status = false, mensagem = "Item não encontrado" });
            return Ok(new { status = true, mensagem = "Item encontrado com sucesso", tarefa });
        }
    }
}
