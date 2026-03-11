using EjerciciosTest.Test;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace EjerciciosTest._03.Test
{
    [TestClass]
    public class Ejercicio3bTest : Test_Base
    {
        [TestMethod]
        //Conexión a la página de OpenCart, navegar para llegar al login y
        // Localizar Elementos con Cinco Estrategias Diferentes
        public void Test_EstrategiasBuscarElementos()
        {
            // ==================== ARRANGE ====================
            string urlPrincipal = "http://opencart.abstracta.us";

            // ==================== ACT ====================
            // Paso 1: Navegar a la página principal
            Driver?.Navigate().GoToUrl(urlPrincipal);
            Console.WriteLine($"✓ Navegación a página principal: {Driver?.Url}");

            // Paso 2: Hacer clic en "My Account" para desplegar el menú
            IWebElement btnMyAccount = Driver.FindElement(By.LinkText("My Account"));
            btnMyAccount.Click();
            Console.WriteLine("✓ Click en 'My Account' - Menú desplegado");

            // Esperar un momento para que se despliegue el menú
            Thread.Sleep(500);

            // Paso 3: Hacer clic en "Login" dentro del menú desplegado
            IWebElement linkLogin = Driver.FindElement(By.LinkText("Login"));
            linkLogin.Click();
            Console.WriteLine("✓ Click en 'Login' - Navegando a la página de login");

            // ==================== ACT - Buscar elementos con 5 estrategias diferentes ====================
            // 1. By.Id - Buscar el input de email por ID
            IWebElement emailInput = Driver.FindElement(By.Id("input-email"));

            // 2. By.Name - Buscar el input de password por Name
            IWebElement passwordInput = Driver.FindElement(By.Name("password"));

            // 3. By.CssSelector - Buscar el botón de login por CSS
            IWebElement loginButton = Driver.FindElement(By.CssSelector("input[value='Login']"));

            // 4. By.LinkText - Buscar el link "Forgotten Password" por texto exacto
            IWebElement forgottenPasswordLink = Driver.FindElement(By.LinkText("Forgotten Password"));

            // 5. By.XPath - Buscar el contenedor principal por XPath
            IWebElement mainContent = Driver.FindElement(By.XPath("//div[@id='content']"));

            // Obtener valores para verificación
            string urlActual = Driver?.Url;
            bool emailVisible = emailInput != null && emailInput.Displayed;
            bool passwordVisible = passwordInput != null && passwordInput.Displayed;
            bool loginButtonVisible = loginButton != null && loginButton.Displayed;
            bool forgottenLinkVisible = forgottenPasswordLink != null && forgottenPasswordLink.Displayed;
            bool mainContentVisible = mainContent != null && mainContent.Displayed;

            // ==================== ASSERT ====================
            // Verificar que llegamos a la página de login
            StringAssert.Contains(urlActual, "route=account/login",
                $"No se navegó a la página de login. URL actual: {urlActual}");
            Console.WriteLine($"✓ URL correcta: {urlActual}");

            // Verificar que todos los elementos se encontraron con las 5 estrategias
            Assert.IsTrue(emailVisible, "Estrategia 1 (By.Id): No se encontró el input de email");
            Console.WriteLine("✓ Estrategia 1 (By.Id): Email input encontrado");

            Assert.IsTrue(passwordVisible, "Estrategia 2 (By.Name): No se encontró el input de password");
            Console.WriteLine("✓ Estrategia 2 (By.Name): Password input encontrado");

            Assert.IsTrue(loginButtonVisible, "Estrategia 3 (By.CssSelector): No se encontró el botón de login");
            Console.WriteLine("✓ Estrategia 3 (By.CssSelector): Botón de login encontrado");

            Assert.IsTrue(forgottenLinkVisible, "Estrategia 4 (By.LinkText): No se encontró el link 'Forgotten Password'");
            Console.WriteLine("✓ Estrategia 4 (By.LinkText): Link 'Forgotten Password' encontrado");

            Assert.IsTrue(mainContentVisible, "Estrategia 5 (By.XPath): No se encontró el contenedor principal");
            Console.WriteLine("✓ Estrategia 5 (By.XPath): Contenedor principal encontrado");

            Console.WriteLine("\n✅ ¡Todas las 5 estrategias de búsqueda funcionaron correctamente!");
        }
    }
}
