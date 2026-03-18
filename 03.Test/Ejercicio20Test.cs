using EjerciciosTest.Pages;
using EjerciciosTest.Test;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Collections.ObjectModel;
using System.Runtime.ConstrainedExecution;

namespace EjerciciosTest.Test
{
    [TestClass]
    //Ejercicio 20 – Crear una Clase BaseTest Reutilizable con Herencia
    public class Ejercicio20Test : Test_Base
    {

        [TestMethod]
        // Ejercicio 20 – Capturar Screenshots Automáticos cuando un Test Falla
        public void Test_CircuitoCompleto()
        {
            /*/*Registrar un nuevo usuario con email único.

            Iniciar sesión con el usuario recién creado usando el POM de LoginPage.

            Verificar que el login fue exitoso.

            Navegar al iPhone, agregarlo al carrito y esperar la confirmación.

            Navegar al carrito y verificar que el iPhone aparece.

            Hacer logout y verificar que la sesión fue cerrada.*/
            // Crear instancias de SearchPage, Product y CartPage
            var loginPage = new Pages.LoginPage(Driver);
            var searchPage = new SearchPage(Driver);
            var productPage = new Product(Driver);
            var cartPage = new CartPage(Driver);


            // ==================== ARRANGE ====================
            Console.WriteLine($"✓ Página principal cargada automáticamente: {Driver?.Url}");
            loginPage.HacerClickMyAccount();
            loginPage.HacerClickRegister();
            WebDriverWait wait = new(Driver, TimeSpan.FromSeconds(10));



            // Paso 1: Registrar un nuevo usuario con email único.
            // Ingresar datos en First Name, Last Name, Email y Telephone.
            string emailUnico = $"juanaPerez{DateTime.Now.Ticks}@test.com";
            Driver.FindElement(By.Id("input-firstname")).SendKeys("Juana");
            Driver.FindElement(By.Id("input-lastname")).SendKeys("Perez");
            Driver.FindElement(By.Id("input-email")).SendKeys(emailUnico);
            Driver.FindElement(By.Id("input-telephone")).SendKeys("47827309");
            Driver.FindElement(By.Id("input-password")).SendKeys("Pass12345!");
            Driver.FindElement(By.Id("input-confirm")).SendKeys("Pass12345!");
            loginPage.HacerClickBtnCheckBox();
            loginPage.HacerClickBtnContinue();
            Thread.Sleep(1000);
            loginPage.HacerClickMyAccount();
            Thread.Sleep(500);
            loginPage.HacerClickLogOut();

            // Paso 2: Iniciar sesión con el usuario recién creado usando el POM de LoginPage
            loginPage.HacerClickMyAccount();
            loginPage.HacerClickLinkLogin();
            loginPage.Login(emailUnico, "Pass12345!");
            Console.WriteLine("✓ Login ejecutado con POM");
            Thread.Sleep(2000);
            string urlActual = Driver.Url;
            Console.WriteLine($"URL actual: {urlActual}");

            // Verificar que el login fue exitoso
            StringAssert.Contains(urlActual, "route=account/account",
                $"No se navegó a la página de cuenta. URL actual: {urlActual}");
            Console.WriteLine("✓ Login exitoso verificado");

            // Paso 3: Navegar al iPhone, agregarlo al carrito y esperar la confirmación.
            searchPage.SearchProduct("iPhone");
            Thread.Sleep(1000);
            productPage.ClickBtnAddCart();
            Thread.Sleep(1500);
            var mensajeExito = cartPage.GetSuccessAlert();
            wait.Until(driver => mensajeExito.Text.Contains("Success"));
            Console.WriteLine($"✓ Condición lambda verificada: mensaje contiene 'Success'");
            Console.WriteLine("✓ iPhone agregado al carrito");

            // Paso 4: Navegar al carrito y verificar que el iPhone aparece.
            Driver.Navigate().GoToUrl(UrlPrincipal + "/index.php?route=checkout/cart");
            Thread.Sleep(1000);
            var filasCarrito = Driver.FindElements(By.CssSelector("#content .table tbody tr"));
            Assert.IsTrue(filasCarrito.Count > 0, "El carrito debería tener al menos un producto");
            bool iPhoneEnCarrito = filasCarrito.Any(f => f.Text.Contains("iPhone"));
            Assert.IsTrue(iPhoneEnCarrito, "El iPhone debería estar en el carrito");
            Console.WriteLine("✓ iPhone verificado en el carrito");

            // Paso 5: Hacer logout y verificar que la sesión fue cerrada.
            loginPage.HacerClickMyAccount();
            loginPage.HacerClickLogOut();
            Thread.Sleep(1000);
            string urlLogout = Driver.Url;
            Console.WriteLine($"URL tras logout: {urlLogout}");
            StringAssert.Contains(urlLogout, "route=account/logout",
                $"No se navegó a la página de logout. URL actual: {urlLogout}");
            Console.WriteLine("✓ Logout exitoso verificado");

        }
    }
}
