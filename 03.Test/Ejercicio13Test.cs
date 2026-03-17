using EjerciciosTest.Pages;
using EjerciciosTest.Test;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace EjerciciosTest.Test
{
    [TestClass]
    // Ejercicio 13 – Manejar Dropdowns con SelectElement
    public class Ejercicio13Test : Test_Base
    {
        [TestMethod]
        public void Test_DropDown()
        {
            /*  Navegar al carrito con al menos un producto.
                Localizar el dropdown de países (id = "input-country") en la sección "Estimate Shipping & Taxes".
                Crear una instancia de SelectElement pasándole el elemento.
                Usar SelectByText("Argentina") para seleccionar por texto visible.
                Verificar con SelectedOption.Text que la opción correcta quedó seleccionada.
                Obtener todas las opciones con Options e imprimir la cantidad.*/

            // ==================== ARRANGE ====================
            Console.WriteLine($"✓ Página principal cargada automáticamente: {Driver?.Url}");
            SearchPage searchPage = new(Driver);
            Product productPage   = new(Driver);
            CartPage cartPage     = new(Driver);

            // ==================== ACT ====================
            // Paso 1: Agregar un producto al carrito para habilitar la sección Estimate Shipping & Taxes
            Console.WriteLine("✓ Buscando iPhone...");
            searchPage.SearchProduct("iPhone");
            Thread.Sleep(1000);
            productPage.ClickBtnAddCart();
            bool exitoAgregar = cartPage.EsperarYVerificarAlertaExito();
            Assert.IsTrue(exitoAgregar, "No apareció el alert de éxito al agregar iPhone al carrito");
            Console.WriteLine("✓ iPhone agregado al carrito");

            // Paso 2: Navegar a la página del carrito
            Driver!.Navigate().GoToUrl(UrlPrincipal + "/index.php?route=checkout/cart");
            Thread.Sleep(1000);
            Console.WriteLine($"✓ Página del carrito abierta. URL: {Driver.Url}");

            // Paso 3: Expandir la sección "Estimate Shipping & Taxes" (está colapsada por defecto)
            IWebElement toggleShipping = Driver.FindElement(
                By.XPath("//a[contains(text(),'Estimate Shipping')]"));
            toggleShipping.Click();
            Thread.Sleep(800);
            Console.WriteLine("✓ Sección 'Estimate Shipping & Taxes' expandida");

            // Paso 4: Localizar el dropdown de países y crear SelectElement
            IWebElement dropdownPais = Driver.FindElement(By.Id("input-country"));
            SelectElement selectPais = new(dropdownPais);
            Console.WriteLine($"✓ Dropdown de países localizado. Opciones disponibles: {selectPais.Options.Count}");

            // Paso 5: Seleccionar "Argentina" por texto visible
            selectPais.SelectByText("Argentina");
            Thread.Sleep(500);
            Console.WriteLine("✓ 'Argentina' seleccionada en el dropdown");

            // ==================== ASSERT ====================
            // Paso 6: Verificar que la opción seleccionada es "Argentina"
            string opcionSeleccionada = selectPais.SelectedOption.Text;
            Assert.AreEqual("Argentina", opcionSeleccionada,
                $"La opción seleccionada debería ser 'Argentina'. Actual: '{opcionSeleccionada}'");
            Console.WriteLine($"✓ Assert OK: opción seleccionada = '{opcionSeleccionada}'");

            // Paso 7: Verificar cantidad total de opciones
            int totalOpciones = selectPais.Options.Count;
            Assert.IsTrue(totalOpciones > 0, "El dropdown debería tener al menos una opción");
            Console.WriteLine($"✓ Assert OK: dropdown contiene {totalOpciones} países disponibles");
        }
    }
}
