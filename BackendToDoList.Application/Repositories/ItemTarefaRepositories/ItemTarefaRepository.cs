using BackendToDoList.Application.InputModels.ItemTarefa;
using BackendToDoList.Application.ViewModels.ItemTarefa;
using BackendToDoList.Core.Entities;
using BackendToDoList.Infra;
using Microsoft.EntityFrameworkCore;

namespace BackendToDoList.Application.Repositories.ItemTarefaRepositories
{
    public class ItemTarefaRepository : IITemTarefaRepository
    {
        private readonly SistemaToDoListDbContext _context;
        public ItemTarefaRepository(SistemaToDoListDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ConcluirTarefa(int id)
        {
            var itemTarefa = await _context.ItensTarefas.FindAsync(id);
            if (itemTarefa == null)
                return false;
            itemTarefa.Complete = true;
            _context.ItensTarefas.Update(itemTarefa);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Create(CreateItemTarefaDto model)
        {
            if (model != null)
            {
                var itemTarefa = new ItemTarefa()
                {
                    Descricao = model.Descricao,
                    Quantidade = model.Quantidade,
                    Complete = model.Complete,
                    TarefaId = model.TarefaId
                };
                await _context.ItensTarefas.AddAsync(itemTarefa);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> Delete(int id)
        {
            var itemTarefa = await _context.ItensTarefas.FindAsync(id);
            if (itemTarefa == null) return false;
            _context.ItensTarefas.Remove(itemTarefa);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<ViewItemTarefaDto>> GetAllByTarefa(int idTarefa)
        {
            return await _context
                .ItensTarefas
                .Where(t => t.TarefaId == idTarefa)
                .Select(i => new ViewItemTarefaDto()
                {
                    Descricao = i.Descricao,
                    Quantidade = i.Quantidade,
                    Complete = i.Complete,
                    Id = i.Id
                })
                .AsNoTracking()
                .Distinct()
                .ToListAsync();
        }

        public async Task<ViewItemTarefaDto?> GetById(int id)
        {
            var tarefa = await _context
                .ItensTarefas
                .Where(t => t.Id == id)
                .Select(i => new ViewItemTarefaDto()
                {
                    Descricao = i.Descricao,
                    Quantidade = i.Quantidade,
                    Complete = i.Complete,
                    Id = i.Id
                }).FirstOrDefaultAsync();
            if (tarefa == null) return null;
            return tarefa;
        }
    }
}
