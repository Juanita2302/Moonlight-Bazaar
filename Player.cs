using System.Collections.Generic;

namespace MooonlightStore
{
    public class Player
    {
        public int Gold { get; set; }
        public Dictionary<Item, int> Equipment { get; } = new();
        public Dictionary<Item, int> Supplies { get; } = new();

        public Player(int gold)
        {
            Gold = gold;
        }

        public void AddItems(Dictionary<Item, int> items)
        {
            foreach (var kvp in items)
            {
                var item = kvp.Key;
                var qty = kvp.Value;
                if (item.Category == ItemCategory.Supply)
                {
                    if (Supplies.ContainsKey(item))
                        Supplies[item] += qty;
                    else
                        Supplies[item] = qty;
                }
                else
                {
                    if (Equipment.ContainsKey(item))
                        Equipment[item] += qty;
                    else
                        Equipment[item] = qty;
                }
            }
        }
    }
}