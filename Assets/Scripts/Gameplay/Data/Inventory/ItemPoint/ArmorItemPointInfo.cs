using UnityEngine;

namespace Gameplay.Data.Inventory.ItemPoint
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "armor_weapon_point_info", menuName = "Inventory/Infos/ItemPointInfos/ArmorPointInfo", order = 1)]
    public class ArmorItemPointInfo : ItemPointInfo
    {
        public override ItemPointType ItemPointType => ItemPointType.ArmorPoint;
    }
}