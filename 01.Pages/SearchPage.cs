using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System.Collections.ObjectModel;

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

        // Resultados de búsqueda - colección de productos encontrados
        private ReadOnlyCollection<IWebElement> resultadosBusqueda => driver.FindElements(By.CssSelector(".product-thumb h4 a"));

        // Menú - Desktops (desplegable en navbar)
        private IWebElement MenuDesktops => driver.FindElement(By.LinkText("Desktops"));
        private IWebElement MenuShowAllDesktops => driver.FindElement(By.LinkText("Show All Desktops"));
        private IWebElement MenuDesktopsPC => driver.FindElement(By.XPath("//ul[contains(@class,'dropdown-menu')]//a[normalize-space()='PC']"));
        private IWebElement MenuDesktopsMac => driver.FindElement(By.XPath("//ul[contains(@class,'dropdown-menu')]//a[normalize-space()='Mac']"));


        // Menú - Laptops & Notebooks (desplegable en navbar)
        private IWebElement MenuLatopsNotebooks => driver.FindElement(By.LinkText("Laptops & Notebooks"));
        private IWebElement MenuShowAllLatopsNotebooks => driver.FindElement(By.LinkText("Show All Laptops & Notebooks"));
        private IWebElement MenuLatopsNotebooksMacs => driver.FindElement(By.XPath("//ul[contains(@class,'dropdown-menu')]//a[normalize-space()='Macs']"));
        private IWebElement MenuLatopsNotebooksWindows => driver.FindElement(By.XPath("//ul[contains(@class,'dropdown-menu')]//a[normalize-space()='Windows']"));

        // Menú - Components
        private IWebElement MenuComponents => driver.FindElement(By.LinkText("Components"));
        private IWebElement MenuShowAllComponents => driver.FindElement(By.LinkText("Show All Components"));

        // Menú - Monitors (subcategoría en el sidebar de Components)
        private IWebElement Monitors => driver.FindElement(By.XPath("//div[contains(@class,'list-group')]//a[contains(text(),'Monitors')]"));

        // Menú - Tablets (No tiene desplegable en navbar)
        private IWebElement MenuTablets => driver.FindElement(By.LinkText("Tablets"));

        // Menú - Software (No tiene desplegable en navbar)
        private IWebElement MenuSoftware => driver.FindElement(By.LinkText("Software"));

        // Menú - Phones & PDAs (No tiene desplegable en navbar)
        private IWebElement MenuPhonesPDAs => driver.FindElement(By.LinkText("Phones & PDAs"));

        // Menú - Cameras (No tiene desplegable en navbar)
        private IWebElement MenuCameras => driver.FindElement(By.LinkText("Cameras"));

        // Menú - MP3 Players (No tiene desplegable en navbar)
        private IWebElement MenuMP3Players => driver.FindElement(By.LinkText("MP3 Players"));

        // Constructor que recibe el driver desde los tests
        public SearchPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        // ========== MÉTODOS GET PÚBLICOS - Para acceso desde tests ==========
        public IWebElement GetSearchField() => searchField;
        public IWebElement GetBtnSearch() => btnSearch;
        public int GetCantidadProductos() => resultadosBusqueda.Count;

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

        public void HacerClickDesktops(string opcion = "all")
        {
            // Hover sobre Desktops para abrir el dropdown
            new Actions(driver)
                .MoveToElement(MenuDesktops)
                .Perform();
            Thread.Sleep(800);

            // Click según la opción elegida
            switch (opcion.ToLower())
            {
                case "pc":   MenuDesktopsPC.Click();       break;
                case "mac":  MenuDesktopsMac.Click();      break;
                default:     MenuShowAllDesktops.Click();  break;
            }
            Thread.Sleep(1000);
        }

        public void HacerClickLaptopsNotebooks(string opcion = "all")
        {
            // Hover sobre Laptops & Notebooks para abrir el dropdown
            new Actions(driver)
                .MoveToElement(MenuLatopsNotebooks)
                .Perform();
            Thread.Sleep(800);

            // Click según la opción elegida
            switch (opcion.ToLower())
            {
                case "macs":    MenuLatopsNotebooksMacs.Click();    break;
                case "windows": MenuLatopsNotebooksWindows.Click(); break;
                default:        MenuShowAllLatopsNotebooks.Click(); break;
            }
            Thread.Sleep(1000);
        }

        public void HacerClickMonitors()
        {
            Monitors.Click();
            Thread.Sleep(1000);
        }

        public void HacerClickTablets()
        {
            MenuTablets.Click();
        }

        public void HacerClickSoftware()
        {
            MenuSoftware.Click();
        }

        public void HacerClickPhonesPDAs()
        {
            MenuPhonesPDAs.Click();
        }

        public void HacerclickCameras()
        {
            MenuCameras.Click();
        }

        public void HacerClickMP3()
        {
            MenuMP3Players.Click();
        }

        public void SearchProductByCategory(string producto, string categoria)
        {
            // Paso 1: buscar el producto para llegar a la página de resultados
            SearchProduct(producto);
            Thread.Sleep(1000);

            // Paso 2: seleccionar la categoría en el dropdown de la página de resultados
            var selectCategoria = new SelectElement(driver.FindElement(By.Name("category_id")));
            selectCategoria.SelectByText(categoria);

            // Paso 3: volver a hacer clic en buscar para aplicar el filtro
            driver.FindElement(By.Id("button-search")).Click();
            Thread.Sleep(1000);
        }
    }
}
