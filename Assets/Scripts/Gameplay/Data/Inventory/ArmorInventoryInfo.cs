using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Data.Inventory
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "armor_info", menuName = "Inventory/Infos/ArmorInfo", order = 0)]
    public class ArmorInventoryInfo : InventoryItemInfo<InventoryItemIcon>
    {
        [field: SerializeField] protected override List<InventoryItemIcon> Icons { get; set; }
        
        public int Tier;
        public int UnlockPointAmount;
        public int UnlockLevel;
        public ArmorType ArmorType;
    }
}