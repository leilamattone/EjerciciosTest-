using System.Collections.ObjectModel;

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

using SeleniumExtras.WaitHelpers;

namespace EjerciciosTest
{
    [TestClass]
    public sealed class Test1
    {   //ejercicio

        private ChromeDriver? Driver; //[Comment] Nombres fuera del metodo deben ser PascalCase por convención, y no deben ser públicos a menos que sea necesario. Se cambió a private IWebDriver driver;

        [TestInitialize]
        public void Setup()
        {
            //Opciones de configuración para el navegador Chrome
            var options = new ChromeOptions();
            options.AddArgument("--disable-blink-features=AutomationControlled");
            options.AddArgument("--start-maximized");
            options.AddExcludedArgument("enable-automation");
            options.AddAdditionalOption("useAutomationExtension", false);

            // Ignorar errores de certificado SSL (solo para entornos de prueba)
            options.AcceptInsecureCertificates = true;
            options.AddArgument("--ignore-certificate-errors");
            options.AddArgument("--ignore-ssl-errors");

            //Inicialización del controlador de Chrome con las opciones configuradas
            Driver = new ChromeDriver(options);
        }
        [TestMethod]
        //Conexión a la página de OpenCart y búsqueda de "Your Store"
        public void Ejercicio1()
        {
            Driver?.Navigate().GoToUrl("http://opencart.abstracta.us/");

            /*
            if(string.IsNullOrEmpty(Driver?.Title))
            {
                titulo = "";
            }
            else
            {
                titulo = Driver.Title;
            }

            string titulo = string.IsNullOrEmpty(Driver?.Title) ? "" : Driver.Title;
             */

            string tituloActual = Driver?.Title ?? "";
            bool contieneYourStore = tituloActual.Contains("Your Store");

            Console.WriteLine($"Título de la página: {tituloActual}");
            Console.WriteLine($"¿Contiene 'Your Store'?: {(contieneYourStore ? "Sí" : "No")}");

            // Espera 5 segundos para ver el resultado (antes del Assert)
            //Thread.Sleep(5000); //Espera siempre 5 segundos, aunque el resultado ya se mostró. No es ideal porque puede hacer que los tests sean más lentos de lo necesario.

            // El Assert se ejecuta al final, después de ver el resultado
            Assert.IsTrue(contieneYourStore, $"El título '{tituloActual}' NO contiene 'Your Store'");
        }

