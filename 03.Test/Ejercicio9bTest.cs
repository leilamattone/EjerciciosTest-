using EjerciciosTest.Pages;
using EjerciciosTest.Test;
using OpenQA.Selenium;
using System.Collections.ObjectModel;

namespace EjerciciosTest._03.Test
{
    [TestClass]
    public class Ejercicio9bTest : Test_Base
    {
        [TestMethod]
        //Buscar un Producto y Validar los Resultados
        public void Test_BuscarVerificar()
        {
            // ==================== ARRANGE ====================
            // Ya estamos en la página principal gracias a Test_Base.Setup()
            Console.WriteLine($"✓ Página principal cargada automáticamente: {Driver?.Url}");

            // ==================== ACT ====================
            // Paso 1: Crear instancias de SearchPage, Product y CartPage
            var searchPage = new SearchPage(Driver);
            var productPage = new Product(Driver);
            var cartPage = new CartPage(Driver);

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

            // Paso 3: Obtener lista de productos
            ReadOnlyCollection<IWebElement> productos = productPage.GetListProducts();
            Console.WriteLine($"✓ Cantidad de productos encontrados: {productos.Count}");

            // Mostrar los títulos de todos los productos encontrados
            Console.WriteLine("\n📦 Títulos de productos encontrados:");
            for (int i = 0; i < productos.Count; i++)
            {
                Console.WriteLine($"  {i + 1}. {productos[i].Text}");
            }
            Console.WriteLine();

            // Paso 4: Agregar el primer producto al carrito (si se encontró alguno)
            if (productos.Count > 0)
            {
                productPage.ClickBtnAddCart();
                Console.WriteLine($"✓ Primer producto '{productos[0].Text}' agregado al carrito");
                Thread.Sleep(1000);
            }

            // Paso 5: Verificar que el .alert-success contiene "Success"
            IWebElement successAlert = cartPage.GetSuccessAlert();

            // ==================== ASSERT ====================
            // Verificar que se encontraron productos
            Assert.IsTrue(productos.Count > 0,
                $"Se esperaba encontrar productos de 'iPhone', pero se encontraron {productos.Count}");
            Console.WriteLine($"✓ Verificación 1: Se encontraron {productos.Count} productos");

            // Verificar que apareció la alerta de éxito
            Assert.IsTrue(successAlert.Displayed,
                "No apareció el mensaje de éxito al agregar el producto al carrito");
            Console.WriteLine("✓ Verificación 2: Alerta de éxito visible");

            StringAssert.Contains(successAlert.Text, "Success",
                $"El mensaje no contiene 'Success'. Texto actual: '{successAlert.Text}'");
            Console.WriteLine($"✓ Verificación 3: Mensaje contiene 'Success': '{successAlert.Text}'");
        }
    }
}