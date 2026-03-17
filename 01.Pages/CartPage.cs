using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
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
        public static By DangerAlertLocator  => By.CssSelector(".alert-danger");

        // Ver carrito completo
        private IWebElement BtnVerCarrito => driver.FindElement(By.CssSelector(".fa .fa-shopping-cart"));

        // Botón que elimina el primer producto del dropdown del carrito
        private IWebElement BtnEliminarProducto => driver.FindElement(By.CssSelector("#cart ul.dropdown-menu li:first-child table tbody tr td:last-child button"));


        // Constructor que recibe el driver desde los tests
        public CartPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        // ========== MÉTODOS GET PÚBLICOS - Para acceso desde tests ==========
        public string GetTextoCarrito() => eCart.Text;
        public IWebElement GetSuccessAlert() => successAlert;
        public int GetCantidadItemsCarrito() => itemsCarrito.Count;

        // Espera hasta 10 segundos a que aparezca el alert y verifica que contiene "Success"
        public bool EsperarYVerificarAlertaExito(int segundos = 10)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(segundos));
            IWebElement alerta = wait.Until(d => d.FindElement(SuccessAlertLocator));
            return alerta.Displayed && alerta.Text.Contains("Success");
        }

        // Verifica si el producto está disponible en la cantidad deseada.
        // Retorna true si NO hay alerta de stock, false si aparece el .alert-danger.
        public bool ProductoDisponibleEnCarrito(int timeoutSegundos = 3)
        {
            try
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutSegundos));
                IWebElement alerta = wait.Until(d => d.FindElement(DangerAlertLocator));
                // Si encontró la alerta roja → producto NO disponible en esa cantidad
                return !alerta.Displayed || !alerta.Text.Contains("not available");
            }
            catch (WebDriverTimeoutException)
            {
                // No apareció alerta roja → producto disponible
                return true;
            }
        }

        // ========== MÉTODOS DE ACCIÓN ==========
        public void IngresarAlCarrito() => eCart.Click();
        public void MostrarCarrito() => BtnVerCarrito.Click();

        // Abre el dropdown solo si no está ya abierto (evita el toggle que lo cierra)
        private void AbrirDropdown()
        {
            if (!eCart.GetAttribute("class").Contains("open"))
            {
                eCart.Click();
                Thread.Sleep(500);
            }
        }

        // Cierra el dropdown solo si está abierto
        private void CerrarDropdown()
        {
            if (eCart.GetAttribute("class").Contains("open"))
            {
                eCart.Click();
                Thread.Sleep(300);
            }
        }

        // Busca un producto por categoría y nombre, y lo agrega al carrito la cantidad indicada
        public void AgregarProductoAlCarrito(string categoria, string nombreProducto, int cantidad)
        {
            new SearchPage(driver).SearchProductByCategory(nombreProducto, categoria);
            Thread.Sleep(1000);

            var tarjetas = driver.FindElements(By.CssSelector(".product-thumb"));
            foreach (var tarjeta in tarjetas)
            {
                if (tarjeta.FindElement(By.CssSelector("h4 a")).Text.Contains(nombreProducto))
                {
                    IWebElement btnAgregar = tarjeta.FindElement(By.CssSelector("button:first-of-type"));
                    for (int i = 0; i < cantidad; i++)
                    {
                        btnAgregar.Click();
                        Thread.Sleep(800);
                    }
                    return;
                }
            }
            throw new Exception($"Producto '{nombreProducto}' no encontrado en la categoría '{categoria}'");
        }

        // Agrega una cámara al carrito navegando a su página de detalle.
        // color: "Red" o "Blue" para Canon | "0" para Nikon (sin opción de color)
        public void AgregarCamera(string nombreCamera, int cantidad, string color)
        {
            // Paso 1: Navegar a la categoría Cameras y buscar la cámara
            new SearchPage(driver).SearchProductByCategory(nombreCamera, "Cameras");
            Thread.Sleep(1000);

            // Paso 2: Hacer clic en el producto para ir a la página de detalle
            var tarjetas = driver.FindElements(By.CssSelector(".product-thumb"));
            foreach (var tarjeta in tarjetas)
            {
                if (tarjeta.FindElement(By.CssSelector("h4 a")).Text.Contains(nombreCamera))
                {
                    tarjeta.FindElement(By.CssSelector("h4 a")).Click();
                    Thread.Sleep(1000);
                    break;
                }
            }

            // Paso 3: Si la cámara tiene opción de color (Canon), seleccionarla
            if (color != "0")
            {
                string colorNormalizado = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(color.ToLower());
                var selectColor = new SelectElement(
                    driver.FindElement(By.XPath("//select[contains(@name,'option')]")));
                selectColor.SelectByText(colorNormalizado);
            }

            // Paso 4: Establecer la cantidad
            IWebElement inputCantidad = driver.FindElement(By.Id("input-quantity"));
            inputCantidad.Clear();
            inputCantidad.SendKeys(cantidad.ToString());

            // Paso 5: Hacer clic en Add to Cart de la página de detalle
            driver.FindElement(By.Id("button-cart")).Click();
            Thread.Sleep(500);
        }

        // Elimina del dropdown el producto cuyo nombre coincida con el parámetro
        public void EliminarProducto(string nombreProducto)
        {
            AbrirDropdown();
            var filas = driver.FindElements(By.CssSelector("#cart ul.dropdown-menu li:first-child table tbody tr"));
            foreach (var fila in filas)
            {
                if (fila.Text.Contains(nombreProducto))
                {
                    fila.FindElement(By.CssSelector("button")).Click();
                    Thread.Sleep(500);
                    break;
                }
            }
        }

        // Elimina todos los productos del carrito uno por uno
        public void VaciarCarrito()
        {
            AbrirDropdown();
            var filas = driver.FindElements(By.CssSelector("#cart ul.dropdown-menu li:first-child table tbody tr"));
            while (filas.Count > 0)
            {
                filas[0].FindElement(By.CssSelector("button")).Click();
                Thread.Sleep(500);
                AbrirDropdown();
                filas = driver.FindElements(By.CssSelector("#cart ul.dropdown-menu li:first-child table tbody tr"));
            }
        }

        // Elimina el primer producto del carrito (mantiene compatibilidad con tests existentes)
        public void EliminarProductoDelCarrito()
        {
            AbrirDropdown();
            BtnEliminarProducto.Click();
            Thread.Sleep(500);
        }

        // Navega a la página completa del carrito, busca el producto y actualiza su cantidad
        public void ActualizarCarrito(string nombreProducto, int nuevaCantidad)
        {
            string urlCarrito = new Uri(driver.Url).GetLeftPart(UriPartial.Authority) + "/index.php?route=checkout/cart";
            driver.Navigate().GoToUrl(urlCarrito);
            Thread.Sleep(1000);

            var filas = driver.FindElements(By.CssSelector("#content .table tbody tr"));
            foreach (var fila in filas)
            {
                if (fila.Text.Contains(nombreProducto))
                {
                    var celdas = fila.FindElements(By.TagName("td"));
                    IWebElement celdaCantidad = celdas[3];
                    IWebElement inputCantidad = celdaCantidad.FindElement(By.TagName("input"));
                    inputCantidad.Clear();
                    inputCantidad.SendKeys(nuevaCantidad.ToString());
                    celdaCantidad.FindElement(By.CssSelector("button.btn-primary")).Click();
                    Thread.Sleep(1000);
                    Console.WriteLine($"✓ Cantidad de '{nombreProducto}' actualizada a {nuevaCantidad}");
                    return;
                }
            }
            throw new Exception($"Producto '{nombreProducto}' no encontrado en el carrito");
        }

        // Lee el valor actual del input de cantidad de un producto en la página completa del carrito
        public int GetCantidadProductoEnCarrito(string nombreProducto)
        {
            if (!driver.Url.Contains("route=checkout/cart"))
            {
                string urlCarrito = new Uri(driver.Url).GetLeftPart(UriPartial.Authority) + "/index.php?route=checkout/cart";
                driver.Navigate().GoToUrl(urlCarrito);
                Thread.Sleep(1000);
            }

            var filas = driver.FindElements(By.CssSelector("#content .table tbody tr"));
            foreach (var fila in filas)
            {
                if (fila.Text.Contains(nombreProducto))
                {
                    var celdas = fila.FindElements(By.TagName("td"));
                    IWebElement input = celdas[3].FindElement(By.TagName("input"));
                    return int.Parse(input.GetAttribute("value"));
                }
            }
            throw new Exception($"Producto '{nombreProducto}' no encontrado en el carrito");
        }

        // ========== MÉTODO DE COMPORTAMIENTO ==========
        // Muestra nombre, importe de cada producto y total del carrito
        public void MostrarProductosCarrito()
        {
            Console.WriteLine($"\n🛒 Resumen del carrito: {GetTextoCarrito().Split('\n')[0].Trim()}");

            AbrirDropdown();

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

            Console.WriteLine($"  💰 Total: {totalCarrito.Text}");
            CerrarDropdown();
        }
    }
}
