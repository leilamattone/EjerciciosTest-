using EjerciciosTest.Pages;
using EjerciciosTest.Test;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Collections.ObjectModel;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;

namespace EjerciciosTest.Test
{
    [TestClass]
    public class Ejercicio17Test : Test_Base
    {
        [TestMethod]
        // Ejercicio 17 – Capturar Screenshots Automáticos cuando un Test Falla
        public void Test_CapturarScreenshots()
        {
            /*En [TestCleanup], verificar TestContext.CurrentTestOutcome.

              Si el resultado es UnitTestOutcome.Failed, castear el driver a ITakesScreenshot.

              Generar un nombre de archivo con el nombre del test y el timestamp.
            
              Guardar el screenshot con screenshot.SaveAsFile(path).

              Crear un test que falle intencionalmente y verificar que se genera el archivo.*/


            // ==================== ARRANGE ====================
            Console.WriteLine($"✓ Página principal cargada automáticamente: {Driver?.Url}");

            //==================== ASSERT ====================
            // Test que fall aintencionalmente para verificar el screenshot automático

            Assert.Fail("Fallo intencional para verificar el screenshot automático");

        }
    }
}
