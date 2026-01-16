using System.Collections.Generic;
using Gameplay.Data.Interfaces;
using UnityEngine;
using Utilities;

namespace Gameplay.Data.Inventory
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "utility_info", menuName = "Inventory/Infos/UtilityInfo", order = 1)]
    public class UtilityInventoryInfo : InventoryItemInfo<InventoryItemIcon>
    {
        [field: SerializeField] protected override List<InventoryItemIcon> Icons { get; set; }
        public override InventoryItemID ID => itemType.ToID(UtilityName);
        public UtilityType UtilityType;
        public UtilityName UtilityName;
        public int Tier;
        public int UnlockLevel;
    }
}

