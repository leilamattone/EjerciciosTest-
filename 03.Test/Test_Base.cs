using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace EjerciciosTest.Test
{
    // [TestClass]
    public class Test_Base
    {
        // Aquí puedes agregar cualquier configuración común para tus pruebas, como inicialización de datos, configuración de entorno, etc.
        // Driver, TestInitialize, TestCleanup, etc.

        // URL principal de la aplicación
        protected const string UrlPrincipal = "http://opencart.abstracta.us";

        // Cambiar a protected para que las clases hijas puedan acceder
        protected IWebDriver? Driver;

        [TestInitialize]
        public void Setup()
        {
            //Opciones de configuración para el navegador Chrome
            var options = new ChromeOptions();
            options.AddArgument("--disable-blink-features=AutomationControlled");
            options.AddArgument("--start-maximized");
            options.AddExcludedArgument("enable-automation");
            options.AddAdditionalOption("useAutomationExtension", false);

            // Ignorar errores de certificado SSL (solo para entornos de prueba)
            options.AcceptInsecureCertificates = true;
            options.AddArgument("--ignore-certificate-errors");
            options.AddArgument("--ignore-ssl-errors");

            //Inicialización del controlador de Chrome con las opciones configuradas
            Driver = new ChromeDriver(options);

            // Navegar automáticamente a la página principal
            Driver.Navigate().GoToUrl(UrlPrincipal);
        }

        [TestCleanup]
        //Cierre de conexión
        public void TearDown()
        {
            Driver?.Quit();
        }
    }
}
