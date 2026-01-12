using Gameplay.Data.Inventory;
using UnityEngine;

namespace Gameplay.Data.Rewards
{
    [CreateAssetMenu(fileName = "utility_reward_info", menuName = "RewardSystem/Infos/UtilityRewardInfo", order = 1)]
    
    public class UtilityReward : Reward
    {
        public UtilityType UtilityType;
        public UtilityName UtilityName;

        public override BaseInventoryItemInfo GetInfo()
        {
            throw new System.NotImplementedException();
        }
    }
}
