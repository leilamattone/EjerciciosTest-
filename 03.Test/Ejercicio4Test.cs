using EjerciciosTest.Test;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Net.WebRequestMethods;

namespace EjerciciosTest._03.Test
{
    [TestClass]
    public class Ejercicio4Test : Test_Base
    {
        [TestMethod]
        public void Test_Loguearse()
        {
            // Arrange
            string urlLoguearse = "https://opencart.abstracta.us/index.php?route=account/login";

            // Act - Navegar a la página de login
            Driver?.Navigate().GoToUrl(urlLoguearse);

            // Act - Realizar el login
            Driver.FindElement(By.Id("input-email")).SendKeys("leila3@test.com");
            Driver.FindElement(By.Id("input-password")).SendKeys("Pass12345!");
            Driver.FindElement(By.CssSelector("input[value='Login']")).Click();

            // Esperar 3 segundos para que cargue la página después del login
            Thread.Sleep(3000);

            // Assert - Verificar login exitoso
            Console.WriteLine($"URL actual: {Driver.Url}");
            Console.WriteLine($"¿Contiene error?: {Driver.PageSource.Contains("Warning")}");

            StringAssert.Contains(Driver.Url, "route=account/account", "El login falló - no se redirigió a la página de cuenta");
            StringAssert.Contains(Driver.PageSource, "My Account");

            // Act - Logout
            Driver.FindElement(By.LinkText("Logout")).Click();

            // Assert - Verificar logout exitoso
            StringAssert.Contains(Driver.PageSource, "logged off");
        }
    }
}
