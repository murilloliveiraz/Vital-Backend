namespace Application.Services.Interfaces
{
    /// <summary>
    /// Interface Genérica para criação de serviços do tipo CRUD
    /// </summary>
    /// <typeparam name="RQ">Contrato de Request</typeparam>
    /// <typeparam name="RS">Contrato de Response</typeparam>
    /// <typeparam name="I">Tipo do Id</typeparam>
    public interface IService<RQ, RS, I> where RQ : class
    {
        Task<IEnumerable<RS>> Get();
        Task<RS> GetById(I id);
        Task<RS> Create(RQ model);
        Task<RS> Update(I id, RQ model);
        Task Delete(I id);
    }
}