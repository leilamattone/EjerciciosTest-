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

        // Propiedad privada - Botón "Add to Cart" del primer producto
        private IWebElement BtnAddCart => driver.FindElement(By.XPath("(//div[@class='product-thumb']//button)[1]"));

        // Localizador público - Para usar con WebDriverWait en los tests
        public static By BtnAddCartLocator => By.XPath("(//div[@class='product-thumb']//button)[1]");

        // Constructor que recibe el driver desde los tests
        public Product(IWebDriver driver)
        {
            this.driver = driver;
        }

        // ========== MÉTODOS GET PÚBLICOS - Para acceso desde tests (Opción 2) ==========
        public ReadOnlyCollection<IWebElement> GetListProducts() => listProducts;

        // ========== MÉTODOS DE ACCIÓN ==========
        public void ClickBtnAddCart() => BtnAddCart.Click();
    }
}
