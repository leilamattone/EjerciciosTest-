using EjerciciosTest.Test;
using EjerciciosTest.Pages;
using OpenQA.Selenium;

namespace EjerciciosTest._03.Test
{
    [TestClass]
    public class Ejercicio7bTest : Test_Base
    {
        [TestMethod]
        public void Test_LoginConPOM()
        {
            // ==================== ARRANGE ====================
            string urlPrincipal = "http://opencart.abstracta.us";
            string email = "leila3@test.com";
            string password = "Pass12345!";
            string email2 = "juan34@test.com";
            string password2 = "12345!PasswordIncorrecta";

            // ==================== ACT - PRIMER LOGIN (EXITOSO) ====================
            // Paso 1: Navegar a la página principal
            Driver?.Navigate().GoToUrl(urlPrincipal);
            Console.WriteLine($"✓ Navegación a página principal: {Driver?.Url}");

            // Paso 2: Navegar a la página de login
            IWebElement btnMyAccount = Driver.FindElement(By.LinkText("My Account"));
            btnMyAccount.Click();
            Console.WriteLine("✓ Click en 'My Account' - Menú desplegado");
            Thread.Sleep(500);

            IWebElement linkLogin = Driver.FindElement(By.LinkText("Login"));
            linkLogin.Click();
            Console.WriteLine("✓ Click en 'Login' - Navegando a la página de login");
            Thread.Sleep(1000);

            // Paso 3: Primer login con credenciales correctas
            var loginPage = new LoginPage(Driver);
            loginPage.Login(email, password);
            Console.WriteLine($"✓ Primer login ejecutado con POM usando: {email}");
            Thread.Sleep(2000);

            // Capturar URL después del primer login
            string urlDespuesLogin1 = Driver.Url;
            Console.WriteLine($"URL después del primer login: {urlDespuesLogin1}");

            // ==================== ASSERT - VERIFICAR PRIMER LOGIN EXITOSO ====================
            // Verificar que el primer login fue exitoso
            bool primerLoginExitoso = urlDespuesLogin1.Contains("route=account/account");

            if (primerLoginExitoso)
            {
                Console.WriteLine("✓ Primer login EXITOSO - URL contiene 'route=account/account'");
                Assert.IsTrue(Driver.PageSource.Contains("My Account"), 
                    "La página no contiene 'My Account'");
                Console.WriteLine("✓ Página de cuenta cargada correctamente");
            }
            else
            {
                Assert.Fail($"El primer login falló. URL actual: {urlDespuesLogin1}");
            }

            // ==================== ACT - LOGOUT ====================
            // Paso 4: Hacer logout
            loginPage.HacerClickLogOut();
            Console.WriteLine("✓ Logout ejecutado");
            Thread.Sleep(2000);

            // ==================== ACT - SEGUNDO LOGIN (FALLIDO) ====================
            // Paso 5: Navegar nuevamente al login
            IWebElement btnMyAccount2 = Driver.FindElement(By.LinkText("My Account"));
            btnMyAccount2.Click();
            Console.WriteLine("✓ Click en 'My Account' - Menú desplegado (segunda vez)");
            Thread.Sleep(500);

            IWebElement linkLogin2 = Driver.FindElement(By.LinkText("Login"));
            linkLogin2.Click();
            Console.WriteLine("✓ Click en 'Login' - Navegando a la página de login (segunda vez)");
            Thread.Sleep(1000);

            // Paso 6: Segundo login con credenciales incorrectas
            var loginPage2 = new LoginPage(Driver);
            loginPage2.Login(email2, password2);
            Console.WriteLine($"✓ Segundo login ejecutado con POM usando: {email2}");
            Thread.Sleep(2000);

            // Capturar URL después del segundo login
            string urlDespuesLogin2 = Driver.Url;
            Console.WriteLine($"URL después del segundo login: {urlDespuesLogin2}");

            // ==================== ASSERT - VERIFICAR SEGUNDO LOGIN FALLIDO ====================
            // Verificar que el segundo login falló (no redirige a account/account)
            bool segundoLoginFallo = !urlDespuesLogin2.Contains("route=account/account");

            if (segundoLoginFallo)
            {
                // Buscar el mensaje de error (alerta de peligro)
                try
                {
                    IWebElement alertaDanger = Driver.FindElement(By.CssSelector(".alert-danger"));
                    bool alertaVisible = alertaDanger.Displayed;

                    Assert.IsTrue(alertaVisible, "No se encontró el mensaje de error después del login fallido");
                    Console.WriteLine("✓ Segundo login FALLÓ correctamente - Aparece alerta de error");
                    Console.WriteLine($"✓ Mensaje de error: {alertaDanger.Text}");
                }
                catch (NoSuchElementException)
                {
                    Console.WriteLine("⚠️ Advertencia: No se encontró .alert-danger, verificando por otros medios");
                    // Verificar que seguimos en la página de login
                    Assert.IsTrue(urlDespuesLogin2.Contains("route=account/login"),
                        "El login falló pero no apareció mensaje de error");
                    Console.WriteLine("✓ Se mantuvo en la página de login - Login falló correctamente");
                }
            }
            else
            {
                Assert.Fail($"El segundo login NO debería haber sido exitoso. URL actual: {urlDespuesLogin2}");
            }

            Console.WriteLine("\n✅ ¡Ambos casos de login verificados correctamente!");
            Console.WriteLine($"   - Login exitoso con: {email} ✓");
            Console.WriteLine($"   - Login fallido con: {email2} ✓");
        }
    }
}
