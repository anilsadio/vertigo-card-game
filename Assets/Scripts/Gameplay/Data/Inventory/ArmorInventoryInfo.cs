using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Data.Inventory
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "armor_info", menuName = "Inventory/Infos/ArmorInfo", order = 0)]
    public abstract class ArmorInventoryInfo : InventoryItemInfo<InventoryItemIcon>
    {
        [field: SerializeField] protected override List<InventoryItemIcon> Icons { get; set; }
        protected abstract ArmorType ArmorType { get; }
        
        public int Tier;
        public int UnlockPointAmount;
        public int UnlockLevel;
    }
}