using System.Collections.Generic;
using Gameplay.Data.Interfaces;
using Newtonsoft.Json;
using UnityEngine;

namespace Gameplay.Data.Inventory.InventorySaveSystem
{
    public static class InventoryService
    {
        private static Dictionary<InventoryItemID, int> runtimeInventoryData = new();
        private static List<InventoryData> inventoryDatas = new();

        
        public static void InitializeInventory()
        {
            inventoryDatas = PlayerPrefs.HasKey(PlayerPrefKeys.INVENTORY_DATA) ?
                JsonConvert.DeserializeObject<List<InventoryData>>(PlayerPrefs.GetString(PlayerPrefKeys.INVENTORY_DATA))
                    : new List<InventoryData>();
            
            Debug.Log("Inventory Loaded: " + JsonConvert.SerializeObject(inventoryDatas));
            
            foreach (var data in inventoryDatas)
            {
                var inventoryItemID = new InventoryItemID(data.itemType, data.hash);
                runtimeInventoryData.TryAdd(inventoryItemID, data.count);
            }
        }

        public static int GetAmountOfItem(InventoryItemID itemID)
        {
            return runtimeInventoryData.GetValueOrDefault(itemID, 0);
        }

        public static bool HasItem(InventoryItemID itemID)
        {
            return runtimeInventoryData.ContainsKey(itemID);
        }
        
        public static void SaveInventoryData()
        {
            inventoryDatas.Clear();
            foreach (var kv in runtimeInventoryData)
            {
                inventoryDatas.Add(new InventoryData
                {
                    itemType = kv.Key.type,
                    hash = kv.Key.hash,
                    count = kv.Value
                });
            }
            PlayerPrefs.SetString(PlayerPrefKeys.INVENTORY_DATA, JsonConvert.SerializeObject(inventoryDatas));
            
            Debug.Log("Inventory Saved: " + JsonConvert.DeserializeObject<List<InventoryData>>(PlayerPrefs.GetString(PlayerPrefKeys.INVENTORY_DATA)));
        }

        public static void AddItemToInventory(InventoryItemID itemID, int amount)
        {
            if (!runtimeInventoryData.TryAdd(itemID, amount))
            {
                runtimeInventoryData[itemID] += amount;
            }
        }
        
        public static void AddItemToInventoryAndSaveData(InventoryItemID itemID, int amount)
        {
            AddItemToInventory(itemID, amount);
            SaveInventoryData();
        }
    }
}