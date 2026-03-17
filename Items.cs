using System;

namespace RPGStore
{
    public class Item : IEquatable<Item>
    {
        public string Name { get; }
        public int Price { get; }
        public ItemCategory Category { get; }

        public Item(string name, int price, ItemCategory category)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("El nombre no puede estar vacÃ­o");
            if (price <= 0)
                throw new ArgumentException("El precio debe ser positivo");
            Name = name;
            Price = price;
            Category = category;
        }

        public bool Equals(Item other)
        {
            if (other == null) return false;
            return Name == other.Name && Price == other.Price && Category == other.Category;
        }

        public override bool Equals(object obj) => Equals(obj as Item);
        public override int GetHashCode() => HashCode.Combine(Name, Price, Category);
    }
}