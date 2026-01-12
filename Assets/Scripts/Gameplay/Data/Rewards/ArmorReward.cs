using Gameplay.Data.Inventory;
using UnityEngine;

namespace Gameplay.Data.Rewards
{
    [CreateAssetMenu(fileName = "armor_reward_info", menuName = "RewardSystem/Infos/ArmorRewardInfo", order = 1)]
    public class ArmorReward : Reward
    {
        public ArmorType ArmorType;
        public ArmorName ArmorName;
        
        public override BaseInventoryItemInfo GetInfo()
        {
            throw new System.NotImplementedException();
        }
    }
}
