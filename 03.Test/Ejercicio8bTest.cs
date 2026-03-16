using EjerciciosTest.Pages;
using EjerciciosTest.Test;
using OpenQA.Selenium;
using System.Collections.ObjectModel;

namespace EjerciciosTest.Test
{
    [TestClass]
    public class Ejercicio8bTest : Test_Base
    {
        [TestMethod]
        //Buscar un Producto y Validar los Resultados
        public void Test_BuscarValidar()
        {
            // ==================== ARRANGE ====================
            // Ya estamos en la página principal gracias a Test_Base.Setup()
            Console.WriteLine($"✓ Página principal cargada automáticamente: {Driver?.Url}");

            // ==================== ACT ====================
            // ========== USANDO SEARCHPAGE ==========
            // Paso 1: Crear instancia de SearchPage
            var searchPage = new SearchPage(Driver);

            // Paso 2: Ingresar "iPhone" en el campo de búsqueda
            IWebElement searchField = searchPage.GetSearchField();
            Console.WriteLine("✓ Campo de búsqueda localizado (usando SearchPage.GetSearchField())");
            searchField.SendKeys("iPhone");
            Console.WriteLine("✓ 'iPhone' ingresado en el campo de búsqueda");

            // Hacer click en el botón de búsqueda
            IWebElement btnSearch = searchPage.GetBtnSearch();
            btnSearch.Click();
            Console.WriteLine("✓ Click en botón de búsqueda");
            Thread.Sleep(2000);

            // ========== USANDO PRODUCT CLASS ==========
            // Paso 3: Crear instancia de Product y obtener lista de productos
            var productPage = new Product(Driver);
            ReadOnlyCollection<IWebElement> productos = productPage.GetListProducts();

            Console.WriteLine($"✓ Cantidad de productos encontrados: {productos.Count}");

            // Mostrar los títulos de todos los productos encontrados
            Console.WriteLine("\n📦 Títulos de productos encontrados:");
            for (int i = 0; i < productos.Count; i++)
            {
                Console.WriteLine($"  {i + 1}. {productos[i].Text}");
            }
            Console.WriteLine();

            // Obtener URL actual para verificación
            string urlActual = Driver.Url;

            // ==================== ASSERT ====================
            // Verificar que estamos en la página de resultados de búsqueda
            StringAssert.Contains(urlActual, "search=MacBook",
                $"No se navegó a la página de búsqueda. URL actual: {urlActual}");
            Console.WriteLine($"✓ URL correcta: {urlActual}");

            // Verificar que se encontraron productos
            Assert.IsTrue(productos.Count > 0, 
                $"Se esperaba encontrar productos de 'MacBook', pero se encontraron {productos.Count}");
            Console.WriteLine($"✓ Verificación 1: Se encontraron {productos.Count} productos");

            // Verificar que todos los productos contienen "MacBook" en su nombre
            bool todosContienenMacBook = true;
            foreach (var producto in productos)
            {
                string nombreProducto = producto.Text;
                if (!nombreProducto.Contains("MacBook"))
                {
                    todosContienenMacBook = false;
                    Console.WriteLine($"⚠️ Producto que no contiene 'MacBook': {nombreProducto}");
                }
            }

            Assert.IsTrue(todosContienenMacBook, "Algunos productos no contienen 'MacBook' en su nombre");
            Console.WriteLine("✓ Verificación 2: Todos los productos contienen 'MacBook'");

            // Mostrar los primeros 3 productos encontrados
            Console.WriteLine("\n📦 Productos encontrados:");
            int maxProductos = Math.Min(3, productos.Count);
            for (int i = 0; i < maxProductos; i++)
            {
                Console.WriteLine($"  {i + 1}. {productos[i].Text}");
            }

            // ==================== SEGUNDA BÚSQUEDA - PRODUCTO INEXISTENTE ====================
            Console.WriteLine("\n========== Segunda búsqueda: Producto inexistente ==========");

            // Volver a la página principal para hacer nueva búsqueda
            Driver.Navigate().GoToUrl(UrlPrincipal);
            Thread.Sleep(1000);

            // Crear nuevas instancias para la segunda búsqueda
            var searchPage2 = new SearchPage(Driver);
            IWebElement searchField2 = searchPage2.GetSearchField();

            // Ingresar producto inexistente
            searchField2.SendKeys("xyzabc123");
            Console.WriteLine("✓ 'xyzabc123' ingresado en el campo de búsqueda");

            // Hacer click en búsqueda
            IWebElement btnSearch2 = searchPage2.GetBtnSearch();
            btnSearch2.Click();
            Console.WriteLine("✓ Click en botón de búsqueda");
            Thread.Sleep(2000);

            // Obtener lista de productos (debería estar vacía o mostrar mensaje)
            var productPage2 = new Product(Driver);
            ReadOnlyCollection<IWebElement> productosNoEncontrados = productPage2.GetListProducts();

            Console.WriteLine($"✓ Cantidad de productos encontrados: {productosNoEncontrados.Count}");

            // Buscar mensaje de "no hay productos" o similar
            bool hayMensajeNoResultados = false;
            string mensajeNoResultados = "";

            try
            {
                // Intentar encontrar el mensaje de "no hay productos"
                IWebElement mensajeElement = Driver.FindElement(By.CssSelector("p"));
                mensajeNoResultados = mensajeElement.Text;

                if (mensajeNoResultados.Contains("no product") || 
                    mensajeNoResultados.Contains("No products") ||
                    mensajeNoResultados.Contains("There is no product"))
                {
                    hayMensajeNoResultados = true;
                    Console.WriteLine($"✓ Mensaje encontrado: '{mensajeNoResultados}'");
                }
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("⚠️ No se encontró un mensaje específico de 'sin resultados'");
            }

            // ==================== ASSERT - SEGUNDA BÚSQUEDA ====================
            // Verificar que NO se encontraron productos
            Assert.AreEqual(0, productosNoEncontrados.Count,
                $"Se esperaba NO encontrar productos para 'xyzabc123', pero se encontraron {productosNoEncontrados.Count}");
            Console.WriteLine("✓ Verificación 3: No se encontraron productos (como se esperaba)");

            // Verificar que hay un mensaje de "sin resultados" o la lista está vacía
            Assert.IsTrue(productosNoEncontrados.Count == 0 || hayMensajeNoResultados,
                "Se esperaba que no hubiera productos o que apareciera un mensaje de 'sin resultados'");
            Console.WriteLine("✓ Verificación 4: Se confirmó que el producto no existe");

            Console.WriteLine("\n✅ ¡Ambas búsquedas funcionaron correctamente!");
            Console.WriteLine("   - Búsqueda exitosa: 'MacBook' ✓");
            Console.WriteLine("   - Búsqueda sin resultados: 'xyzabc123' ✓");
        }
    }
}
