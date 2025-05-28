namespace BackendToDoList.Application.ViewModels.ItemTarefa
{
    public class ViewItemTarefaDto
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public int? Quantidade { get; set; }
        public bool Complete { get; set; }
    }
}
