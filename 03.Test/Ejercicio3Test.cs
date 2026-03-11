using System;
using System.Collections.Generic;
using System.Text;
using EjerciciosTest.Test;
using OpenQA.Selenium;

namespace EjerciciosTest._03.Test
{
    [TestClass]
    public class Ejercicio3Test : Test_Base
    {
        [TestMethod]
        public void Test_Registrarse()
        {
           
            string urlRegistrarse = "http://opencart.abstracta.us/index.php?route=account/register";

            // Act
            Driver?.Navigate().GoToUrl(urlRegistrarse);


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

            // Verificar que la cuenta fue creada exitosamente
            StringAssert.Contains(Driver.PageSource, "Your Account Has Been Created");
            //Buscar si existe Div de Exito
        }    
    }
}
