using OpenQA.Selenium;

namespace EjerciciosTest.Pages
{
    internal class CartPage
    {
        // Campo privado para almacenar el driver
        private readonly IWebDriver driver;

        // Propiedad privada - Localizador de carrito
        private IWebElement eCart => driver.FindElement(By.Id("cart"));

        // Constructor que recibe el driver desde los tests
        public CartPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        // ========== MÉTODOS GET PÚBLICOS - Para acceso desde tests ==========
        public IWebElement GetECart() => eCart;
        public string GetTextoCarrito() => eCart.Text;
    }
}
