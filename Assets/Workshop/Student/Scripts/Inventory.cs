using System.Collections.Generic;
using UnityEngine;

namespace Solution
{
    public class Inventory : MonoBehaviour
    {
        public Dictionary<string, int> inventory = new Dictionary<string, int>();

        public void AddItem(string item, int amount)
        {
            if (inventory.ContainsKey(item))
            {
                inventory[item] += amount;
            }
            else
            {
                inventory.Add(item, amount);
            }

            Debug.Log("Added " + amount + " " + item +
                      ". Total: " + inventory[item]);
        }

        public void RemoveItem(string item, int amount)
        {
            if (inventory.ContainsKey(item))
            {
                inventory[item] -= amount;

                if (inventory[item] <= 0)
                {
                    inventory.Remove(item);
                }
            }
        }

        public bool HasItem(string item, int amount)
        {
            return inventory.ContainsKey(item) &&
                   inventory[item] >= amount;
        }

        public int GetItemCount(string item)
        {
            if (inventory.ContainsKey(item))
            {
                return inventory[item];
            }

            return 0;
        }

        public void PrintInventory()
        {
            Debug.Log("--- Inventory Content ---");

            if (inventory.Count == 0)
            {
                Debug.Log("Inventory is empty.");
                return;
            }

            foreach (var itemEntry in inventory)
            {
                Debug.Log("กระเป๋ามี "+ itemEntry.Key + ": " + itemEntry.Value);
            }
        }
    }
}