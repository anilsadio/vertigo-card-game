using Gameplay.Data.Inventory;
using UnityEngine;

namespace Gameplay.Data.Rewards
{
    [CreateAssetMenu(fileName = "weapon_reward_info", menuName = "RewardSystem/Infos/WeaponRewardInfo", order = 1)]
    public class WeaponReward : Reward
    {
        public WeaponType WeaponType;
        public WeaponName WeaponName;

        public override BaseInventoryItemInfo GetInfo()
        {
            throw new System.NotImplementedException();
        }
    }
}
