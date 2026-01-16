using Gameplay.Data.Interfaces;
using Gameplay.Data.Inventory;
using UnityEngine;
using Utilities;

namespace Gameplay.Data.Rewards
{
    [CreateAssetMenu(fileName = "currency_reward_info", menuName = "RewardSystem/Infos/CurrencyRewardInfo", order = 1)]
    public class CurrencyReward : Reward
    {
        public override InventoryItemID ID => itemType.ToID(CurrencyType);
        
        public CurrencyType CurrencyType;
    }
}
