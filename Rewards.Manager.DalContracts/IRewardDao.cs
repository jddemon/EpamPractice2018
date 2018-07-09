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
    public interface IRewardDao
    {
        bool Add(Reward reward);
        IEnumerable<Reward> GetPersonAll(int id);
    }
}
