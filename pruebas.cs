using NUnit.Framework;
using System.Collections.Generic;
//using RPGStore;

namespace MooonlightStore
{
    public class StoreTests
    {
        [Test]
        public void Articulos()
        {
            var i1 = new Item("Espada", 100, ItemCategory.Weapon);
            Assert.AreEqual("Espada", i1.Name);
            Assert.Throws<System.ArgumentException>(() => new Item("", 10, ItemCategory.Supply));
        }

        [Test]
        public void TiendaBasica()
        {
            var e = new Item("Espada", 100, ItemCategory.Weapon);
            var t = new Store(e, 1); // Modificado para cumplir Regla 4a
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
            var e = new Item("Espada", 100, ItemCategory.Weapon);
            var t = new Store(e, 2); // Nace con 2 espadas
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
            var e = new Item("Espada", 100, ItemCategory.Weapon);
            var t = new Store(e, 1); // Solo hay 1
            var p = new Player(200);
            var buy = new Dictionary<Item, int> { { e, 2 } }; // Intenta comprar 2

            var ok = t.SellItems(buy, p);

            Assert.IsFalse(ok);
        }

        [Test]
        public void CompraSinGold()
        {
            var e = new Item("Espada", 100, ItemCategory.Weapon);
            var t = new Store(e, 1);
            var p = new Player(50); // Oro insuficiente
            var buy = new Dictionary<Item, int> { { e, 1 } };

            var ok = t.SellItems(buy, p);

            Assert.IsFalse(ok);
        }

        [Test]
        public void InventarioJugador()
        {
            var e = new Item("Espada", 100, ItemCategory.Weapon);
            var c = new Item("Poción", 10, ItemCategory.Supply);
            var t = new Store(e, 1);
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
            var e = new Item("Espada", 100, ItemCategory.Weapon);
            var t = new Store(e, 1);
            var e2 = new Item("Espada", 120, ItemCategory.Weapon);

            Assert.Throws<System.InvalidOperationException>(() => t.AddItem(e2, 1));
        }

        // NUEVO TEST: Escenario 4a (Múltiples tiendas, mismo jugador)
        [Test]
        public void CompraMultiplesTiendas()
        {
            var espada = new Item("Espada", 100, ItemCategory.Weapon);
            var pocion = new Item("Poción", 10, ItemCategory.Supply);

            var tiendaArmas = new Store(espada, 1);
            var tiendaMagia = new Store(pocion, 5);

            var jugador = new Player(200);

            // Mismo jugador compra en ambas tiendas
            tiendaArmas.SellItems(new Dictionary<Item, int> { { espada, 1 } }, jugador);
            tiendaMagia.SellItems(new Dictionary<Item, int> { { pocion, 2 } }, jugador);

            // Verificamos descuentos y agrupación correcta
            Assert.AreEqual(80, jugador.Gold); // 200 - 100 - 20 = 80
            Assert.AreEqual(1, jugador.Equipment[espada]);
            Assert.AreEqual(2, jugador.Supplies[pocion]);
        }
    }
}