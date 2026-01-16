using Gameplay.Data.Interfaces;
using Gameplay.Data.Inventory;
using Gameplay.Data.Utils;
using UnityEngine;
using Utilities.Pool;

namespace Gameplay.Data.Rewards
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "reward_info", menuName = "RewardSystem/Infos/RewardInfo", order = 1)]
    public abstract class Reward : BaseReward, IInventoryItemIDHolder
    {
        public virtual BaseInventoryItemInfo GetInventoryInfo()
        {
            return InventoryItemCatalog.instance.GetInfo(ID);
        }       
        
        public virtual Sprite GetInventoryIcon(InventoryIconType iconType = InventoryIconType.GameIcon)
        {
            return InventoryItemCatalog.instance.GetInfo(ID).GetIcon(iconType);
        }
    }
    
    [System.Serializable]
    public abstract class BaseReward : ScriptableObject
    {
        public virtual InventoryItemID ID => itemType.ToID(RewardType);
        public RewardType RewardType;
        public InventoryItemType itemType;
    }
}
