using EjerciciosTest.Pages;
using EjerciciosTest.Test;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Collections.ObjectModel;

namespace EjerciciosTest.Test
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
                Console.WriteLine($"\n✓ Agregando al carrito: '{tercerProducto}'");
                productPage.ClickBtnAddCart();
                Thread.Sleep(1000);
                bool exitoMac = cartPage.EsperarYVerificarAlertaExito();
                Assert.IsTrue(exitoMac, $"No apareció el alert de éxito al agregar '{tercerProducto}'");
                Console.WriteLine($"✓ Assert OK: '{tercerProducto}' agregado al carrito - alerta confirmada");
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
                Console.WriteLine($"\n✓ Agregando al carrito: '{primerProducto}'");
                productPage.ClickBtnAddCart();
                Thread.Sleep(1000);
                bool exitoPhone = cartPage.EsperarYVerificarAlertaExito();
                Assert.IsTrue(exitoPhone, $"No apareció el alert de éxito al agregar '{primerProducto}'");
                Console.WriteLine($"✓ Assert OK: '{primerProducto}' agregado al carrito - alerta confirmada");
            }
            else
            {
                Console.WriteLine("⚠️ No se encontraron productos para 'Tablet'");
            }

            // Paso 4: Mostrar productos del carrito
            cartPage.MostrarProductosCarrito();

            // ==================== ASSERT FINAL ====================
            // Navegar a la página completa del carrito y verificar exactamente 2 filas
            Driver!.Navigate().GoToUrl(UrlPrincipal + "/index.php?route=checkout/cart");
            Thread.Sleep(1000);
            Console.WriteLine($"✓ Navegando al carrito completo. URL: {Driver.Url}");

            var todasLasFilas = Driver.FindElements(By.CssSelector("#content .table tbody tr"));
            // Filtrar solo filas de productos (tienen <input> de cantidad) — excluye filas de totales
            var filasCarrito = todasLasFilas.Where(f => f.FindElements(By.TagName("input")).Count > 0).ToList();
            int cantFilas = filasCarrito.Count;
            Console.WriteLine($"✓ Total de filas en tabla: {todasLasFilas.Count} | Filas de productos: {cantFilas}");

            Assert.AreEqual(2, cantFilas,
                $"Deberían haber exactamente 2 filas en el carrito. Encontradas: {cantFilas}");
            Console.WriteLine("✓ Assert OK: carrito contiene exactamente 2 productos");

            foreach (var fila in filasCarrito)
            {
                Console.WriteLine($"  → {fila.FindElements(By.TagName("td"))[1].Text}");
            }


        }
    }
}
