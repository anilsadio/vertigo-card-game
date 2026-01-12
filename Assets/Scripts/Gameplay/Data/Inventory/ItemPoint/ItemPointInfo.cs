using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Data.Inventory.ItemPoint
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "item_point_info", menuName = "Inventory/Infos/ItemPointInfo", order = 1)]
    public class ItemPointInfo : InventoryItemInfo<InventoryItemIcon>
    {
        [field: SerializeField] protected override List<InventoryItemIcon> Icons { get; set; }
        public ItemPointType ItemPointType;
    }
}