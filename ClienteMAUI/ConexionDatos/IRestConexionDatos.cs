using ClienteMAUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteMAUI.ConexionDatos
{
    public interface IRestConexionDatos
    {
        Task<List<Plato>> GetPlatosAsync();
        Task AddPlatoAsync(Plato plato);
        Task UpdatePlatoAsync(Plato plato);
        Task DeletePlatoAsync(int id);
    }
}
