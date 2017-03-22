using ClientProject.Data.Interface;
using ClientProject.Models;
using System.Linq;

namespace ClientProject.Data.Repository
{
    /// <summary>
    /// Inherits methods from the base repository and implements the specific methods.
    /// </summary>
    public class ClientRepository : RepositoryBase<ClientModel>, IClientRepository
    {
        public bool DeleteByCpf(string cpf)
        {
            try
            {
                var clients = base.Db.Usuarios.Where(u => u.Cpf.Equals(cpf));
                base.Db.Usuarios.RemoveRange(clients);
                int registers = base.Db.SaveChanges();
                return registers > 0;
            }
            catch
            {
                return false;
            }
        }
    }
}