using System;
using System.Collections.Generic;

namespace MooonlightStore
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("--- INICIANDO ESCENARIOS DE PRUEBA ---");

            // 1. Creación de artículos
            Item espada = new Item("Espada Larga", 150, ItemCategory.Weapon);
            Item escudo = new Item("Escudo de Madera", 50, ItemCategory.Armor);
            Item pocion = new Item("Poción de Vida", 20, ItemCategory.Supply);
            Item anillo = new Item("Anillo Mágico", 200, ItemCategory.Accessory);

            // 2. Creación de tiendas (Cumpliendo Regla 4a: Nacen con inventario)
            Store tiendaArmas = new Store(espada, 2);
            tiendaArmas.AddItem(escudo, 1);
            tiendaArmas.AddItem(pocion, 10);

            Store tiendaMagia = new Store(anillo, 1); // Segunda tienda

            // 3. Creación de personajes
            Player heroe = new Player(500);
            Console.WriteLine($"Héroe creado con {heroe.Gold} de oro.\n");

            // 4. Compra de objetos (Primera tienda)
            Console.WriteLine("Intentando comprar 1 espada y 3 pociones en la Tienda de Armas...");
            var compraTienda1 = new Dictionary<Item, int>
            {
                { espada, 1 },
                { pocion, 3 }
            };

            if (tiendaArmas.SellItems(compraTienda1, heroe))
                Console.WriteLine("¡Compra en Tienda de Armas exitosa!\n");
            else
                Console.WriteLine("La compra falló en la Tienda de Armas.\n");

            // Escenario 4a: Compra de objetos en MÚLTIPLES tiendas con el MISMO personaje
            Console.WriteLine("Intentando comprar 1 anillo en la Tienda de Magia...");
            var compraTienda2 = new Dictionary<Item, int> { { anillo, 1 } };

            if (tiendaMagia.SellItems(compraTienda2, heroe))
                Console.WriteLine("¡Compra en Tienda de Magia exitosa!\n");
            else
                Console.WriteLine("La compra falló en la Tienda de Magia.\n");

            // 5. Actualización de inventario de jugador
            Console.WriteLine("--- INVENTARIO FINAL DEL HÉROE ---");
            Console.WriteLine($"Oro restante: {heroe.Gold}");

            Console.WriteLine("\nEquipamiento:");
            foreach (var equip in heroe.Equipment)
            {
                Console.WriteLine($"- {equip.Key.Name}: {equip.Value}");
            }

            Console.WriteLine("\nSuministros:");
            foreach (var supply in heroe.Supplies)
            {
                Console.WriteLine($"- {supply.Key.Name}: {supply.Value}");
            }

            Console.ReadLine();
        }
    }
}