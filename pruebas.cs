using NUnit.Framework;
using System.Collections.Generic;
using RPGStore;

namespace RPGStoreTests
{
    public class StoreTests
    {
        [Test]
        public void Articulos()
        {
            var i1 = new Item("Espada", 100, ItemCategory.Weapon);
            var i2 = new Item("Poción", 10, ItemCategory.Supply);
            Assert.AreEqual("Espada", i1.Name);
            Assert.Throws<System.ArgumentException>(() => new Item("", 10, ItemCategory.Supply));
        }

        [Test]
        public void TiendaBasica()
        {
            var t = new Store();
            var e = new Item("Espada", 100, ItemCategory.Weapon);
            t.AddItem(e, 1);
            Assert.AreEqual(1, t.Inventory[e]);
        }

        [Test]
        public void JugadorGold()
        {
            var p = new Player(50);
            Assert.AreEqual(50, p.Gold);
            p.Gold = 10;
            Assert.AreEqual(10, p.Gold);
        }

        [Test]
        public void CompraOk()
        {
            var t = new Store();
            var e = new Item("Espada", 100, ItemCategory.Weapon);
            t.AddItem(e, 2);
            var p = new Player(200);
            var buy = new Dictionary<Item, int> { { e, 1 } };
            var ok = t.SellItems(buy, p);
            Assert.IsTrue(ok);
            Assert.AreEqual(1, t.Inventory[e]);
            Assert.AreEqual(100, p.Gold);
        }

        [Test]
        public void CompraSinStock()
        {
            var t = new Store();
            var e = new Item("Espada", 100, ItemCategory.Weapon);
            t.AddItem(e, 1);
            var p = new Player(200);
            var buy = new Dictionary<Item, int> { { e, 2 } };
            var ok = t.SellItems(buy, p);
            Assert.IsFalse(ok);
        }

        [Test]
        public void CompraSinGold()
        {
            var t = new Store();
            var e = new Item("Espada", 100, ItemCategory.Weapon);
            t.AddItem(e, 1);
            var p = new Player(50);
            var buy = new Dictionary<Item, int> { { e, 1 } };
            var ok = t.SellItems(buy, p);
            Assert.IsFalse(ok);
        }

        [Test]
        public void InventarioJugador()
        {
            var t = new Store();
            var e = new Item("Espada", 100, ItemCategory.Weapon);
            var c = new Item("Poción", 10, ItemCategory.Supply);
            t.AddItem(e, 1);
            t.AddItem(c, 2);
            var p = new Player(200);
            var buy = new Dictionary<Item, int> { { e, 1 }, { c, 2 } };
            t.SellItems(buy, p);
            Assert.AreEqual(1, p.Equipment[e]);
            Assert.AreEqual(2, p.Supplies[c]);
        }

        [Test]
        public void DuplicadoPrecio()
        {
            var t = new Store();
            var e = new Item("Espada", 100, ItemCategory.Weapon);
            t.AddItem(e, 1);
            var e2 = new Item("Espada", 120, ItemCategory.Weapon);
            Assert.Throws<System.InvalidOperationException > (() => t.AddItem(e2, 1));
        }
    }
}
