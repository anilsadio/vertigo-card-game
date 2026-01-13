using Gameplay.Data.Interfaces;
using Gameplay.Data.Inventory;
using Gameplay.Data.Utils;
using UnityEngine;

namespace Gameplay.Data.Rewards
{
    [CreateAssetMenu(fileName = "armor_reward_info", menuName = "RewardSystem/Infos/ArmorRewardInfo", order = 1)]
    public class ArmorReward : Reward
    {
        public override InventoryItemID ID => itemType.ToID(ArmorName);
        public ArmorType ArmorType;
        public ArmorName ArmorName;
    }
}
