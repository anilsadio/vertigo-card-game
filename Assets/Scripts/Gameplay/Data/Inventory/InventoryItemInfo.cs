using System.Collections.Generic;
using Gameplay.Data.Interfaces;
using Gameplay.Data.Utils;
using UnityEngine;

namespace Gameplay.Data.Inventory
{
    [System.Serializable]
    public abstract class InventoryItemInfo<TIcon> : BaseInventoryItemInfo, IInventoryItemIDHolder
        where TIcon : InventoryItemIcon
    {
        protected abstract List<TIcon> Icons { get; set; }
        public override Sprite GetIcon(InventoryIconType iconType)
        {
            var icon = Icons.Find(target => target.IconType == iconType);
            
            if (icon == null && Icons.Count > 0) 
                return Icons[0].Icon;
            
            return icon.Icon;
        }
    }
    
    [System.Serializable]
    public abstract class BaseInventoryItemInfo : ScriptableObject
    {
        public virtual InventoryItemID ID => itemType.ToID(ItemName);
        public InventoryItemType itemType;
        public string ItemName;
        
        public abstract Sprite GetIcon(InventoryIconType iconType);
    }
}