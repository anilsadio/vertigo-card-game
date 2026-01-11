using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Data.Inventory.ItemPoint
{
    [System.Serializable]
    public abstract class ItemPointInfo : InventoryItemInfo<InventoryItemIcon>
    {
        [field: SerializeField] protected override List<InventoryItemIcon> Icons { get; set; }
        public abstract ItemPointType ItemPointType { get; }
    }
}