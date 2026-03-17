using System.Collections.Generic;

namespace RPGStore
{
    public class Store
    {
        private readonly Dictionary<Item, int> inventory = new();
        public IReadOnlyDictionary<Item, int> Inventory => inventory;

        public void AddItem(Item item, int quantity)
        {
            if (quantity < 0)
                throw new ArgumentException("La cantidad no puede ser negativa");
            foreach (var invItem in inventory.Keys)
            {
                if (invItem.Name == item.Name && invItem.Category == item.Category && invItem.Price != item.Price)
                    throw new InvalidOperationException("No se puede agregar el mismo artÃ­culo con diferente precio");
            }
            if (inventory.ContainsKey(item))
                inventory[item] += quantity;
            else
                inventory[item] = quantity;
        }

        public bool CanSell(Item item, int quantity)
        {
            return inventory.TryGetValue(item, out int stock) && stock >= quantity;
        }

        public bool SellItems(Dictionary<Item, int> itemsToBuy, Player player)
        {
            int total = 0;
            foreach (var kvp in itemsToBuy)
            {
                if (!CanSell(kvp.Key, kvp.Value))
                    return false;
                total += kvp.Key.Price * kvp.Value;
            }
            if (player.Gold < total)
                return false;
            foreach (var kvp in itemsToBuy)
                inventory[kvp.Key] -= kvp.Value;
            player.Gold -= total;
            player.AddItems(itemsToBuy);
            return true;
        }
    }
}