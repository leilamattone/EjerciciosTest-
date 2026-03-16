using EjerciciosTest.Pages;
using EjerciciosTest.Test;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Collections.ObjectModel;

namespace EjerciciosTest._03.Test
{
    [TestClass]
    public class Ejercicio14Test : Test_Base
    {
        [TestMethod]
        //Agregar Múltiples Productos y Verificar el Carrito
        public void Test_AgregarMultiplesProductos()
        {

            /* Crear un método privado AddToCart(int productId) que navega y agrega un producto.

               Agregar el iPhone (product_id=40) usando el método helper.

               Agregar el MacBook (product_id=43) usando el método helper.

               Navegar a /index.php?route=checkout/cart.

               Verificar que hay exactamente 2 filas en la tabla del carrito.*/

            // ==================== ARRANGE ====================
            Console.WriteLine($"✓ Página principal cargada automáticamente: {Driver?.Url}");

            // Crear instancias de SearchPage, Product y CartPage
            var searchPage = new SearchPage(Driver);
            var productPage = new Product(Driver);
            var cartPage = new CartPage(Driver);

            // ==================== ACT ====================
            // Paso 1: Buscar "Mac" y hacer click en buscar
            searchPage.SearchProduct("Mac");
            Console.WriteLine("✓ Búsqueda de 'Mac' realizada");
            Thread.Sleep(1000);

            // Paso 2: Obtener la lista de productos del resultado de búsqueda
            ReadOnlyCollection<IWebElement> productos = productPage.GetListProducts();
            Console.WriteLine($"✓ Cantidad de productos encontrados: {productos.Count}");

            // Paso 3: Si hay productos, mostrar la lista e agregar el primero al carrito
            if (productos.Count > 0)
            {
                // Mostrar todos los productos encontrados
                Console.WriteLine("\n📦 Productos encontrados:");
                for (int i = 0; i < productos.Count; i++)
                {
                    Console.WriteLine($"  {i + 1}. {productos[i].Text}");
                }

                // Agregar el primer producto al carrito
                string tercerProducto = productos[2].Text;
                Console.WriteLine($"\n✓ Agregando al carrito el primer producto: '{tercerProducto}'");
                productPage.ClickBtnAddCart();
                Thread.Sleep(1000);
                Console.WriteLine($"✓ Producto '{tercerProducto}' agregado al carrito");
            }
            else
            {
                Console.WriteLine("⚠️ No se encontraron productos para 'Mac'");
            }

            // Paso 1: Buscar "Phone" y hacer click en buscar
            searchPage.SearchProduct("Phone");
            Console.WriteLine("✓ Búsqueda de 'Phone' realizada");
            Thread.Sleep(1000);

            // Paso 2: Obtener la lista de productos del resultado de búsqueda
            ReadOnlyCollection<IWebElement> productos2 = productPage.GetListProducts();
            Console.WriteLine($"✓ Cantidad de productos encontrados: {productos2.Count}");

            // Paso 3: Si hay productos, mostrar la lista e agregar el primero al carrito
            if (productos2.Count > 0)
            {
                // Mostrar todos los productos encontrados
                Console.WriteLine("\n📦 Productos encontrados:");
                for (int i = 0; i < productos2.Count; i++)
                {
                    Console.WriteLine($"  {i + 1}. {productos2[i].Text}");
                }

                // Agregar el primer producto al carrito
                string primerProducto = productos2[0].Text;
                Console.WriteLine($"\n✓ Agregando al carrito el primer producto: '{primerProducto}'");
                productPage.ClickBtnAddCart();
                Thread.Sleep(1000);
                Console.WriteLine($"✓ Producto '{primerProducto}' agregado al carrito");
            }
            else
            {
                Console.WriteLine("⚠️ No se encontraron productos para 'Tablet'");
            }

            // Paso 4: Mostrar productos del carrito (MostrarProductosCarrito abre el dropdown internamente)
            cartPage.MostrarProductosCarrito();


        }
    }
}
