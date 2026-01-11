using UnityEngine;

namespace Gameplay.Data.Inventory.Weapon
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "melee_weapon_info", menuName = "Inventory/Infos/WeaponInfos/MeleeWeaponInfo", order = 0)]
    public class MeleeWeaponInventoryInfo : WeaponInventoryInfo
    {
        protected override WeaponCategory WeaponCategory => WeaponCategory.Melee;
        public MeleeWeaponType MeleeWeaponType;
    }
}