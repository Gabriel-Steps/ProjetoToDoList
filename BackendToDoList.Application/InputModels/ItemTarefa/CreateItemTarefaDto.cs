namespace BackendToDoList.Application.InputModels.ItemTarefa
{
    public class CreateItemTarefaDto
    {
        public string Descricao { get; set; }
        public int? Quantidade { get; set; }
        public bool Complete { get; set; } = false;
        public int TarefaId { get; set; }
    }
}
