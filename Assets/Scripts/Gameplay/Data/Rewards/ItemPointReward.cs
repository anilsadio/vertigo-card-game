using Gameplay.Data.Inventory;
using UnityEngine;

namespace Gameplay.Data.Rewards
{
    [CreateAssetMenu(fileName = "point_reward_info", menuName = "RewardSystem/Infos/ItemPointRewardInfo", order = 1)]
    public class ItemPointReward : Reward
    {
        public ItemPointType ItemPointType;
        
        public override BaseInventoryItemInfo GetInfo()
        {
            //Get info from catalog
            throw new System.NotImplementedException();
        }
    }
}
