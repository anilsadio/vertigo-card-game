using System.Collections.Generic;
using Gameplay.Data.Interfaces;
using Gameplay.Data.Utils;
using UnityEngine;

namespace Gameplay.Data.Inventory.Currency
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "money_info", menuName = "Inventory/Infos/MoneyInfo", order = 1)]
    public class MoneyInventoryInfo : InventoryItemInfo<InventoryItemIcon>
    {
        public override InventoryItemID ID => itemType.ToID(CurrencyType);
        [field: SerializeField] protected override List<InventoryItemIcon> Icons { get; set; }
        public CurrencyType CurrencyType = CurrencyType.Money;
        
    }
}

