using System;
using System.Collections.Generic;
using Gameplay.Data.Interfaces;
using Gameplay.Data.Inventory;
using UnityEngine;

namespace Gameplay.Data.Utils
{
    public static class CustomUtilities
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
        
        public static bool TryFindInRange<T>(this List<T> list, int startIndex, int endIndex, Predicate<T> predicate, out int index)
        {
            startIndex = Mathf.Max(0, startIndex);
            endIndex = Mathf.Min(list.Count - 1, endIndex);

            for (int i = startIndex; i <= endIndex; i++)
            {
                if (predicate(list[i]))
                {
                    index = i;
                    return true;
                }
            }

            index = 0;
            return false;
        }
    }
}
