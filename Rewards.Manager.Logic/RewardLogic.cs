using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rewards.Manager.LogicContracts;
using Rewards.Manager.DalContracts;
using Rewards.Manager.Entities;

namespace Rewards.Manager.Logic
{
    public class RewardLogic : IRewardLogic
    {
        private IRewardDao _rewardDao;
        public RewardLogic(IRewardDao rewardDao)
        {
            _rewardDao = rewardDao;
        }

        // метод получения списка наград
        public Reward[] GetPersonAll(int id)
        {
            return _rewardDao.GetPersonAll(id).ToArray();

           
        }

        // сохранение награды
        public Reward Save(Reward reward)
        {
            if (_rewardDao.Add(reward))
            {
                return reward;
            }
            else
                throw new InvalidOperationException("Error on reward saving!");
        }
    }

}
