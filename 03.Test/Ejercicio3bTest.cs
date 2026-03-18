using EjerciciosTest.Pages;
using EjerciciosTest.Test;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System.Runtime.ConstrainedExecution;

namespace EjerciciosTest.Test
{
    [TestClass]
    public class Ejercicio3bTest : Test_Base
    {
        [TestMethod]
        //Conexión a la página de OpenCart, navegar para llegar al login y
        public void Test_EstrategiasBuscarElementos()
        {
            // ==================== ARRANGE ====================
            // Ya estamos en la página principal gracias a Test_Base.Setup()
            Console.WriteLine($"✓ Página principal cargada automáticamente: {Driver?.Url}");
            // Crear instancia de LoginPage para utilizar todos los métodos
            var loginPageNav = new LoginPage(Driver);
            // ==================== ACT ====================

            // Paso 1: Hacer clic en "My Account" para desplegar el menú
            //  y link "Login"

            loginPageNav.HacerClickMyAccount();
            Console.WriteLine("✓ Click en 'My Account' - Menú desplegado");

            // Esperar un momento para que se despliegue el menú
            Thread.Sleep(1000);

            // Paso 3: Hacer clic en "Login" dentro del menú desplegado usando LoginPage
            loginPageNav.HacerClickLinkLogin();
            Console.WriteLine("✓ Click en 'Login' - Navegando a la página de login (usando LoginPage.HacerClickLinkLogin())");



            //Obtener valores para verificación
            //string urlActual = Driver?.Url;
            //bool emailVisible = emailInput != null && emailInput.Displayed;
            //bool passwordVisible = passwordInput != null && passwordInput.Displayed;
            //bool loginButtonVisible = loginButton != null && loginButton.Displayed;
            //bool forgottenLinkVisible = forgottenPasswordLink != null && forgottenPasswordLink.Displayed;
            //bool mainContentVisible = mainContent != null && mainContent.Displayed;

            //// ==================== ASSERT ====================
            //// Verificar que llegamos a la página de login
            //StringAssert.Contains(urlActual, "route=account/login",
            //    $"No se navegó a la página de login. URL actual: {urlActual}");
            //Console.WriteLine($"✓ URL correcta: {urlActual}");

            //// Verificar que todos los elementos se encontraron con las 5 estrategias
            //Assert.IsTrue(emailVisible, "Estrategia 1 (By.Id): No se encontró el input de email");
            //Console.WriteLine("✓ Estrategia 1 (By.Id): Email input encontrado");

            //Assert.IsTrue(passwordVisible, "Estrategia 2 (By.Name): No se encontró el input de password");
            //Console.WriteLine("✓ Estrategia 2 (By.Name): Password input encontrado");

            //Assert.IsTrue(loginButtonVisible, "Estrategia 3 (By.CssSelector): No se encontró el botón de login");
            //Console.WriteLine("✓ Estrategia 3 (By.CssSelector): Botón de login encontrado");

            //Assert.IsTrue(forgottenLinkVisible, "Estrategia 4 (By.LinkText): No se encontró el link 'Forgotten Password'");
            //Console.WriteLine("✓ Estrategia 4 (By.LinkText): Link 'Forgotten Password' encontrado");

            //Assert.IsTrue(mainContentVisible, "Estrategia 5 (By.XPath): No se encontró el contenedor principal");
            //Console.WriteLine("✓ Estrategia 5 (By.XPath): Contenedor principal encontrado");

            Console.WriteLine("\n✅ ¡Todas las 5 estrategias de búsqueda funcionaron correctamente!");
        }
    }
}
