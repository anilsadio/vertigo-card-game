using Gameplay.Data.Interfaces;
using Gameplay.Data.Inventory;
using Gameplay.Data.Utils;
using UnityEngine;

namespace Gameplay.Data.Rewards
{
    [CreateAssetMenu(fileName = "point_reward_info", menuName = "RewardSystem/Infos/ItemPointRewardInfo", order = 1)]
    public class ItemPointReward : Reward
    {
        public override InventoryItemID ID => itemType.ToID(ItemPointType);
        public ItemPointType ItemPointType;
    }
}
