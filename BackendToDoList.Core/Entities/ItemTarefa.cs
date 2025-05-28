namespace BackendToDoList.Core.Entities
{
    public class ItemTarefa
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public int? Quantidade { get; set; }
        public bool Complete { get; set; }
        public int TarefaId { get; set; }
        public Tarefa Tarefa { get; set; }

        public ItemTarefa() { }
    }
}
