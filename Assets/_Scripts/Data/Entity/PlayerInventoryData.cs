using System.Collections.Generic;
using Game.Entites;
using UnityEngine;

namespace Game.Data
{
    public class PlayerInventoryData : MonoBehaviour
    {
        public List<Item> InventoryItems;
        public Dictionary<EntityEquipSlots, Item> _equippedItems;
    }
}
