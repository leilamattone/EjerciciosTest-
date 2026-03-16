using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace EjerciciosTest.Pages
{
    internal class SearchPage
    {
        // Campo privado para almacenar el driver
        private readonly IWebDriver driver;

        // Propiedad privada - Localizador barra de búsqueda 
        private IWebElement searchField => driver.FindElement(By.CssSelector("input[name=\"search\"]"));

        // Propiedad privada - Localizador del botón de búsqueda
        private IWebElement btnSearch => driver.FindElement(By.CssSelector("button.btn.btn-default.btn-lg"));

        // Navegación - Breadcrumb
        private IWebElement breadcrumb => driver.FindElement(By.CssSelector("ul.breadcrumb"));

        // Menú - Components
        private IWebElement MenuComponents => driver.FindElement(By.LinkText("Components"));
        private IWebElement MenuShowAllComponents => driver.FindElement(By.LinkText("Show All Components"));

        // Menú - Monitors (subcategoría en el sidebar de Components)
        private IWebElement Monitors => driver.FindElement(By.XPath("//div[contains(@class,'list-group')]//a[contains(text(),'Monitors')]"));

        // Constructor que recibe el driver desde los tests
        public SearchPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        // ========== MÉTODOS GET PÚBLICOS - Para acceso desde tests ==========
        public IWebElement GetSearchField() => searchField;
        public IWebElement GetBtnSearch() => btnSearch;

        // ========== MÉTODO DE COMPORTAMIENTO - Expone texto, no el elemento ==========
        public string GetBreadcrumbText() => breadcrumb.Text;

        // ========== MÉTODOS DE ACCIÓN ==========
        public void HacerClickSearch()
        {
            btnSearch.Click();
        }

        public void SearchProduct(string producto)
        {
            searchField.Clear();
            searchField.SendKeys(producto);
            HacerClickSearch();
        }

        public void HacerClickComponents()
        {
            // Hover sobre Components para abrir el dropdown
            new Actions(driver)
                .MoveToElement(MenuComponents)
                .Perform();
            Thread.Sleep(800);

            // Click en "Show All Components"
            MenuShowAllComponents.Click();
            Thread.Sleep(1000);
        }

        public void HacerClickMonitors()
        {
            Monitors.Click();
            Thread.Sleep(1000);
        }
    }
}
