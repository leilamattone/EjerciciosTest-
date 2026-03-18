using EjerciciosTest.Pages;
using EjerciciosTest.Test;
using OpenQA.Selenium;
using System.Collections.ObjectModel;
using System.Security.Principal;

namespace EjerciciosTest._03.Test
{
    //Ejercicio 10 – Navegar por Categorías y Verificar Breadcrumbs

    [TestClass]
    public class Ejercicio10Test : Test_Base
    {
        [TestMethod]
        public void Test_NavegarCategoriasBreadcrumbs()

        {
            // ==================== ARRANGE ====================
            // Ya estamos en la página principal gracias a Test_Base.Setup()
            Console.WriteLine($"✓ Página principal cargada automáticamente: {Driver?.Url}");

            // ==================== ACT ====================
            // Paso 1: Crear instancias de SearchPage, Product y CartPage
            SearchPage searchPage = new(Driver);
            Product productPage = new(Driver);
            CartPage cartPage = new(Driver);
            
            // Paso 2: Hacer clic en Components en el menú.
            searchPage.HacerClickComponents();

            // Verificar que la URL contiene path=25 y el breadcrumb muestra "Components"
            string urlActual = Driver.Url;
            StringAssert.Contains(urlActual, "path=25",
                $"No se navegó a la página de Components. URL actual: {urlActual}");
            StringAssert.Contains(searchPage.GetBreadcrumbText(), "Components",
                $"El breadcrumb no muestra 'Components'. Texto actual: '{searchPage.GetBreadcrumbText()}'");
            Console.WriteLine($"✓ URL correcta: {urlActual}");
            Console.WriteLine($"✓ Breadcrumb correcto: '{searchPage.GetBreadcrumbText()}'");

            //Hacer clic en Monitors(subcategoría).
            searchPage.HacerClickMonitors();
            //Verificar que la URL contiene path = 25_28 y el breadcrumb muestra "Monitors".*/
            string urlActual2 = Driver.Url;
            StringAssert.Contains(urlActual2, "path=25_28",
                $"No se navegó a la página de Monitors. URL actual: {urlActual2}");
            StringAssert.Contains(searchPage.GetBreadcrumbText(), "Components",
                $"El breadcrumb no muestra 'Monitors'. Texto actual: '{searchPage.GetBreadcrumbText()}'");
            Console.WriteLine($"✓ URL correcta: {urlActual2}");
            Console.WriteLine($"✓ Breadcrumb correcto: '{searchPage.GetBreadcrumbText()}'");
        }
    }
}
