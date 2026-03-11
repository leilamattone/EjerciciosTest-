using EjerciciosTest.Test;
using OpenQA.Selenium;

namespace EjerciciosTest._03.Test
{
    [TestClass]
    public class Ejercicio2Test : Test_Base
    {
        [TestMethod]
        //Conexión a la página de OpenCart y búsqueda de elementos de la página de login
        public void Test_BuscarElementosLoginPage()
        {
            // Arrange
            string urlLogin = "http://opencart.abstracta.us/index.php?route=account/login";

            // Act
            Driver?.Navigate().GoToUrl(urlLogin);

            // Buscar el input de email por ID
            IWebElement emailInput = Driver.FindElement(By.Id("input-email"));

            // Verificar si el elemento fue encontrado y está visible
            bool seEncontroEmailInput = emailInput != null && emailInput.Displayed;

            Console.WriteLine($"¿Se encontró el input de email?: {(seEncontroEmailInput ? "Sí" : "No")}");

            // Assert - Verificar input de email
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

            Console.WriteLine($"¿Se encontró el botón de login?: {(seEncontroLogin ? "Sí" : "No")}");

            Assert.IsTrue(seEncontroLogin, "No se encontró el botón de login");

            // Buscar por LinkText el link de "Forgotten Password"
            IWebElement forgotLink = Driver.FindElement(By.LinkText("Forgotten Password"));

            // Verificar si encontró el link de "Forgotten Password" y está visible
            bool seEncontroLink = forgotLink != null && forgotLink.Displayed;

            Console.WriteLine($"¿Se encontró el link de Forgotten Password?: {(seEncontroLink ? "Sí" : "No")}");

            Assert.IsTrue(seEncontroLink, "No se encontró el link de Forgotten Password");

            // Buscar el botón del carrito por XPath
            IWebElement btnCart = Driver.FindElement(By.XPath("//*[@id=\"cart\"]/button"));

            // Verificar si encontró el botón del carrito
            bool seEncontroCarrito = btnCart != null && btnCart.Displayed;

            Console.WriteLine($"¿Se encontró el botón del carrito?: {(seEncontroCarrito ? "Sí" : "No")}");

            Assert.IsTrue(seEncontroCarrito, "No se encontró el botón del carrito");
        }
    }
}