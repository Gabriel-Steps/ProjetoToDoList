using BackendToDoList.Application.InputModels.ItemTarefa;
using BackendToDoList.Application.ViewModels.ItemTarefa;

namespace BackendToDoList.Application.Repositories.ItemTarefaRepositories
{
    public interface IITemTarefaRepository
    {
        public Task<bool> Create(CreateItemTarefaDto model);
        public Task<bool> ConcluirTarefa(int id);
        public Task<bool> Delete(int id);
        public Task<ViewItemTarefaDto?> GetById(int id);
        public Task<List<ViewItemTarefaDto>> GetAllByTarefa(int idTarefa);
    }
}
