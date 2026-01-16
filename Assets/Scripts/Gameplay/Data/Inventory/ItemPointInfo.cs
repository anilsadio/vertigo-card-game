using System.Collections.Generic;
using Gameplay.Data.Interfaces;
using UnityEngine;
using Utilities;

namespace Gameplay.Data.Inventory
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "item_point_info", menuName = "Inventory/Infos/ItemPointInfo", order = 1)]
    public class ItemPointInfo : InventoryItemInfo<InventoryItemIcon>
    {
        [field: SerializeField] protected override List<InventoryItemIcon> Icons { get; set; }
        public override InventoryItemID ID => itemType.ToID(ItemPointType);
        public ItemPointType ItemPointType;
    }
}