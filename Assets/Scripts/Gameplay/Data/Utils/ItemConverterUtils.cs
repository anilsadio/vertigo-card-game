using System;
using System.Collections.Generic;
using Gameplay.Data.Interfaces;
using Gameplay.Data.Inventory;

namespace Gameplay.Data.Utils
{
    public static class ItemConverterUtils
    {
        private static Dictionary<(int, string), InventoryItemID> inventoryItemIDs = new();
        public static InventoryItemID ToID(this InventoryItemType itemType, string hash = default)
        {
            if (inventoryItemIDs.TryGetValue(((int)itemType, hash), out InventoryItemID id))
            {
                return id;
            }
            InventoryItemID target = new InventoryItemID(itemType, hash);
            inventoryItemIDs.Add(((int)itemType, hash), target);
            return target;
        }
    
        public static InventoryItemID ToID<TEnum>(this InventoryItemType itemType, TEnum enumValue) where TEnum : Enum
        {
            return itemType.ToID(enumValue.ToString());
        }
    }
}
