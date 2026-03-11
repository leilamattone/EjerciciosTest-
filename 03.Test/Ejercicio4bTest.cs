using EjerciciosTest.Test;
using OpenQA.Selenium;

namespace EjerciciosTest._03.Test
{
    [TestClass]
    public class Ejercicio4bTest : Test_Base
    {
        [TestMethod]
        //Ingresar Texto y Limpiar Campos con SendKeys y Clear
        public void Test_IngresoTextoLimpiezaCampos()
        {
            // ==================== ARRANGE ====================
            string urlPrincipal = "http://opencart.abstracta.us";

            // ==================== ACT ====================
            // Paso 1: Navegar a la página principal
            Driver?.Navigate().GoToUrl(urlPrincipal);
            Console.WriteLine($"✓ Navegación a página principal: {Driver?.Url}");

            // Paso 2: Localizar el campo de búsqueda con By.Name("search")
            IWebElement searchField = Driver.FindElement(By.Name("search"));
            Console.WriteLine("✓ Campo de búsqueda localizado");

            // Paso 3: Ingresar "iPhone" con SendKeys y verificar el valor con GetAttribute("value")
            searchField.SendKeys("iPhone");
            string valorConIPhone = searchField.GetAttribute("value");
            Console.WriteLine($"Valor ingresado: '{valorConIPhone}'");

            // Paso 4: Limpiar el campo con Clear() y verificar que quedó vacío
            searchField.Clear();
            string valorDespuesClear = searchField.GetAttribute("value");
            Console.WriteLine($"Valor después de Clear(): '{valorDespuesClear}'");

            // Paso 5: Ingresar "MacBook" y verificar nuevamente
            searchField.SendKeys("MacBook");
            string valorConMacBook = searchField.GetAttribute("value");
            Console.WriteLine($"Valor final: '{valorConMacBook}'");

            // Obtener URL actual para verificación
            string urlActual = Driver.Url;

            // ==================== ASSERT ====================
            // Verificar que estamos en la página correcta
            StringAssert.Contains(urlActual, "opencart.abstracta.us",
                $"No se navegó al sitio esperado. URL actual: {urlActual}");
            Console.WriteLine($"✓ URL correcta: {urlActual}");

            // Verificar que el primer valor fue "iPhone"
            Assert.AreEqual("iPhone", valorConIPhone,
                $"Se esperaba 'iPhone' pero se encontró: '{valorConIPhone}'");
            Console.WriteLine("✓ Verificación 1: 'iPhone' ingresado correctamente");

            // Verificar que después de Clear() el campo quedó vacío
            Assert.AreEqual("", valorDespuesClear,
                $"El campo debería estar vacío después de Clear(), pero contiene: '{valorDespuesClear}'");
            Console.WriteLine("✓ Verificación 2: Campo limpiado correctamente - está vacío");

            // Verificar que el valor final es "MacBook"
            Assert.AreEqual("MacBook", valorConMacBook,
                $"Se esperaba 'MacBook' pero se encontró: '{valorConMacBook}'");
            Console.WriteLine("✓ Verificación 3: 'MacBook' ingresado correctamente");

            Console.WriteLine("\n✅ ¡Todas las operaciones SendKeys y Clear funcionaron correctamente!");
        }
    }
}
