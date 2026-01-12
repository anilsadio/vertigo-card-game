using System.Collections.Generic;
using Gameplay.Data.Interfaces;
using Gameplay.Data.Utils;
using UnityEngine;

namespace Gameplay.Data.Inventory
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "weapon_info", menuName = "Inventory/Infos/WeaponInfo", order = 0)]
    public class WeaponInventoryInfo : InventoryItemInfo<InventoryItemIcon>
    {
        [field: SerializeField] protected override List<InventoryItemIcon> Icons { get; set; }
        public override InventoryItemID ID => itemType.ToID(WeaponName);
        public WeaponCategory WeaponCategory;
        public WeaponType WeaponType;
        public WeaponName WeaponName;
        public int Tier;
        public int UnlockPoint;
        public int UnlockLevel;
    }
}