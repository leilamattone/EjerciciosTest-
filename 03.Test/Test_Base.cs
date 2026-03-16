using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace EjerciciosTest.Test
{
    // [TestClass]
    public class Test_Base
    {
        // Aquí puedes agregar cualquier configuración común para tus pruebas, como inicialización de datos, configuración de entorno, etc.
        // Driver, TestInitialize, TestCleanup, etc.

        // Propiedad inyectada por MSTest para acceder al contexto del test (nombre, resultado, etc.)
        public TestContext TestContext { get; set; } = null!;

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
           
            if (TestContext.CurrentTestOutcome == UnitTestOutcome.Failed && Driver is ITakesScreenshot screenshotDriver)
            {
                // Generar un nombre de archivo con el nombre del test y el timestamp
                string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                string testName = TestContext.TestName;
                string fileName = $"{testName}_{timestamp}.png";
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), fileName);
                // Guardar el screenshot
                Screenshot screenshot = screenshotDriver.GetScreenshot();
                screenshot.SaveAsFile(filePath);
                Console.WriteLine($"✓ Screenshot guardado: {filePath}");
            }
            
            Driver?.Quit();
        }
    }
}
