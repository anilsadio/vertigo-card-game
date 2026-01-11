using Gameplay.Data.Inventory.Weapon;
using UnityEngine;

namespace Gameplay.Data.Inventory.Weapon
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "secondary_weapon_info", menuName = "Inventory/Infos/WeaponInfos/SecondaryWeaponInfo", order = 0)]
    public class SecondaryWeaponInventoryInfo : WeaponInventoryInfo
    {
        protected override WeaponCategory WeaponCategory => WeaponCategory.Secondary;
        public SecondaryWeaponType SecondaryWeaponType;
    }
}