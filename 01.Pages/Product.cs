using OpenQA.Selenium;
using System.Collections.ObjectModel;

namespace EjerciciosTest.Pages
{
    internal class Product
    {
        // Campo privado para almacenar el driver
        private readonly IWebDriver driver;

        // Propiedad privada - Localizador de lista de productos, devuelve una colección
        private ReadOnlyCollection<IWebElement> listProducts => driver.FindElements(By.CssSelector(".product-thumb h4 a"));

        // Constructor que recibe el driver desde los tests
        public Product(IWebDriver driver)
        {
            this.driver = driver;
        }

        // ========== MÉTODO GET PÚBLICO - Para acceso desde tests ==========
        public ReadOnlyCollection<IWebElement> GetListProducts() => listProducts;
    }
}
