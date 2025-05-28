using BackendToDoList.Application.InputModels.Tarefa;
using BackendToDoList.Application.ViewModels.Tarefa;
using BackendToDoList.Core.Entities;
using BackendToDoList.Infra;
using Microsoft.EntityFrameworkCore;

namespace BackendToDoList.Application.Repositories.TarefaRepositories
{
    public class TarefaRepository : ITarefaRepository
    {
        private readonly SistemaToDoListDbContext _context;
        public TarefaRepository(SistemaToDoListDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ConcluirTarefa(int id)
        {
            var tarefa = await _context.Tarefas.FindAsync(id);
            if (tarefa == null)
                return false;
            tarefa.Complete = true;
            _context.Tarefas.Update(tarefa);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Create(CreateTarefaDto model)
        {
            if(model != null)
            {
                var tarefa = new Tarefa()
                {
                    Nome = model.Nome,
                    Descricao = model.Descricao,
                    UsuarioId = model.UsuarioId
                };
                await _context.Tarefas.AddAsync(tarefa);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> Delete(int id)
        {
            var tarefa = await _context.Tarefas.FindAsync(id);
            if (tarefa == null) return false;
            _context.Tarefas.Remove(tarefa);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Tarefa>> GetAllByUser(int idUser)
        {
            return await _context
                .Tarefas
                .Where(t => t.UsuarioId == idUser)
                .AsNoTracking()
                .Distinct()
                .ToListAsync();
        }

        public async Task<ViewTarefaDto?> GetById(int id)
        {
            var tarefa = await _context
                .Tarefas
                .Where(t => t.Id == id)
                .Select(t => new ViewTarefaDto {
                    Nome = t.Nome,
                    Descricao = t.Descricao,
                    Complete = t.Complete,
                    Id = t.Id
                }).FirstOrDefaultAsync();
            if (tarefa == null) return null;
            return tarefa;
        }
    }
}
