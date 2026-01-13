using Gameplay.Data.Interfaces;
using Gameplay.Data.Inventory;
using Gameplay.Data.Utils;
using UnityEngine;

namespace Gameplay.Data.Rewards
{
    [CreateAssetMenu(fileName = "weapon_reward_info", menuName = "RewardSystem/Infos/WeaponRewardInfo", order = 1)]
    public class WeaponReward : Reward
    {
        public override InventoryItemID ID => itemType.ToID(WeaponName);
        public WeaponType WeaponType;
        public WeaponName WeaponName;
    }
}
