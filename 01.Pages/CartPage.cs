using OpenQA.Selenium;
using System.Collections.ObjectModel;

namespace EjerciciosTest.Pages
{
    internal class CartPage
    {
        // Campo privado para almacenar el driver
        private readonly IWebDriver driver;

        // Propiedad privada - Localizador de carrito
        private IWebElement eCart => driver.FindElement(By.Id("cart"));

        // Propiedad privada - Localizador de alerta de éxito
        private IWebElement successAlert => driver.FindElement(By.CssSelector(".alert-success"));

        // Propiedades privadas - Productos del carrito (dropdown)
        private ReadOnlyCollection<IWebElement> itemsCarrito =>
            driver.FindElements(By.CssSelector("#cart ul.dropdown-menu li:first-child table tbody tr"));

        private IWebElement totalCarrito =>
            driver.FindElement(By.CssSelector("#cart li:nth-child(2) tbody tr:last-child td:last-child"));

        // Localizador público - Para usar con WebDriverWait en los tests
        public static By SuccessAlertLocator => By.CssSelector(".alert-success");

        // Ver carrito completo - Localizador público para usar con WebDriverWait en los tests
        private IWebElement BtnVerCarrito => driver.FindElement(By.CssSelector(".fa .fa-shopping-cart"));


        // Constructor que recibe el driver desde los tests
        public CartPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        // ========== MÉTODOS GET PÚBLICOS - Para acceso desde tests ==========
        public string GetTextoCarrito() => eCart.Text;
        public IWebElement GetSuccessAlert() => successAlert;

        // ========== MÉTODOS DE ACCIÓN ==========
        public void IngresarAlCarrito() => eCart.Click();

        public void MostrarCarrito() => BtnVerCarrito.Click();

        // ========== MÉTODO DE COMPORTAMIENTO ==========
        // Muestra nombre, importe de cada producto y total del carrito
        public void MostrarProductosCarrito()
        {
            // Usar GetTextoCarrito() para mostrar el resumen del carrito
            Console.WriteLine($"\n🛒 Resumen del carrito: {GetTextoCarrito().Split('\n')[0].Trim()}");

            // Abrir el dropdown del carrito
            IngresarAlCarrito();
            Thread.Sleep(500);

            // Obtener y mostrar cada producto del carrito
            Console.WriteLine("\n📦 Productos en el carrito:");
            foreach (var item in itemsCarrito)
            {
                var celdas = item.FindElements(By.TagName("td"));
                if (celdas.Count >= 4)
                {
                    string nombre = celdas[1].Text;
                    string importe = celdas[3].Text;
                    Console.WriteLine($"  - Nombre: {nombre} | Importe: {importe}");
                }
            }

            // Mostrar el total
            Console.WriteLine($"  💰 Total: {totalCarrito.Text}");
        }
    }
}
