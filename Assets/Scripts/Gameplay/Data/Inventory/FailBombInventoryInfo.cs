using System.Collections.Generic;
using Gameplay.Data.Interfaces;
using UnityEngine;
using Utilities;

namespace Gameplay.Data.Inventory
{
    [CreateAssetMenu(fileName = "fail_bomb_info", menuName = "Inventory/Infos/FailBombInfo", order = 1)]
    public class FailBombInventoryInfo : InventoryItemInfo<InventoryItemIcon>
    {
        [field: SerializeField] protected override List<InventoryItemIcon> Icons { get; set; }
        public override InventoryItemID ID => itemType.ToID(ItemName);
    }
}
