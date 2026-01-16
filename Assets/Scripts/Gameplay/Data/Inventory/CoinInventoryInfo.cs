using System.Collections.Generic;
using Gameplay.Data.Interfaces;
using UnityEngine;
using Utilities;

namespace Gameplay.Data.Inventory
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "CoinInfo", menuName = "Inventory/Infos/CoinInfo", order = 0)]
    public class CoinInventoryInfo : InventoryItemInfo<InventoryItemIcon>
    {
        public override InventoryItemID ID => itemType.ToID(CurrencyType);
        [field: SerializeField] protected override List<InventoryItemIcon> Icons { get; set; }
        public CurrencyType CurrencyType = CurrencyType.Coin;
        
    }
}
