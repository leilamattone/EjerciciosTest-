using EjerciciosTest.Pages;
using EjerciciosTest.Test;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace EjerciciosTest.Test
{
    [TestClass]
    public class SearchTest : Test_Base
    {
        [TestMethod]
        public void Test_SearchConPOM_OK()
        {
          
            // ==================== ARRANGE ====================
            Console.WriteLine($"✓ Página principal cargada automáticamente: {Driver?.Url}");

            SearchPage searchPage = new(Driver);

            // ==================== ACT ====================
            
            // Paso 1: Buscar "iPhone" usando la barra de búsqueda
            Console.WriteLine($"✓ Buscar \"iPhone\" usando la barra de búsqueda");
            searchPage.SearchProduct("iPhone");
            int cantidad = searchPage.GetCantidadProductos();
            Console.WriteLine($"✓ Productos encontrados: {cantidad}");

            // Paso 2: Buscar "iPhone" navegando por el menú de categorías
            Console.WriteLine($"✓ Buscar \"iPhone\" navegando por el menú de categorías");
            searchPage.SearchProductByCategory("iPhone", "Phones & PDAs");
            int cantidad2 = searchPage.GetCantidadProductos();
            Console.WriteLine($"✓ Productos encontrados: {cantidad2}");

            // ==================== ASSERT ====================
            // Verificar que la búsqueda por barra encontró resultados
            Assert.IsGreaterThan(0, cantidad,
                $"La búsqueda de 'iPhone' debería retornar al menos 1 resultado. Encontrados: {cantidad}");
            Console.WriteLine($"✓ Assert OK: búsqueda por barra encontró {cantidad} resultado(s)");

            // Verificar que la búsqueda por categoría también encontró resultados
            Assert.IsGreaterThan(0, cantidad2,
                $"La búsqueda de 'iPhone' en 'Phones & PDAs' debería retornar al menos 1 resultado. Encontrados: {cantidad2}");
            Console.WriteLine($"✓ Assert OK: búsqueda por categoría encontró {cantidad2} resultado(s)");

            // Verificar que ambas búsquedas devuelven la misma cantidad
            Assert.AreEqual(cantidad, cantidad2,
                $"Ambas búsquedas deberían encontrar la misma cantidad de productos. " +
                $"Barra: {cantidad} | Categoría: {cantidad2}");
            Console.WriteLine($"✓ Assert OK: ambas búsquedas coinciden ({cantidad} == {cantidad2})");
        }

        // ==================== DATA-DRIVEN ====================
        [TestMethod]
        [DataRow("Mac",   4)]
        [DataRow("Phone", 1)]
        // "Tablet" no retorna resultados: existe como categoría en el menú pero no hay productos con ese nombre
        public void Test_SearchProductos_DataRow(string terminoBusqueda, int resultadosEsperados)
        {
            // ==================== ARRANGE ====================
            SearchPage searchPage = new SearchPage(Driver);

            // ==================== ACT ====================
            searchPage.SearchProduct(terminoBusqueda);
            int cantidad = searchPage.GetCantidadProductos();
            Console.WriteLine($"✓ Búsqueda: '{terminoBusqueda}' | Encontrados: {cantidad} | Esperados: {resultadosEsperados}");

            // ==================== ASSERT ====================
            Assert.AreEqual(resultadosEsperados, cantidad,
                $"La búsqueda '{terminoBusqueda}' debería retornar {resultadosEsperados} resultado(s). Encontrados: {cantidad}");
            Console.WriteLine($"✓ Assert OK: '{terminoBusqueda}' retornó {cantidad} resultado(s)");
        }

        // ==================== NAVEGACIÓN POR MENÚ ====================
        // Estas categorías no tienen productos buscables por nombre pero sí accesibles por menú
        [TestMethod]
        [DataRow("tablets")]
        [DataRow("phones")]
        // "software" es categoría de menú pero no tiene productos listados en el sitio
        public void Test_NavegacionCategoria_DataRow(string categoria)
        {
            // ==================== ARRANGE ====================
            var searchPage = new SearchPage(Driver);

            // ==================== ACT ====================
            switch (categoria.ToLower())
            {
                case "tablets": searchPage.HacerClickTablets();    break;
                case "phones":  searchPage.HacerClickPhonesPDAs(); break;
            }
            Thread.Sleep(1000);
            int cantidad = searchPage.GetCantidadProductos();
            Console.WriteLine($"✓ Categoría: '{categoria}' | Encontrados: {cantidad}");


            // ==================== ASSERT ====================
            Assert.IsGreaterThan(0, cantidad,
                $"La categoría '{categoria}' debería tener al menos 1 producto. Encontrados: {cantidad}");
            Console.WriteLine($"✓ Assert OK: categoría '{categoria}' tiene {cantidad} producto(s)");
        }
    }
}
