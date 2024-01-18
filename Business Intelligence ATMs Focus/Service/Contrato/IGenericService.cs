namespace Business_Intelligence_ATMs_Focus.Service.Contrato
{
    public interface IGenericService<T> where T : class
    {
        Task<List<T>> Lista();
        Task<bool> Guardar(T modelo);
        Task<bool> Editar(T modelo);
        Task<bool> Eliminar(int id);
    }
}
