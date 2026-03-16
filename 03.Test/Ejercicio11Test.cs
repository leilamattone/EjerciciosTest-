using EjerciciosTest.Pages;
using EjerciciosTest.Test;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace EjerciciosTest._03.Test
{
    [TestClass]
    public class Ejercicio11Test : Test_Base
    {
        [TestMethod]
        //Ejercicio 11 – Waits Explícitos con WebDriverWait y ExpectedConditions
        public void Test_WebDriverWaitExpectedConditions()
        {
            // ==================== ARRANGE ====================
            Console.WriteLine($"✓ Página principal cargada automáticamente: {Driver?.Url}");

            // Crear WebDriverWait con timeout de 10 segundos
            WebDriverWait wait = new(Driver, TimeSpan.FromSeconds(10));

            // ==================== ACT ====================
            var searchPage = new SearchPage(Driver);

            // Paso 1: Buscar "iPhone" y hacer click en buscar
            searchPage.SearchProduct("iPhone");
            searchPage.HacerClickSearch();
            Console.WriteLine("✓ Búsqueda de 'iPhone' realizada");

            // Paso 2: Esperar que el botón "Add to Cart" sea clickeable (ElementToBeClickable)
            // Usa el localizador de la clase Product (sin exponer la variable privada)
            IWebElement btnAddCart = wait.Until(
                ExpectedConditions.ElementToBeClickable(Product.BtnAddCartLocator)
            );
            Console.WriteLine("✓ Botón 'Add to Cart' está clickeable");

            // Hacer click en "Add to Cart"
            btnAddCart.Click();
            Console.WriteLine("✓ Click en 'Add to Cart' del primer iPhone");

            // Paso 3: Esperar que el alert de éxito sea visible (ElementIsVisible)
            // Usa el localizador de la clase CartPage (sin exponer la variable privada)
            IWebElement successAlert = wait.Until(
                ExpectedConditions.ElementIsVisible(CartPage.SuccessAlertLocator)
            );
            Console.WriteLine("✓ Alert de éxito visible");

            // Paso 4: Condición personalizada con lambda
            // Espera hasta que el texto del alert contenga "Success"
            wait.Until(driver => successAlert.Text.Contains("Success"));
            Console.WriteLine($"✓ Condición lambda verificada: mensaje contiene 'Success'");

            // ==================== ASSERT ====================
            Assert.IsTrue(successAlert.Displayed,
                "No apareció el alert de éxito después de agregar al carrito");

            StringAssert.Contains(successAlert.Text, "Success",
                $"El mensaje no contiene 'Success'. Texto actual: '{successAlert.Text}'");
            Console.WriteLine($"✓ Verificación: '{successAlert.Text}'");

            Console.WriteLine("\n✅ ¡WebDriverWait y ExpectedConditions funcionaron correctamente!");
        }
    }
}
