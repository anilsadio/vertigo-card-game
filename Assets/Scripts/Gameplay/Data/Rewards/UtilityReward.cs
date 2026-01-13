using Gameplay.Data.Interfaces;
using Gameplay.Data.Inventory;
using Gameplay.Data.Utils;
using UnityEngine;

namespace Gameplay.Data.Rewards
{
    [CreateAssetMenu(fileName = "utility_reward_info", menuName = "RewardSystem/Infos/UtilityRewardInfo", order = 1)]
    
    public class UtilityReward : Reward
    {
        public override InventoryItemID ID => itemType.ToID(UtilityName);
        
        public UtilityType UtilityType;
        public UtilityName UtilityName;
    }
}
