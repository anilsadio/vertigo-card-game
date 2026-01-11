using UnityEngine;

namespace Gameplay.Data.Inventory.ItemPoint
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "secondary_weapon_point_info", menuName = "Inventory/Infos/ItemPointInfos/SecondaryWeaponPointInfo", order = 0)]
    public class SecondaryItemPointInfo : ItemPointInfo
    {
        public override ItemPointType ItemPointType => ItemPointType.WeaponPoint;
        public SecondaryWeaponType SecondaryWeaponType;
    }
}