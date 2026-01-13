using System.Collections.Generic;
using Gameplay.Data.Interfaces;
using Gameplay.Data.Inventory;
using Gameplay.Data.Utils;
using UnityEngine;

namespace Gameplay.Data
{
    [CreateAssetMenu(fileName = "fail_bomb_info", menuName = "Inventory/Infos/FailBombInfo", order = 1)]
    public class FailBombInventoryInfo : InventoryItemInfo<InventoryItemIcon>
    {
        [field: SerializeField] protected override List<InventoryItemIcon> Icons { get; set; }
        public override InventoryItemID ID => itemType.ToID(ItemName);
    }
}
