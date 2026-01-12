using Gameplay.Data.Inventory;
using UnityEngine;

namespace Gameplay.Data.Rewards
{
    [CreateAssetMenu(fileName = "currency_reward_info", menuName = "RewardSystem/Infos/CurrencyRewardInfo", order = 1)]
    public class CurrencyReward : Reward
    {
        public CurrencyType CurrencyType;
        
        public override BaseInventoryItemInfo GetInfo()
        {
            throw new System.NotImplementedException();
        }
    }
}
