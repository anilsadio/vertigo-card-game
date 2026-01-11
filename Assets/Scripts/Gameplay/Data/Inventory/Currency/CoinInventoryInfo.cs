using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Data.Inventory.Currency
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "CoinInfo", menuName = "Inventory/Infos/CoinInfo", order = 1)]
    public class CoinInventoryInfo : InventoryItemInfo<InventoryItemIcon>
    {
        [field: SerializeField] protected override List<InventoryItemIcon> Icons { get; set; }
        public CurrencyType CurrencyType = CurrencyType.Coin;
        
    }
}
