using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rewards.Manager.Entities;

namespace Rewards.Manager.DalContracts
{
    /// <summary>
    /// интерфейс получения данных для Medal
    /// </summary>
    public interface IMedalDao
    {
        bool Add(Medal medal);
        IEnumerable<Medal> GetAll();
        Medal GetById(int id);
        bool Delete(int id);
    }
}
