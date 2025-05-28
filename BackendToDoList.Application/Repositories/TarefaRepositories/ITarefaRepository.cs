using BackendToDoList.Application.InputModels.Tarefa;
using BackendToDoList.Application.ViewModels.Tarefa;
using BackendToDoList.Core.Entities;

namespace BackendToDoList.Application.Repositories.TarefaRepositories
{
    public interface ITarefaRepository
    {
        public Task<bool> Create(CreateTarefaDto model);
        public Task<List<Tarefa>> GetAllByUser(int idUser);
        public Task<bool> ConcluirTarefa(int id);
        public Task<bool> Delete(int id);
        public Task<ViewTarefaDto?> GetById(int id);
    }
}
