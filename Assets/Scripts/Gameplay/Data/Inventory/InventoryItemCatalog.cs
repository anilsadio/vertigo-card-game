using System.Collections.Generic;
using Gameplay.Data.Interfaces;
using UnityEngine;

namespace Gameplay.Data.Inventory
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "InventoryItemCatalog", menuName = "Inventory/InventoryItemCatalog", order = 1)]
    internal class InventoryItemCatalog : SingletonScriptableObject<InventoryItemCatalog>
    {
        [SerializeField] private List<BaseInventoryItemInfo> items;
        
        private Dictionary<InventoryItemID, BaseInventoryItemInfo> itemsDictionary;
        public Dictionary<InventoryItemID, BaseInventoryItemInfo> ItemsDictionary
        {
            get
            {
                if (itemsDictionary == null)
                {
                    itemsDictionary = new();
                    foreach (var _item in items)
                    {
                        itemsDictionary.Add(_item.ID, _item);
                    }
                }
                return itemsDictionary;
            }
        }
        
        private bool InfoExist(InventoryItemID id)
        {
            return ItemsDictionary.ContainsKey(id);
        }

        public BaseInventoryItemInfo GetInfo(InventoryItemID id)
        {
            return InfoExist(id) ? ItemsDictionary[id] : null;
        }
    }
}
