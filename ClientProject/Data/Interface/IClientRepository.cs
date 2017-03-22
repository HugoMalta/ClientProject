using ClientProject.Models;

namespace ClientProject.Data.Interface
{
    /// <summary>
    /// Gets all the standard methods for a repository and implements the specific methods.
    /// </summary>
    public interface IClientRepository : IRepositoryBase<ClientModel>
    {
        bool DeleteByCpf(string cpf);
    }
}
