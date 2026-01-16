using System;
using Gameplay.Data.Inventory;

namespace Gameplay.Data.Interfaces
{
    public interface IInventoryItemIDHolder
    {
        InventoryItemID ID { get; }
    }
    
    public struct InventoryItemID
    {
        public InventoryItemType type;
        public string hash;
        
        public InventoryItemID(InventoryItemType type, string hash = default)
        {
            this.type = type;
            this.hash = hash;
        }

        public bool Equals(object other)
        {
            if (other is InventoryItemID itemID)
            {
                return itemID.type == type && itemID.hash == hash;
            }
            return false;
        }

        public int GetHashCode()
        {
            return HashCode.Combine((int) type, hash);
        }
    }
}