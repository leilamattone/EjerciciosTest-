using EjerciciosTest.Test;
using EjerciciosTest.Pages;
using OpenQA.Selenium;

namespace EjerciciosTest.Test
{
    [TestClass]
    public class Ejercicio2bTest : Test_Base
    {
        [TestMethod]
        //Conexión a la página de OpenCart y búsqueda de elementos con assertions
        public void Test_BuscarElementos()
        {
            // ==================== ARRANGE ====================
            // Ya estamos en la página principal gracias a Test_Base.Setup()
            Console.WriteLine($"✓ Página principal cargada automáticamente: {Driver?.Url}");

            // ==================== ACT ====================
            // Navegar a la página
            //Driver?.Navigate().GoToUrl(urlEsperada);

            // ========== USANDO SEARCHPAGE con método Get ==========
            // Crear instancia de CartPage
            CartPage cartPage = new CartPage(Driver);
            // Crear instancia de SearchPage
            SearchPage searchPage = new SearchPage(Driver);

            // Obtener elementos de la página usando SearchPage y CartPage
            IWebElement searchField = searchPage.GetSearchField();
            string textoCarrito = cartPage.GetTextoCarrito();

            /* ========== CÓDIGO ORIGINAL COMENTADO ==========
             * IWebElement eCart = Driver.FindElement(By.Id("cart"));
            IWebElement searchField = Driver.FindElement(By.CssSelector("input[name=\"search\"]"));
            ========================================================================== */

            // Obtener valores actuales
            string urlActual = Driver?.Url;
            bool seEncontroCart = !string.IsNullOrEmpty(textoCarrito);
            bool seEncontroSearch = searchField != null && searchField.Displayed;

            // ==================== ASSERT ====================
            // Verificar que estamos en la URL correcta
            StringAssert.Contains(urlActual, "opencart.abstracta.us",
                $"No se navegó al sitio esperado. URL actual: {urlActual}");
            Console.WriteLine($"✓ Navegación exitosa a: {urlActual}");

            // Verificar que el carrito está visible
            Assert.IsTrue(seEncontroCart, "El carrito no se encuentra visible");
            Console.WriteLine($"✓ Se encontró el carrito: Sí");

            // Verificar que el campo de búsqueda está visible
            Assert.IsTrue(seEncontroSearch, "No se encontró el campo de búsqueda");
            Console.WriteLine($"✓ Se encontró el campo de búsqueda: Sí");

            // Verificar que el texto del carrito contiene "$"
            StringAssert.Contains(textoCarrito, "$",
                $"El texto del botón del carrito no contiene el símbolo '$'. Texto actual: '{textoCarrito}'");
            Console.WriteLine($"✓ Texto del carrito verificado: '{textoCarrito}'");
        }
    }
}