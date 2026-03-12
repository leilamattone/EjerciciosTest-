using OpenQA.Selenium;

namespace EjerciciosTest.Pages
{
    internal class SearchPage
    {
        // Campo privado para almacenar el driver
        private readonly IWebDriver driver;

        // Propiedad privada - Localizador barra de búsqueda (CORREGIDO: = a =>)
        private IWebElement searchField => driver.FindElement(By.CssSelector("input[name=\"search\"]"));

        // Propiedad privada - Localizador del botón de búsqueda (botón dentro del div search)
        private IWebElement btnSearch => driver.FindElement(By.CssSelector("button.btn.btn-default.btn-lg"));

        // Constructor que recibe el driver desde los tests
        public SearchPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        // ========== MÉTODOS GET PÚBLICOS - Para acceso desde tests ==========
        public IWebElement GetSearchField() => searchField;
        public IWebElement GetBtnSearch() => btnSearch;

        // ========== MÉTODOS DE ACCIÓN ==========
        public void HacerClickSearch()
        {
            btnSearch.Click();
        }
    }
}
