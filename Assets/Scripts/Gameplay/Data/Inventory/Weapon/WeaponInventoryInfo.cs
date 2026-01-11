using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Data.Inventory.Weapon
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "primary_weapon_point_info",
        menuName = "Inventory/Infos/WeaponPointInfos/PrimaryWeaponPointInfo", order = 0)]
    public abstract class WeaponInventoryInfo : InventoryItemInfo<InventoryItemIcon>
    {
        [field: SerializeField] protected override List<InventoryItemIcon> Icons { get; set; }
        protected abstract WeaponCategory WeaponCategory { get; }
        public int Tier;
        public int UnlockPoint;
        public int UnlockLevel;
    }
}