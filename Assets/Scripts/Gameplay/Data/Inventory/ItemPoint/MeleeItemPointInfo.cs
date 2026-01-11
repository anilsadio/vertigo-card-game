using UnityEngine;

namespace Gameplay.Data.Inventory.ItemPoint
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "melee_weapon_point_info", menuName = "Inventory/Infos/ItemPointInfos/MeleeWeaponPointInfo", order = 0)]
    public class MeleeItemPointInfo : ItemPointInfo
    {
        public override ItemPointType ItemPointType => ItemPointType.WeaponPoint;
        public MeleeWeaponType MeleeWeaponType;
    }
}
