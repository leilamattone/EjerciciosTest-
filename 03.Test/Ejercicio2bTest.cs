using EjerciciosTest.Test;
using OpenQA.Selenium;

namespace EjerciciosTest._03.Test
{
    [TestClass]
    public class Ejercicio2bTest : Test_Base
    {
        [TestMethod]
        //Conexión a la página de OpenCart y búsqueda de elementos con assertions
        public void Test_BuscarElementos()
        {
            // ==================== ARRANGE ====================
            string urlEsperada = "http://opencart.abstracta.us";

            // ==================== ACT ====================
            // Navegar a la página
            Driver?.Navigate().GoToUrl(urlEsperada);

            // Obtener elementos de la página
            IWebElement eCart = Driver.FindElement(By.Id("cart"));
            IWebElement searchField = Driver.FindElement(By.CssSelector("input[name=\"search\"]"));

            // Obtener valores actuales
            string urlActual = Driver?.Url;
            bool seEncontroCart = eCart != null && eCart.Displayed;
            bool seEncontroSearch = searchField != null && searchField.Displayed;
            string textoCarrito = eCart.Text;

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