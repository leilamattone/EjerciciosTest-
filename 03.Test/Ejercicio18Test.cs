using EjerciciosTest.Pages;
using EjerciciosTest.Test;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Collections.ObjectModel;

namespace EjerciciosTest.Test
{
    [TestClass]
    public class Ejercicio18Test : Test_Base
    {
        /*
        Método	Tests generados
        Test_DataDrivenTesting(string, int)	("Mac", 4) · ("Phone", 1) · ("Tablet", 1)
        Test_DataDrivenTesting_DynamicData(string, int)	("iMac", 1) · ("MacBook", 3) · ("Canon", 1)
        Diferencia clave entre los dos atributos:
        •	[DataRow] — los datos van directamente sobre el método, son literales estáticos.
        •	[DynamicData] — los datos vienen de una propiedad/método estático, útil para casos más complejos o datos generados en tiempo de ejecución.
        */

        // ==================== VERSIÓN 1: [DataRow] ====================
        [DataTestMethod]
        [DataRow("Mac", 4)]
        [DataRow("Phone", 1)]
        //[DataRow("Tablet", 1)]
        // Permiten definir parámetros de entrada personalizado para las pruebas automatizadas, pudiendo ingresar múltiples conjuntos de datos.
        // Ejercicio 18 – Data-Driven Testing con [DataTestMethod] y [DataRow]
        public void Test_DataDrivenTesting(string terminoBusqueda, int resultadosEsperados)
        {
            // ==================== ARRANGE ====================
            Console.WriteLine($"✓ Página principal cargada automáticamente: {Driver?.Url}");
            var searchPage = new SearchPage(Driver);
            var productPage = new Product(Driver);

            // ==================== ACT ====================
            searchPage.SearchProduct(terminoBusqueda);
            Thread.Sleep(1000);

            ReadOnlyCollection<IWebElement> productos = productPage.GetListProducts();
            Console.WriteLine($"✓ Búsqueda: '{terminoBusqueda}' | Encontrados: {productos.Count} | Esperados: {resultadosEsperados}");

            // ==================== ASSERT ====================
            Assert.AreEqual(resultadosEsperados, productos.Count, $"La búsqueda '{terminoBusqueda}' debería retornar {resultadosEsperados} resultado(s)");
        }

        // ==================== VERSIÓN 2: [DynamicData] ====================
        public static IEnumerable<object[]> DatosBusqueda =>
        [
            ["iMac",    1],
            ["MacBook", 3],
            ["Canon",   1],
        ];

        [DataTestMethod]
        [DynamicData(nameof(DatosBusqueda))]
        public void Test_DataDrivenTesting_DynamicData(string terminoBusqueda, int resultadosEsperados)
        {
            // ==================== ARRANGE ====================
            Console.WriteLine($"✓ Página principal cargada automáticamente: {Driver?.Url}");
            var searchPage = new SearchPage(Driver);
            var productPage = new Product(Driver);

            // ==================== ACT ====================
            searchPage.SearchProduct(terminoBusqueda);
            Thread.Sleep(1000);

            ReadOnlyCollection<IWebElement> productos = productPage.GetListProducts();
            Console.WriteLine($"✓ Búsqueda: '{terminoBusqueda}' | Encontrados: {productos.Count} | Esperados: {resultadosEsperados}");

            // ==================== ASSERT ====================
            Assert.AreEqual(resultadosEsperados, productos.Count, $"La búsqueda '{terminoBusqueda}' debería retornar {resultadosEsperados} resultado(s)");
        }
    }
}

