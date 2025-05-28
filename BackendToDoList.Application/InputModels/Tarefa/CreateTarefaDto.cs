namespace BackendToDoList.Application.InputModels.Tarefa
{
    public class CreateTarefaDto
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int UsuarioId { get; set; }
    }
}
