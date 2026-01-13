using System.Collections.Generic;
using Gameplay.Data.Interfaces;
using Gameplay.Data.Utils;
using UnityEngine;

namespace Gameplay.Data.Inventory.Currency
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
