using EjerciciosTest.Pages;
using EjerciciosTest.Test;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Collections.ObjectModel;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;


namespace EjerciciosTest.Test
{
    [TestClass]
    public class Ejercicio16Test : Test_Base
    {
        [TestMethod]
        //Ejercicio 16 – Leer y Validar una Tabla HTML Completa
        public void Test_LeerValidarAtributos()
        {
            /*Recorrer dinámicamente todas las filas de la tabla del carrito, extraer datos de cada celda y validar la coherencia de la información.*/
            
            // ==================== ARRANGE ====================
            Console.WriteLine($"✓ Página principal cargada automáticamente: {Driver?.Url}");

            // Crear instancias de SearchPage, Product y CartPage
            LoginPage loginPage = new (Driver);
            SearchPage searchPage = new (Driver);
            Product productPage = new (Driver);
            CartPage cartPage = new (Driver);


            // ==================== ACT ====================

            /*Agregar 2 productos al carrito y navegar a /index.php?route=checkout/cart.

            Localizar todas las filas con FindElements(By.CssSelector("#content .table tbody tr")).

            Para cada fila, usar row.FindElements(By.TagName("td")) en el contexto de esa fila.

            Extraer nombre, cantidad y precio de cada fila y guardarlos en un Dictionary.

            Verificar que ningún nombre está vacío y que la cantidad inicial es "1".*/

            // Paso 1 - Agregar 2 productos al carrito
            // Buscar "Mac" y hacer click en buscar
            searchPage.SearchProduct("Mac");
            Console.WriteLine("✓ Búsqueda de 'Mac' realizada");
            Thread.Sleep(1000);

            // Obtener la lista de productos del resultado de búsqueda
            ReadOnlyCollection<IWebElement> productos = productPage.GetListProducts();
            Console.WriteLine($"✓ Cantidad de productos encontrados: {productos.Count}");

            // Si hay productos, mostrar la lista e agregar el primero al carrito
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

            // Buscar "Phone" y hacer click en buscar
            searchPage.SearchProduct("Phone");
            Console.WriteLine("✓ Búsqueda de 'Phone' realizada");
            Thread.Sleep(1000);

            // Obtener la lista de productos del resultado de búsqueda
            ReadOnlyCollection<IWebElement> productos2 = productPage.GetListProducts();
            Console.WriteLine($"✓ Cantidad de productos encontrados: {productos2.Count}");

            // Si hay productos, mostrar la lista e agregar el primero al carrito
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

            // Navegar a la página del carrito
            Driver!.Navigate().GoToUrl(UrlPrincipal + "/index.php?route=checkout/cart");
            Thread.Sleep(1000);
            // Paso 2: Localizar todas las filas con FindElements(By.CssSelector("#content .table tbody tr")).

            //  Para cada fila, usar row.FindElements(By.TagName("td")) en el contexto de esa fila.
            var filasCarrito = Driver.FindElements(By.CssSelector("#content .table tbody tr"));
            Assert.IsTrue(filasCarrito.Count > 0, "No se encontraron filas en la tabla del carrito");

            // Paso 3: Extraer nombre, cantidad y precio de cada fila y guardarlos en un Dictionary.


            Dictionary<string, (string cantidad, string precio)> productosCarrito = new Dictionary<string, (string cantidad, string precio)>();

            foreach (var fila in filasCarrito)
            {
                var celdas = fila.FindElements(By.TagName("td"));
                if (celdas.Count >= 4)
                {
                    string nombre = celdas[1].Text;
                    string cantidad = celdas[3].FindElement(By.CssSelector("input")).GetAttribute("value");
                    string precio = celdas[4].Text;
                    productosCarrito[nombre] = (cantidad, precio);
                    Console.WriteLine($"Producto: {nombre} | Cantidad: {cantidad} | Precio: {precio}");
                }
            }

            // ==================== ASSERT ====================
            // Verificar que ningún nombre está vacío y que la cantidad inicial es "1"
            foreach (var kvp in productosCarrito)
            {
                Assert.IsFalse(string.IsNullOrEmpty(kvp.Key), "El nombre del producto no debe estar vacío");
                Assert.AreEqual("1", kvp.Value.cantidad, $"La cantidad del producto '{kvp.Key}' debería ser '1'");
                Console.WriteLine($"✓ Producto '{kvp.Key}' validado: cantidad={kvp.Value.cantidad}, precio={kvp.Value.precio}");
            }

        }
    }
}
