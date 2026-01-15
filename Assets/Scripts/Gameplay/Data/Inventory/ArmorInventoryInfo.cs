using System.Collections.Generic;
using Gameplay.Data.Interfaces;
using Gameplay.Data.Utils;
using UnityEngine;

namespace Gameplay.Data.Inventory
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "armor_info", menuName = "Inventory/Infos/ArmorInfo", order = 0)]
    public class ArmorInventoryInfo : InventoryItemInfo<InventoryItemIcon>
    {
        [field: SerializeField] protected override List<InventoryItemIcon> Icons { get; set; }
        public override InventoryItemID ID => itemType.ToID(ArmorName);
        public ArmorName ArmorName;
        public int Tier;
        public int UpdatePointAmount;
        public int UnlockLevel;
        public ArmorType ArmorType;
    }
}