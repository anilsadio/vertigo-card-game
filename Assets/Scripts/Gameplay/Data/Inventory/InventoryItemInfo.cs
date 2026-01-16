using System.Collections.Generic;
using Gameplay.Data.Interfaces;
using Gameplay.Data.Rewards;
using Sirenix.OdinInspector;
using UnityEngine;
using Utilities;
using Utilities.Pool;

namespace Gameplay.Data.Inventory
{
    [System.Serializable]
    public abstract class InventoryItemInfo<TIcon> : BaseInventoryItemInfo, IInventoryItemIDHolder
        where TIcon : InventoryItemIcon
    {
        protected abstract List<TIcon> Icons { get; set; }
        public override Sprite GetIcon(InventoryIconType iconType = InventoryIconType.GameIcon)
        {
            var _icon = Icons.Find(target => target.IconType == iconType);
            
            if (_icon == null && Icons.Count > 0) 
                return Icons[0].Icon;
            
            return _icon.Icon;
        }
    }
    
    [System.Serializable]
    public abstract class BaseInventoryItemInfo : ScriptableObject
    {
        public virtual InventoryItemID ID => itemType.ToID(ItemName);
        public InventoryItemType itemType;
        public string ItemName;
        public InventoryItemConsumeType InventoryItemConsumeType;
        public ObjectType CardUIItemType;
        
        [ShowIf(nameof(IsFallBackRewardNeeded))]
        public Reward FallbackReward;//If the nonconsumable inventory item has been claimed before, you should convert the inventory item to that type of inventory item. 

        public abstract Sprite GetIcon(InventoryIconType iconType = InventoryIconType.GameIcon);

        public virtual bool IsFallBackRewardNeeded()
        {
            return InventoryItemConsumeType == InventoryItemConsumeType.NonConsumable;;
        }
    }
}