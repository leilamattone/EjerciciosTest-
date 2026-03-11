using EjerciciosTest.Test;
using OpenQA.Selenium;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;

namespace EjerciciosTest._03.Test
{
    [TestClass]
    public class Ejercicio5bTest : Test_Base
    {
        [TestMethod]
        // Navegar entre Páginas con Links y el Historial del Navegador
        public void Test_NavegarPaginas()
        {
            // ==================== ARRANGE ====================
            string url = "http://opencart.abstracta.us";

            // ==================== ACT ====================
            // Paso 1: Navegar a la página principal y guardar su URL en una variable
            Driver?.Navigate().GoToUrl(url);
            Console.WriteLine($"✓ Navegación a página principal: {Driver?.Url}");
            string urlPrincipal = Driver.Url;

            // Paso 2: Hacer clic en el link "Desktops" del menú para abrir el dropdown
            IWebElement linkDesktops = Driver.FindElement(By.LinkText("Desktops"));
            Console.WriteLine("✓ Desktops localizado");
            linkDesktops.Click();
            Thread.Sleep(500); // Esperar a que se despliegue el menú

            // Hacer clic en "Show All Desktops" dentro del submenú
            IWebElement showAllDesktops = Driver.FindElement(By.LinkText("Show All Desktops"));
            Console.WriteLine("✓ Show All Desktops localizado");
            showAllDesktops.Click();
            Thread.Sleep(1000); // Esperar a que cargue la página

            // Capturar URL después de navegar a Desktops
            string urlDesktops = Driver.Url;
            Console.WriteLine($"URL después de navegar a Desktops: {urlDesktops}");

            // Paso 3: Verificar que la URL contiene "path=20"
            StringAssert.Contains(urlDesktops, "path=20",
                $"No se navegó a la página de Desktops. URL actual: {urlDesktops}");

            // Paso 4: Usar driver.Navigate().Back() para volver.
            Driver.Navigate().Back();

            // Paso 5: Verificar que la URL volvió a la home.
            string urlDespuesBack = Driver.Url;
            Console.WriteLine($"URL después de Back(): {urlDespuesBack}");

            Assert.AreEqual(urlPrincipal, urlDespuesBack,
                $"No volvió a la página principal. Esperado: {urlPrincipal}, Actual: {urlDespuesBack}");
            Console.WriteLine("✓ Verificación: Volvió correctamente a la página principal");

            // Paso 6: Usar driver.Navigate().Forward() y verificar que avanzó correctamente.
            Driver.Navigate().Forward();
            Thread.Sleep(1000); // Esperar a que cargue

            // Obtener URL actual para verificación
            string urlActual = Driver.Url;
            Console.WriteLine($"URL después de Forward(): {urlActual}");

            // ==================== ASSERT ====================
            // Verificar que avanzó y la URL es DISTINTA a la página principal
            Assert.AreNotEqual(urlPrincipal, urlActual,
                $"No avanzó correctamente. Aún está en la página principal: {urlActual}");
            Console.WriteLine($"✓ URL diferente a la principal - Forward() ejecutado correctamente");

            // Verificar que volvió a Desktops (contiene "path=20")
            StringAssert.Contains(urlActual, "path=20",
                $"No volvió a Desktops. URL actual: {urlActual}");
            Console.WriteLine($"✓ Forward() llevó de vuelta a Desktops");

            Console.WriteLine("\n✅ ¡Todas las operaciones de navegación funcionaron correctamente!");
        }
    }
    
}
