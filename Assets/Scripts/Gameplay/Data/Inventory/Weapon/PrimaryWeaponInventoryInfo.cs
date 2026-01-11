using UnityEngine;

namespace Gameplay.Data.Inventory.Weapon
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "primary_weapon_info", menuName = "Inventory/Infos/WeaponInfos/PrimaryWeaponInfo", order = 0)]
    public class PrimaryWeaponInventoryInfo : WeaponInventoryInfo
    {
        protected override WeaponCategory WeaponCategory => WeaponCategory.Primary;
        public PrimaryWeaponType PrimaryWeaponType;
    }
}