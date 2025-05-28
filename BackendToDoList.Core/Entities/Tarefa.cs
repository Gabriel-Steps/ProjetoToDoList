namespace BackendToDoList.Core.Entities
{
    public class Tarefa
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public bool Complete { get; set; }
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        public ICollection<ItemTarefa> ItensTarefa { get; set; }

        public Tarefa()
        {
            Complete = false;
        }
    }
}
