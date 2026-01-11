using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Data.Inventory
{
    [System.Serializable]
    public abstract class WeaponInventoryInfo : InventoryItemInfo<InventoryItemIcon>
    {
        [field: SerializeField] protected override List<InventoryItemIcon> Icons { get; set; }
        protected abstract WeaponCategory WeaponCategory { get; }
        public int Tier;
        public int UnlockPoint;
        public int UnlockLevel;
    }
}