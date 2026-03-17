using System;
using System.Collections.Generic;

namespace RPGStore
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

            // 2. Creación de tiendas
            Store tienda = new Store();
            tienda.AddItem(espada, 2);
            tienda.AddItem(escudo, 1);
            tienda.AddItem(pocion, 10);

            // 3. Creación de personajes
            Player heroe = new Player(500);
            Console.WriteLine($"Héroe creado con {heroe.Gold} de oro.\n");

            // 4. Compra de objetos
            Console.WriteLine("Intentando comprar 1 espada y 3 pociones...");
            var carritoDeCompras = new Dictionary<Item, int>
            {
                { espada, 1 },
                { pocion, 3 }
            };

            bool compraExitosa = tienda.SellItems(carritoDeCompras, heroe);

            if (compraExitosa)
            {
                Console.WriteLine("¡Compra exitosa!\n");
            }
            else
            {
                Console.WriteLine("La compra falló (falta de oro o de stock).\n");
            }


            Console.WriteLine("--- INVENTARIO DEL HÉROE ---");
            Console.WriteLine($"Oro restante: {heroe.Gold}");

            Console.WriteLine("Equipamiento:");
            foreach (var equip in heroe.Equipment)
            {
                Console.WriteLine($"- {equip.Key.Name}: {equip.Value}");
            }

            Console.WriteLine("Suministros:");
            foreach (var supply in heroe.Supplies)
            {
                Console.WriteLine($"- {supply.Key.Name}: {supply.Value}");
            }

            // Pausar la consola para poder leer el resultado
            Console.ReadLine();
        }
    }
}