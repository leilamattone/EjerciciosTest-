using EjerciciosTest.Test;
using EjerciciosTest.Pages;
using OpenQA.Selenium;

namespace EjerciciosTest.Test
{
    [TestClass]
    public class LoginTest : Test_Base
    {
        // Necesario para usar TestContext.TestName, TestRunDirectory, etc.
        public TestContext TestContext { get; set; }
        [TestMethod]
        public void Test_LoginConPOM_OK()
        {
            // ==================== ARRANGE ====================
            string email = "leila3@test.com";
            string password = "Pass12345!";

            var loginPage = new LoginPage(Driver);

            // ==================== ACT - PRIMER LOGIN (EXITOSO) ====================
            // Ya estamos en la página principal gracias a Test_Base.Setup()
            Console.WriteLine($"✓ Página principal cargada automáticamente: {Driver?.Url}");

            // Paso 1: Navegar a la página de login
            loginPage.HacerClickMyAccount();
            Thread.Sleep(500);
            loginPage.HacerClickLinkLogin();
            Thread.Sleep(1000);

            // Verifica si está en la página de login antes de intentar hacer login
            Assert.IsTrue(Driver.Url.Contains("route=account/login"), "No se llegó a la página de login");
            Console.WriteLine($"✓ Llegó a la página de login: {Driver.Url}");

            loginPage.Login(email, password);

            Console.WriteLine($"✓ Primer login ejecutado con POM usando: {email}");
            Thread.Sleep(2000);

            // Capturar URL después del primer login
            string urlDespuesLogin1 = Driver.Url;
            Console.WriteLine($"URL después del primer login: {urlDespuesLogin1}");

            // ==================== ASSERT - VERIFICAR PRIMER LOGIN EXITOSO ====================
            Assert.IsTrue(Driver.Title.Contains("My Account"), "La página no contiene 'My Account'");
            Assert.IsTrue(urlDespuesLogin1.Contains("route=account/account"), "La url de la página no es la esperada");
        }

        [TestMethod]
        public void Test_LoginConPOM_Erroneo()
        {
            // ==================== ARRANGE ====================
            string email2 = "juan34@test.com";
            string password2 = "12345!PasswordIncorrecta";

            var loginPage = new LoginPage(Driver);

            // ==================== ACT - LOGIN ERRÓNEO ====================
            // Ya estamos en la página principal gracias a Test_Base.Setup()
            Console.WriteLine($"✓ Página principal cargada automáticamente: {Driver?.Url}");

            // Paso 1: Navegar a la página de login
            loginPage.HacerClickMyAccount();
            Thread.Sleep(500);
            loginPage.HacerClickLinkLogin();
            Thread.Sleep(1000);

            // Verifica si está en la página de login antes de intentar hacer login
            Assert.IsTrue(Driver.Url.Contains("route=account/login"), "No se llegó a la página de login");
            Console.WriteLine($"✓ Llegó a la página de login: {Driver.Url}");

            loginPage.Login(email2, password2);
            Thread.Sleep(2000);

            // Verificar que existe el mensaje de error usando Displayed
            bool existeMensajeError = loginPage.ErrorMessage != null && loginPage.ErrorMessage.Displayed;
            Console.WriteLine($"¿Mensaje de error visible?: {(existeMensajeError ? "Sí" : "No")}");

            if (existeMensajeError)
            {
                Console.WriteLine($"Texto del mensaje de error: '{loginPage.ErrorMessage.Text}'");
            }
            else
            {
                // Solo capturar screenshot cuando el test falla (no apareció el mensaje de error esperado)
                try
                {
                    Console.WriteLine($"\n⚠️ Test '{TestContext.TestName}' FALLÓ - No apareció el mensaje de error - Capturando screenshot...");

                    ITakesScreenshot screenshotDriver = (ITakesScreenshot)Driver;
                    Screenshot screenshot = screenshotDriver.GetScreenshot();

                    string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                    string nombreTest = TestContext.TestName;
                    string nombreArchivo = $"Screenshot_{nombreTest}_{timestamp}.png";

                    string directorioScreenshots = Path.Combine(TestContext.TestRunDirectory, "Screenshots");
                    if (!Directory.Exists(directorioScreenshots))
                    {
                        Directory.CreateDirectory(directorioScreenshots);
                    }

                    string rutaCompleta = Path.Combine(directorioScreenshots, nombreArchivo);
                    screenshot.SaveAsFile(rutaCompleta);

                    Console.WriteLine($"✓ Screenshot guardado en: {rutaCompleta}");
                    TestContext.AddResultFile(rutaCompleta);
                    Console.WriteLine($"✓ Screenshot adjuntado al reporte de pruebas");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"❌ Error al capturar screenshot: {ex.Message}");
                }
            }

            // ==================== ASSERT - VERIFICAR LOGIN FALLIDO ====================
            string urlDespuesLogin2 = Driver.Url;

            Assert.IsFalse(urlDespuesLogin2.Contains("route=account/account"),
                $"El login NO debería haber sido exitoso. URL actual: {urlDespuesLogin2}");
            Console.WriteLine("✓ Login falló correctamente - No redirigió a la página de cuenta");

            Assert.IsTrue(existeMensajeError, "No hay mensaje de error visible después del login fallido");
            Console.WriteLine("✓ Verificación: Mensaje de error (Warning) está visible");

            Console.WriteLine("\n✅ ¡Login fallido verificado correctamente!");
        }
    }
}
