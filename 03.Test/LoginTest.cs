using EjerciciosTest.Test;
using EjerciciosTest.Pages;
using OpenQA.Selenium;

namespace EjerciciosTest.Test
{
    [TestClass]
    public class LoginTest : Test_Base
    {
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
            Assert.Contains("route=account/login", Driver.Url, "No se llegó a la página de login");
            Console.WriteLine($"✓ Llegó a la página de login: {Driver.Url}");

            loginPage.Login(email, password);

            Console.WriteLine($"✓ Primer login ejecutado con POM usando: {email}");
            Thread.Sleep(2000);

            // Capturar URL después del primer login
            string urlDespuesLogin1 = Driver.Url;
            Console.WriteLine($"URL después del primer login: {urlDespuesLogin1}");

            // ==================== ASSERT - VERIFICAR PRIMER LOGIN EXITOSO ====================
            Assert.Contains("My Account", Driver.Title, "La página no contiene 'My Account'");
            Assert.Contains("route=account/account", urlDespuesLogin1, "La url de la página no es la esperada");
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
            Assert.Contains("route=account/login", Driver.Url, "No se llegó a la página de login");
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

        [TestMethod]
        public void Test_EditarCuenta_OK()
        {
            // ==================== ARRANGE ====================
            string email    = "leila3@test.com";
            string password = "Pass12345!";
            string nuevoNombre = "Leila";

            var loginPage = new LoginPage(Driver);

            // ==================== ACT ====================
            Console.WriteLine($"✓ Página principal cargada: {Driver?.Url}");

            // Paso 1: Login
            loginPage.HacerClickMyAccount();
            Thread.Sleep(500);
            loginPage.HacerClickLinkLogin();
            Thread.Sleep(1000);
            loginPage.Login(email, password);
            Thread.Sleep(2000);
            Console.WriteLine($"✓ Login exitoso. URL: {Driver!.Url}");

            // Paso 2: Navegar a Edit Account
            loginPage.EditAccount();
            Thread.Sleep(1000);
            Console.WriteLine($"✓ Formulario Edit Account abierto. URL: {Driver.Url}");

            // Paso 3: Editar solo el nombre (resto vacío = no se modifica)
            Console.WriteLine($"✓ Editando First Name → '{nuevoNombre}'");
            loginPage.EditarCuenta(nuevoNombre, "", "", "");

            // Paso 4: Verificar alert de éxito
            bool exitoEdicion = loginPage.EsperarAlertaExito();
            Assert.IsTrue(exitoEdicion, "No apareció el mensaje de éxito al guardar la cuenta");
            Console.WriteLine("✓ Assert OK: alerta 'Your account has been successfully updated' confirmada");

            // Paso 5: Volver a Edit Account para verificar que el nombre fue guardado
            loginPage.HacerClickMyAccount();
            Thread.Sleep(500);
            loginPage.EditAccount();
            Thread.Sleep(1000);

            string firstNameActual = loginPage.GetFirstNameValue();
            Console.WriteLine($"✓ Valor actual del campo First Name: '{firstNameActual}'");

            // ==================== ASSERT ====================
            Assert.AreEqual(nuevoNombre, firstNameActual,
                $"El First Name debería ser '{nuevoNombre}'. Valor actual: '{firstNameActual}'");
            Console.WriteLine($"✓ Assert OK: First Name guardado correctamente → '{firstNameActual}'");
        }

        [TestMethod]
        public void Test_EditarPassword_OK()
        {
            // ==================== ARRANGE ====================
            string email           = "leila34@test.com";
            string passwordActual  = "test123";
            string passwordNuevo   = "test456";

            var loginPage = new LoginPage(Driver);

            // ==================== ACT ====================
            Console.WriteLine($"✓ Página principal cargada: {Driver?.Url}");

            // Paso 1: Login con contraseña actual
            loginPage.HacerClickMyAccount();
            Thread.Sleep(500);
            loginPage.HacerClickLinkLogin();
            Thread.Sleep(1000);
            loginPage.Login(email, passwordActual);
            Thread.Sleep(2000);
            Assert.Contains("route=account/account", Driver!.Url, "No se pudo hacer login con la contraseña actual");
            Console.WriteLine($"✓ Login exitoso con contraseña actual. URL: {Driver.Url}");

            // Paso 2: Navegar al formulario Change Password
            loginPage.HacerClickMyAccount();
            Thread.Sleep(500);
            loginPage.HacerClickBtnPassword();
            Thread.Sleep(1000);
            Assert.Contains("route=account/password", Driver.Url, "No se llegó al formulario de cambio de contraseña");
            Console.WriteLine($"✓ Formulario Change Password abierto. URL: {Driver.Url}");

            // Paso 3: Cambiar la contraseña
            Console.WriteLine($"✓ Cambiando contraseña → '{passwordNuevo}'");
            loginPage.EditarContraseña(passwordNuevo, passwordNuevo);

            // Paso 4: Verificar alert de éxito
            bool exitoCambio = loginPage.EsperarAlertaExito();
            Assert.IsTrue(exitoCambio, "No apareció el mensaje de éxito al cambiar la contraseña");
            Console.WriteLine("✓ Assert OK: alerta 'Your password has been successfully updated' confirmada");

            // Paso 5: Logout y login con la nueva contraseña para confirmar el cambio
            loginPage.HacerClickMyAccount();
            Thread.Sleep(500);
            loginPage.HacerClickLogOut();
            Thread.Sleep(1000);
            Console.WriteLine("✓ Logout realizado");

            loginPage.HacerClickMyAccount();
            Thread.Sleep(500);
            loginPage.HacerClickLinkLogin();
            Thread.Sleep(1000);
            loginPage.Login(email, passwordNuevo);
            Thread.Sleep(2000);
            Assert.Contains("route=account/account", Driver.Url, "El login con la nueva contraseña falló");
            Console.WriteLine("✓ Assert OK: login exitoso con la nueva contraseña");

            // Paso 6: Revertir contraseña al valor original (para que el test sea repetible)
            loginPage.HacerClickMyAccount();
            Thread.Sleep(500);
            loginPage.HacerClickBtnPassword();
            Thread.Sleep(1000);
            loginPage.EditarContraseña(passwordActual, passwordActual);
            bool exitoRevertir = loginPage.EsperarAlertaExito();
            Assert.IsTrue(exitoRevertir, "No se pudo revertir la contraseña al valor original");
            Console.WriteLine($"✓ Assert OK: contraseña revertida a '{passwordActual}' correctamente");
        }
    }
}