        [TestMethod]
        //Conexión a la página de OpenCart y búsqueda de elementos de la página de login
        public void Ejercicio2()
        {
            Driver.Navigate().GoToUrl("http://opencart.abstracta.us/index.php?route=account/login");

            // Buscar el input de email por ID
            IWebElement emailInput = Driver.FindElement(By.Id("input-email-asd"));

            // Verificar si el elemento fue encontrado y está visible
            bool seEncontroEmailInput = emailInput != null && emailInput.Displayed;

            Console.WriteLine($"¿Se encontró el input de email?: {(seEncontroEmailInput ? "Sí" : "No")}");

            // Espera 5 segundos para ver el resultado (antes del Assert)
            // Thread.Sleep(5000);

            // El Assert se ejecuta al final, después de ver el resultado
            Assert.IsTrue(seEncontroEmailInput, "No se encontró el input de email en la página de login");

            // Buscar por nombre la password
            IWebElement passField = Driver.FindElement(By.Name("password"));

            // Verificar si el elemento fue encontrado y está visible
            bool seEncontroPassword = passField != null && passField.Displayed;

            Console.WriteLine($"¿Se encontró el campo de password?: {(seEncontroPassword ? "Sí" : "No")}");

            Assert.IsTrue(seEncontroPassword, "No se encontró el elemento password");
            // Buscar por CssSelector el botón de Login
            IWebElement loginBtn = Driver.FindElement(By.CssSelector("input[value='Login']"));

            // Verificar si el botón de Login fue encontrado y está visible
            bool seEncontroLogin = loginBtn != null && loginBtn.Displayed;

            Console.WriteLine($"¿Se encontró el campo de password?: {(seEncontroLogin ? "Sí" : "No")}");

            Assert.IsTrue(seEncontroLogin, "No se encontró el botón de login");

            // Buscar por LinkText el link de "Forgotten Password"
            IWebElement forgotLink = Driver.FindElement(By.LinkText("Forgotten Password"));

            // Verificar si encontró el link de "Forgotten Password" y está visible

            bool seEncontroLink = forgotLink != null && forgotLink.Displayed;

            Console.WriteLine($"¿Se encontró el link de Forgotten Password?: {(seEncontroLink ? "Sí" : "No")}");

            Assert.IsTrue(seEncontroLogin, "No se encontró el botón de login");

            IWebElement btnCart = Driver.FindElement(By.XPath("//*[@id=\"cart\"]/button"));

        }
        [TestMethod]
        public void Ejercicio3()
        {
            Driver.Navigate().GoToUrl("http://opencart.abstracta.us/index.php?route=account/register");

            // Ingresar datos en First Name, Last Name, Email y Telephone.
            Driver.FindElement(By.Id("input-firstname")).SendKeys("Leila Sol");
            Driver.FindElement(By.Id("input-lastname")).SendKeys("Mattone");
            //driver.FindElement(By.Id("input-email")).SendKeys($"leila3@test.com");
            Driver.FindElement(By.Id("input-email")).SendKeys($"leila{DateTime.Now.Ticks}@test.com");
            Driver.FindElement(By.Id("input-telephone")).SendKeys("987654321");
            Driver.FindElement(By.Id("input-password")).SendKeys("Pass12345!");
            Driver.FindElement(By.Id("input-confirm")).SendKeys("Pass12345!");

            // Aceptar Privacy Policy
            Driver.FindElement(By.Name("agree")).Click();
            Driver.FindElement(By.CssSelector("input[value='Continue']")).Click();

            // Esperar 3 segundos para que cargue la página de confirmación
            Thread.Sleep(3000);

            // Verificar que la cuenta fue creada exitosamente
            StringAssert.Contains(Driver.PageSource, "Your Account Has Been Created");
            //Buscar si existe Div de Exito

            // Esperar 5 segundos para ver el resultado
            Thread.Sleep(5000);
        }
        [TestMethod]
        public void Ejercicio4()
        {
            Driver.Navigate().GoToUrl("https://opencart.abstracta.us/index.php?route=account/login");

            // Realizar el login
            Driver.FindElement(By.Id("input-email")).SendKeys("leila3@test.com");
            Driver.FindElement(By.Id("input-password")).SendKeys("Pass12345!");
            Driver.FindElement(By.CssSelector("input[value='Login']")).Click();

            // Esperar 3 segundos para que cargue la página después del login
            Thread.Sleep(3000);

            // Imprimir información de debugging
            Console.WriteLine($"URL actual: {Driver.Url}");
            Console.WriteLine($"¿Contiene error?: {Driver.PageSource.Contains("Warning")}");

            // Verificar login exitoso
            StringAssert.Contains(Driver.Url, "route=account/account", "El login falló - no se redirigió a la página de cuenta");
            StringAssert.Contains(Driver.PageSource, "My Account");

            // Logout
            Driver.FindElement(By.LinkText("Logout")).Click();
            Thread.Sleep(2000);

            StringAssert.Contains(Driver.PageSource, "logged off");

            // Esperar 5 segundos para ver el resultado
            Thread.Sleep(5000);
        }
        [TestMethod]
        public void Ejercicio5()
        {
            // Arrange o preparación
            Driver.Navigate().GoToUrl("https://opencart.abstracta.us/");

            IWebElement divSearch = Driver.FindElement(By.Id("search"));
            IWebElement txtSearch = divSearch.FindElement(By.TagName("input"));
            IWebElement btnSearch = divSearch.FindElement(By.TagName("button"));

            // Act o pasos de la prueba
            // Buscar el campo de búsqueda y escribir un término inexistente
            IWebElement searchBox = Driver.FindElement(By.Name("search"));
            searchBox.Clear();
            //searchBox.SendKeys("xyzabc999");
            searchBox.SendKeys("iPhone");

            // Hacer clic en el botón de búsqueda
            btnSearch.Click();

            // Esperar 3 segundos para que carguen los resultados
            Thread.Sleep(3000);

            // Assert o validaciones
            // Buscar elementos de productos en la página (usando el selector de productos)
            ReadOnlyCollection<IWebElement> productosEncontrados = Driver.FindElements(By.CssSelector(".product-thumb"));

            // Ahora esperamos que SÍ haya productos porque buscamos "iPhone"
            bool hayProductos = productosEncontrados.Count > 0;

            Console.WriteLine($"Cantidad de productos encontrados: {productosEncontrados.Count}");
            Console.WriteLine($"¿Se encontraron productos?: {(hayProductos ? "Sí, correcto" : "No, error")}");
            Console.WriteLine($"URL actual: {Driver.Url}");

            // Verificar que SÍ se encontraron productos (Count debe ser mayor a 0 para iPhone)
            Assert.IsTrue(hayProductos, $"Se esperaba encontrar productos de iPhone, pero se encontraron {productosEncontrados.Count}");

            // Esperar 5 segundos para ver los resultados
            Thread.Sleep(5000);
        }
        [TestMethod]
        public void Ejercicio6()
        {
            // Arrange o preparación
            Driver.Navigate().GoToUrl("https://opencart.abstracta.us/index.php?route=product/product&product_id=40");

            // Crear WebDriverWait con timeout de 10 segundos
            WebDriverWait wait = new(Driver, TimeSpan.FromSeconds(10));

            // Act o pasos de la prueba
            // Hacer clic en el botón Add to Cart
            Driver.FindElement(By.CssSelector("button.btn.btn-primary.btn-lg.btn-block")).Click();

            // Esperar a que aparezca el mensaje de éxito (alert-success)
            IWebElement alertaExito = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".alert-success")));

            // Assert o validaciones
            // Obtener el texto del mensaje de éxito
            string mensajeExito = alertaExito.Text;
            bool productoAgregado = mensajeExito.Contains("Success");

            Console.WriteLine($"Mensaje de éxito: {mensajeExito}");
            Console.WriteLine($"¿Producto agregado correctamente?: {(productoAgregado ? "Sí" : "No")}");
            Console.WriteLine($"URL actual: {Driver.Url}");

            // Verificar que el producto se agregó exitosamente al carrito
            Assert.IsTrue(productoAgregado, "No se mostró el mensaje de éxito al agregar el producto al carrito");

            // Verificar que el contador del carrito en el header cambió a 1 item
            IWebElement totalCarrito = Driver.FindElement(By.Id("cart-total"));
            string textoCarrito = totalCarrito.Text;
            bool hayUnItem = textoCarrito.Contains("1 item") || textoCarrito.Contains("1 Item");

            Console.WriteLine($"Texto del carrito: {textoCarrito}");
            Console.WriteLine($"¿Hay 1 item en el carrito?: {(hayUnItem ? "Sí" : "No")}");

            // Verificar que el carrito muestra 1 item
            Assert.IsTrue(hayUnItem, $"Se esperaba '1 item' en el carrito, pero se encontró: {textoCarrito}");

            // Esperar 5 segundos para ver los resultados
            Thread.Sleep(5000);
        }

        // LoginPage
        public class LoginPage
        {
            private IWebDriver driver;
            private const string URL_LOGIN = "https://opencart.abstracta.us/index.php?route=account/login";

            public LoginPage(IWebDriver driver)
            {
                this.driver = driver;
            }

            // Método para navegar a la página de login -> Es parte de los TESTs, no del POM. El POM solo debería tener métodos relacionados con la interacción con la página, no con la navegación general del sitio.
            public void IrAPaginaLogin()
            {
                driver.Navigate().GoToUrl(URL_LOGIN);
            }

            public void IngresarEmail(string email)
            {
                driver.FindElement(By.Id("input-email")).SendKeys(email);
            }
            public void IngresarPassword(string password)
            {
                driver.FindElement(By.Id("input-password")).SendKeys(password);
            }
            public void HacerClickLogin()
            {
                driver.FindElement(By.CssSelector("input[value='Login']")).Click();
            }

            public void Login(string email, string password)
            {
                IngresarEmail(email);
                IngresarPassword(password);
                HacerClickLogin();
            }
        }

        [TestMethod]
        // Test usando LoginPom
        public void Ejercicio7()
        {
            // Crear instancia del Page Object Model para Login
            var logPom = new LoginPage(Driver);

            // Navegar a la página de login usando el POM
            logPom.IrAPaginaLogin();

            // Realizar el login usando el método del POM
            logPom.Login("leila3@test.com", "Pass12345!");

            // Esperar 3 segundos para que cargue la página después del login
            Thread.Sleep(3000);

            // Imprimir información de debugging
            Console.WriteLine($"URL actual: {Driver.Url}");
            Console.WriteLine($"¿Contiene error?: {Driver.PageSource.Contains("Warning")}");

            // Verificar login exitoso
            StringAssert.Contains(Driver.Url, "route=account/account", "El login falló - no se redirigió a la página de cuenta");
            StringAssert.Contains(Driver.PageSource, "My Account");

            // Logout
            Driver.FindElement(By.LinkText("Logout")).Click();
            Thread.Sleep(2000);

            StringAssert.Contains(Driver.PageSource, "logged off");
        }
        [TestMethod]
        // Test usando WebDriverWait con ElementToBeClickable - Búsqueda y agregar producto
        public void Ejercicio8()
        {
            // Arrange o preparación - Navegar a la página principal
            Driver.Navigate().GoToUrl("https://opencart.abstracta.us/");

            // Crear WebDriverWait con timeout de 10 segundos
            WebDriverWait wait = new(Driver, TimeSpan.FromSeconds(10));

            // Act - Paso 1: Buscar "Mac"
            // Esperar a que el campo de búsqueda sea clickeable
            IWebElement searchBox = wait.Until(ExpectedConditions.ElementToBeClickable(By.Name("search")));
            searchBox.Clear();
            searchBox.SendKeys("Mac");

            Console.WriteLine("Término de búsqueda ingresado: Mac");

            // Esperar a que el botón de búsqueda sea clickeable y hacer clic
            IWebElement botonBuscar = wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("button.btn.btn-default.btn-lg")));
            botonBuscar.Click();

            Console.WriteLine("Botón de búsqueda clickeado");

            // Act - Paso 2: Esperar a que aparezcan los resultados y hacer clic en el primer producto
            // Esperar a que aparezca al menos un producto
            wait.Until(driver => driver.FindElements(By.CssSelector(".product-thumb h4 a")).Count > 0);

            // Obtener el primer producto de la lista
            IWebElement primerProducto = wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(".product-thumb h4 a")));
            string nombreProducto = primerProducto.Text;

            Console.WriteLine($"Primer producto encontrado: {nombreProducto}");

            primerProducto.Click();

            // Act - Paso 3: Agregar el producto al carrito
            // Esperar a que el botón "Add to Cart" sea clickeable
            IWebElement botonAddToCart = wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("button.btn.btn-primary.btn-lg.btn-block")));
            botonAddToCart.Click();

            Console.WriteLine("Producto agregado al carrito");

            // Esperar a que aparezca el mensaje de éxito (alert-success)
            IWebElement alertaExito = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".alert-success")));

            // Assert o validaciones
            // Obtener el texto del mensaje de éxito
            string mensajeExito = alertaExito.Text;
            bool productoAgregado = mensajeExito.Contains("Success");

            Console.WriteLine($"Mensaje de éxito: {mensajeExito}");
            Console.WriteLine($"¿Producto agregado correctamente?: {(productoAgregado ? "Sí" : "No")}");
            Console.WriteLine($"URL actual: {Driver.Url}");

            // Verificar que el producto se agregó exitosamente al carrito
            Assert.IsTrue(productoAgregado, "No se mostró el mensaje de éxito al agregar el producto al carrito");

            // Verificar que el contador del carrito en el header cambió a 1 item
            IWebElement totalCarrito = Driver.FindElement(By.Id("cart-total"));
            string textoCarrito = totalCarrito.Text;
            bool hayUnItem = textoCarrito.Contains("1 item") || textoCarrito.Contains("1 Item");

            Console.WriteLine($"Texto del carrito: {textoCarrito}");
            Console.WriteLine($"¿Hay 1 item en el carrito?: {(hayUnItem ? "Sí" : "No")}");

            // Verificar que el carrito muestra 1 item
            Assert.IsTrue(hayUnItem, $"Se esperaba '1 item' en el carrito, pero se encontró: {textoCarrito}");


        }
        [TestMethod]
        // Test - Búsqueda y agregar 2 productos diferentes
        public void Ejercicio9()
        {
            // Arrange o preparación - Navegar a la página principal
            Driver.Navigate().GoToUrl("https://opencart.abstracta.us/");

            // Crear WebDriverWait con timeout de 10 segundos
            WebDriverWait wait = new(Driver, TimeSpan.FromSeconds(10));

            // Act - Paso 1: Buscar "Mac"
            // Esperar a que el campo de búsqueda sea clickeable
            IWebElement searchBox = wait.Until(ExpectedConditions.ElementToBeClickable(By.Name("search")));
            searchBox.Clear();
            searchBox.SendKeys("Mac");

            Console.WriteLine("Término de búsqueda ingresado: Mac");

            // Esperar a que el botón de búsqueda sea clickeable y hacer clic
            IWebElement botonBuscar = wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("button.btn.btn-default.btn-lg")));
            botonBuscar.Click();

            Console.WriteLine("Botón de búsqueda clickeado");

            // Act - Paso 2: Esperar a que aparezcan los resultados y hacer clic en el primer producto
            // Esperar a que aparezca al menos un producto
            wait.Until(driver => driver.FindElements(By.CssSelector(".product-thumb h4 a")).Count > 0);

            // Obtener el primer producto de la lista
            IWebElement primerProducto = wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(".product-thumb h4 a")));
            string nombreProducto = primerProducto.Text;

            Console.WriteLine($"Primer producto encontrado: {nombreProducto}");

            primerProducto.Click();

            // Act - Paso 3: Agregar el producto al carrito
            // Esperar a que el botón "Add to Cart" sea clickeable
            IWebElement botonAddToCart = wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("button.btn.btn-primary.btn-lg.btn-block")));
            botonAddToCart.Click();

            Console.WriteLine("Producto agregado al carrito");

            // Esperar a que aparezca el mensaje de éxito (alert-success)
            IWebElement alertaExito = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".alert-success")));

            // Assert o validaciones
            // Obtener el texto del mensaje de éxito
            string mensajeExito = alertaExito.Text;
            bool productoAgregado = mensajeExito.Contains("Success");

            Console.WriteLine($"Mensaje de éxito: {mensajeExito}");
            Console.WriteLine($"¿Producto agregado correctamente?: {(productoAgregado ? "Sí" : "No")}");
            Console.WriteLine($"URL actual: {Driver.Url}");

            // Verificar que el producto se agregó exitosamente al carrito
            Assert.IsTrue(productoAgregado, "No se mostró el mensaje de éxito al agregar el producto al carrito");

            // Verificar que el contador del carrito en el header cambió a 1 item
            IWebElement totalCarrito = Driver.FindElement(By.Id("cart-total"));
            string textoCarrito = totalCarrito.Text;
            bool hayUnItem = textoCarrito.Contains("1 item") || textoCarrito.Contains("1 Item");

            Console.WriteLine($"Texto del carrito: {textoCarrito}");
            Console.WriteLine($"¿Hay 1 item en el carrito?: {(hayUnItem ? "Sí" : "No")}");

            // Verificar que el carrito muestra 1 item
            Assert.IsTrue(hayUnItem, $"Se esperaba '1 item' en el carrito, pero se encontró: {textoCarrito}");

            // ===== AGREGAR SEGUNDO PRODUCTO: iPhone =====
            Console.WriteLine("\n--- Iniciando búsqueda del segundo producto ---");

            // Volver a la página principal para buscar otro producto
            Driver.Navigate().GoToUrl("https://opencart.abstracta.us/");

            // Act - Paso 4: Buscar "iphone"
            IWebElement searchBox2 = wait.Until(ExpectedConditions.ElementToBeClickable(By.Name("search")));
            searchBox2.Clear();
            searchBox2.SendKeys("iphone");

            Console.WriteLine("Término de búsqueda ingresado: iphone");

            // Hacer clic en el botón de búsqueda
            IWebElement botonBuscar2 = wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("button.btn.btn-default.btn-lg")));
            botonBuscar2.Click();

            Console.WriteLine("Botón de búsqueda clickeado para iPhone");

            // Act - Paso 5: Esperar a que aparezcan los resultados y hacer clic en el primer iPhone
            wait.Until(driver => driver.FindElements(By.CssSelector(".product-thumb h4 a")).Count > 0);

            IWebElement primerIPhone = wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(".product-thumb h4 a")));
            string nombreIPhone = primerIPhone.Text;

            Console.WriteLine($"Primer iPhone encontrado: {nombreIPhone}");

            primerIPhone.Click();

            // Act - Paso 6: Agregar el iPhone al carrito
            IWebElement botonAddToCart2 = wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("button.btn.btn-primary.btn-lg.btn-block")));
            botonAddToCart2.Click();

            Console.WriteLine("iPhone agregado al carrito");

            // Esperar a que aparezca el mensaje de éxito
            IWebElement alertaExito2 = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".alert-success")));

            // Assert o validaciones para el segundo producto
            string mensajeExito2 = alertaExito2.Text;
            bool segundoProductoAgregado = mensajeExito2.Contains("Success");

            Console.WriteLine($"Mensaje de éxito (iPhone): {mensajeExito2}");
            Console.WriteLine($"¿iPhone agregado correctamente?: {(segundoProductoAgregado ? "Sí" : "No")}");

            // Verificar que el segundo producto se agregó exitosamente
            Assert.IsTrue(segundoProductoAgregado, "No se mostró el mensaje de éxito al agregar el iPhone al carrito");

            // Verificar que el contador del carrito ahora muestra 2 items
            IWebElement totalCarrito2 = Driver.FindElement(By.Id("cart-total"));
            string textoCarrito2 = totalCarrito2.Text;
            bool hayDosItems = textoCarrito2.Contains("2 item") || textoCarrito2.Contains("2 Item");

            Console.WriteLine($"Texto del carrito final: {textoCarrito2}");
            Console.WriteLine($"¿Hay 2 items en el carrito?: {(hayDosItems ? "Sí" : "No")}");

            // Verificar que el carrito muestra 2 items
            Assert.IsTrue(hayDosItems, $"Se esperaba '2 items' en el carrito, pero se encontró: {textoCarrito2}");

            // ===== VERIFICAR PRODUCTOS EN LA TABLA DEL CARRITO =====
            Console.WriteLine("\n--- Verificando productos en la tabla del carrito ---");

            // Navegar al carrito para ver la tabla de productos
            Driver.FindElement(By.Id("cart-total")).Click();
            Thread.Sleep(1000);

            // Hacer clic en "View Cart" para ver el carrito completo
            IWebElement viewCartButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.LinkText("View Cart")));
            viewCartButton.Click();

            Console.WriteLine("Navegado al carrito");

            // Esperar a que cargue la tabla del carrito
            Thread.Sleep(2000);

            // Obtener todas las filas de la tabla de productos que contienen una imagen de producto
            // Esto filtra las filas que realmente son productos (excluye subtotales, shipping, etc.)
            var todasLasFilas = Driver.FindElements(By.CssSelector(".table.table-bordered tbody tr"));

            // Filtrar solo las filas que contienen productos (tienen una celda con imagen)
            var filasProductos = todasLasFilas.Where(fila =>
            {
                try
                {
                    // Verificar si la fila tiene una imagen de producto en la primera celda
                    fila.FindElement(By.CssSelector("td:first-child img"));
                    return true;
                }
                catch
                {
                    return false;
                }
            }).ToList();

            Console.WriteLine($"Total de filas en tabla: {todasLasFilas.Count}");
            Console.WriteLine($"Filas de productos encontradas: {filasProductos.Count}");

            // Verificar que hay exactamente 2 productos
            Assert.AreEqual(2, filasProductos.Count, $"Se esperaban 2 productos en la tabla del carrito, pero se encontraron {filasProductos.Count}");

            // Para cada fila, extraer y verificar información
            int contador = 1;
            foreach(var fila in filasProductos)
            {
                try
                {
                    // Extraer nombre del producto (segunda columna - td[2])
                    var celdaNombre = fila.FindElement(By.CssSelector("td:nth-child(2)"));
                    string nombreEnCarrito = celdaNombre.Text;

                    // Extraer cantidad (cuarta columna - td[4])
                    var celdaCantidad = fila.FindElement(By.CssSelector("td:nth-child(4) input"));
                    string cantidadEnCarrito = celdaCantidad.GetAttribute("value");

                    // Extraer precio unitario (quinta columna - td[5])
                    var celdaPrecioUnitario = fila.FindElement(By.CssSelector("td:nth-child(5)"));
                    string precioUnitario = celdaPrecioUnitario.Text;

                    // Extraer precio total (sexta columna - td[6])
                    var celdaPrecioTotal = fila.FindElement(By.CssSelector("td:nth-child(6)"));
                    string precioTotal = celdaPrecioTotal.Text;

                    Console.WriteLine($"\nProducto {contador}:");
                    Console.WriteLine($"  Nombre: {nombreEnCarrito}");
                    Console.WriteLine($"  Cantidad: {cantidadEnCarrito}");
                    Console.WriteLine($"  Precio Unitario: {precioUnitario}");
                    Console.WriteLine($"  Precio Total: {precioTotal}");

                    // Verificación 1: Verificar que el nombre del producto NO está vacío
                    Assert.IsFalse(string.IsNullOrWhiteSpace(nombreEnCarrito),
                        $"El nombre del producto {contador} está vacío");

                    // Verificación 2: Verificar que la cantidad inicial de cada producto es 1
                    Assert.AreEqual("1", cantidadEnCarrito,
                        $"Se esperaba cantidad '1' para el producto {contador} ({nombreEnCarrito}), pero se encontró '{cantidadEnCarrito}'");

                    // Verificación adicional: Verificar que el precio no está vacío
                    Assert.IsFalse(string.IsNullOrWhiteSpace(precioUnitario),
                        $"El precio del producto {contador} ({nombreEnCarrito}) está vacío");

                    contador++;
                }
                catch(Exception ex)
                {
                    Console.WriteLine($"Error al procesar fila {contador}: {ex.Message}");
                    throw;
                }
            }

            Console.WriteLine($"\n✓ Verificación completada: {filasProductos.Count} productos verificados correctamente");

            // Esperar 5 segundos para ver los resultados finales
            Thread.Sleep(5000);
        }
        [TestCleanup]
        //Cierre de conexión
        public void TearDown()
        {
            Driver.Quit();
        }
    }

    // ===== EJERCICIO 10: Captura de Screenshots en Tests Fallidos =====
    [TestClass]
    public class Ejercicio10
    {
        private IWebDriver driver;

        // Propiedad pública TestContext requerida por MSTest
        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void Setup()
        {
            var options = new ChromeOptions();
            options.AddArgument("--disable-blink-features=AutomationControlled");
            options.AddArgument("--start-maximized");
            options.AddExcludedArgument("enable-automation");
            options.AddAdditionalOption("useAutomationExtension", false);
            options.AcceptInsecureCertificates = true;
            options.AddArgument("--ignore-certificate-errors");
            options.AddArgument("--ignore-ssl-errors");
            driver = new ChromeDriver(options);
        }

        [TestCleanup]
        public void TearDown()
        {
            try
            {
                // Verificar el resultado del test
                if(TestContext.CurrentTestOutcome == UnitTestOutcome.Failed)
                {
                    Console.WriteLine($"\n⚠️ Test '{TestContext.TestName}' FALLÓ - Capturando screenshot...");

                    // Capturar screenshot usando ITakesScreenshot
                    ITakesScreenshot screenshotDriver = (ITakesScreenshot)driver;
                    Screenshot screenshot = screenshotDriver.GetScreenshot();

                    // Crear nombre de archivo con nombre del test y timestamp
                    string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                    string nombreTest = TestContext.TestName;
                    string nombreArchivo = $"Screenshot_{nombreTest}_{timestamp}.png";

                    // Crear directorio para screenshots si no existe
                    string directorioScreenshots = Path.Combine(TestContext.TestRunDirectory, "Screenshots");
                    if(!Directory.Exists(directorioScreenshots))
                    {
                        Directory.CreateDirectory(directorioScreenshots);
                    }

                    // Ruta completa del archivo
                    string rutaCompleta = Path.Combine(directorioScreenshots, nombreArchivo);

                    // Guardar el screenshot
                    screenshot.SaveAsFile(rutaCompleta);

                    Console.WriteLine($"✓ Screenshot guardado en: {rutaCompleta}");

                    // Adjuntar el archivo al reporte de pruebas
                    TestContext.AddResultFile(rutaCompleta);

                    Console.WriteLine($"✓ Screenshot adjuntado al reporte de pruebas");
                }
                else
                {
                    Console.WriteLine($"\n✓ Test '{TestContext.TestName}' completado exitosamente - No se requiere screenshot");
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"❌ Error al capturar screenshot: {ex.Message}");
            }
            finally
            {
                // Cerrar el navegador
                driver?.Quit();
            }
        }

        [TestMethod]
        public void Test_Exitoso()
        {
            // Este test debería pasar y NO generar screenshot
            driver.Navigate().GoToUrl("https://opencart.abstracta.us/");

            string tituloActual = driver.Title;
            bool contieneYourStore = tituloActual.Contains("Your Store");

            Console.WriteLine($"Título de la página: {tituloActual}");
            Console.WriteLine($"¿Contiene 'Your Store'?: {(contieneYourStore ? "Sí" : "No")}");

            // Este Assert debería pasar
            Assert.IsTrue(contieneYourStore, $"El título '{tituloActual}' NO contiene 'Your Store'");

            Thread.Sleep(2000);
        }

        [TestMethod]
        public void Test_IntencionalmenteFallido()
        {
            // Este test está diseñado para FALLAR y generar un screenshot
            driver.Navigate().GoToUrl("https://opencart.abstracta.us/");

            Console.WriteLine("⚠️ ESTE TEST FALLARÁ INTENCIONALMENTE PARA DEMOSTRAR LA CAPTURA DE SCREENSHOTS");

            // Esperar a que cargue la página
            Thread.Sleep(2000);

            string tituloActual = driver.Title;
            Console.WriteLine($"Título actual de la página: {tituloActual}");

            // Este Assert FALLARÁ intencionalmente para disparar la captura de screenshot
            Assert.IsTrue(tituloActual.Contains("Texto Inexistente XYZ123"),
                "Este Assert falla intencionalmente para demostrar la captura de screenshots cuando un test falla");
        }

        [TestMethod]
        public void Test_LoginFallidoConScreenshot()
        {
            // Test que intenta hacer login con credenciales inválidas
            driver.Navigate().GoToUrl("https://opencart.abstracta.us/index.php?route=account/login");

            Thread.Sleep(2000);

            // Intentar login con credenciales incorrectas
            driver.FindElement(By.Id("input-email")).SendKeys("usuario_inexistente@test.com");
            driver.FindElement(By.Id("input-password")).SendKeys("ContraseñaIncorrecta123");
            driver.FindElement(By.CssSelector("input[value='Login']")).Click();

            Thread.Sleep(2000);

            // Este Assert debería fallar porque las credenciales son incorrectas
            // y NO debería redirigir a la página de cuenta
            Assert.IsTrue(driver.Url.Contains("route=account/account"),
                "Login falló - Las credenciales incorrectas no permitieron el acceso");
        }
    }
}
