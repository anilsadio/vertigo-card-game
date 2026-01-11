using UnityEngine;

namespace Gameplay.Data.Inventory.ItemPoint
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "primary_weapon_point_info", menuName = "Inventory/Infos/ItemPointInfos/PrimaryWeaponPointInfo", order = 0)]
    public class PrimaryItemPointInfo : ItemPointInfo
    {
        public override ItemPointType ItemPointType => ItemPointType.WeaponPoint;
        public PrimaryWeaponType PrimaryWeaponType;
    }
}