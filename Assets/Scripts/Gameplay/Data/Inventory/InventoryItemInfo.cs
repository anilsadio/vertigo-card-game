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
    }
    
    [System.Serializable]
    public abstract class BaseInventoryItemInfo : ScriptableObject
    {
        public virtual InventoryItemID ID => itemType.ToID();
        public InventoryItemType itemType;
    }
}