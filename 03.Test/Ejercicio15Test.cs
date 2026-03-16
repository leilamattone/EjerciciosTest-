using EjerciciosTest.Pages;
using EjerciciosTest.Test;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Collections.ObjectModel;
using static EjerciciosTest.Test1;

namespace EjerciciosTest.Test
{
    [TestClass]
    public class Ejercicio15Test : Test_Base
    {
        [TestMethod]
        //Ejercicio 15 – Verificar Atributos HTML, CSS y Estados de Elementos
        public void Test_VerificarAtributos()
        {

            /* Navegar a la página de login.

                Verificar con GetAttribute("type") que el campo password es de tipo "password".

                Verificar con GetAttribute("type") que el campo email es de tipo "email".

                Verificar con GetCssValue("background-color") el color de fondo del campo email.

                Navegar al registro, verificar que el checkbox no está marcado inicialmente.

                Hacer clic y verificar que el checkbox ahora está marcado.*/

            // ==================== ARRANGE ====================
            Console.WriteLine($"✓ Página principal cargada automáticamente: {Driver?.Url}");

            // Crear instancias de SearchPage, Product y CartPage
            var loginPage = new Pages.LoginPage(Driver);
            var searchPage = new SearchPage(Driver);
            var productPage = new Product(Driver);
            var cartPage = new CartPage(Driver);

            //  Navegar a la página de login
            loginPage.HacerClickMyAccount();
            Thread.Sleep(500);
            loginPage.HacerClickLinkLogin();
            Thread.Sleep(1000);

            // ==================== ACT ====================
            // Paso 1 - Verificar con GetDomAttribute("type") que el campo password es de tipo "password".
            Assert.AreEqual("password", loginPage.EnterPassInput.GetDomAttribute("type"), "El campo password no es de tipo 'password'");

            // Paso 2 - Verificar con GetDomAttribute("type") que el campo email es de tipo "text".
            Assert.AreEqual("text", loginPage.EmailInput.GetDomAttribute("type"), "El campo email no es de tipo 'text'");

            // Paso 3 - Verificar con GetCssValue("background-color") el color de fondo del campo email.
            String colorFondoEmail = loginPage.EmailInput.GetCssValue("background-color");
            Console.WriteLine($"✓ El background color del email input es: {colorFondoEmail}");
            Assert.IsTrue(colorFondoEmail.Contains("rgba(255, 255, 255"), $"El color de fondo del campo email no es el esperado. Color actual: {colorFondoEmail}");

            // Paso 4 - Navegar al registro, verificar que el checkbox no está marcado inicialmente.
            loginPage.HacerClickRegister();
            Thread.Sleep(500);
            Assert.IsFalse(loginPage.BtnCheckBox.Selected, "El checkbox debería estar desmarcado inicialmente");
            loginPage.HacerClickBtnCheckBox();

            // ==================== ASSERT - VERIFICAR CHECKBOX MARCADO ====================
            Assert.IsTrue(loginPage.BtnCheckBox.Selected, "El checkbox debe estar marcado luego del click");



        }
    }
}
