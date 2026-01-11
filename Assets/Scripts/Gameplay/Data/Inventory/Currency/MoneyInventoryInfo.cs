using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Data.Inventory.Currency
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "money_info", menuName = "Inventory/Infos/MoneyInfo", order = 1)]
    public class MoneyInventoryInfo : InventoryItemInfo<InventoryItemIcon>
    {
        [field: SerializeField] protected override List<InventoryItemIcon> Icons { get; set; }
        public CurrencyType CurrencyType = CurrencyType.Money;
        
    }
}

