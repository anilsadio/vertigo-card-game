using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Data.Inventory.Currency
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "utility_info", menuName = "Inventory/Infos/UtilityInfo", order = 1)]
    public class UtilityInventoryInfo : InventoryItemInfo<InventoryItemIcon>
    {
        [field: SerializeField] protected override List<InventoryItemIcon> Icons { get; set; }
        public UtilityType UtilityType;
        public int Tier;
        public int UnlockLevel;
    }
}

