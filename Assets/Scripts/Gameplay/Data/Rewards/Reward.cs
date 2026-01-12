using Gameplay.Data.Inventory;
using UnityEngine;

namespace Gameplay.Data.Rewards
{
    [CreateAssetMenu(fileName = "reward_info", menuName = "RewardSystem/Infos/RewardInfo", order = 1)]
    public abstract class Reward : ScriptableObject
    {
        public InventoryItemType ItemType;
        public RewardType RewardType;
        public abstract BaseInventoryItemInfo GetInfo();
    }
}
