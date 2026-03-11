using EjerciciosTest.Test;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using static System.Net.WebRequestMethods;

namespace EjerciciosTest._03.Test
{
    [TestClass]
    public class Ejercicio5Test : Test_Base
    {
        [TestMethod]
        public void Test_Loguearse()
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
            searchBox.SendKeys("MacBook");

            // Hacer clic en el botón de búsqueda
            btnSearch.Click();

            // Esperar 3 segundos para que carguen los resultados
            Thread.Sleep(3000);

            // Assert o validaciones
            // Buscar elementos de productos en la página (usando el selector de productos)
            ReadOnlyCollection<IWebElement> productosEncontrados = Driver.FindElements(By.CssSelector(".product-thumb"));

            // Ahora esperamos que SÍ haya productos porque buscamos "MacBook"
            bool hayProductos = productosEncontrados.Count > 0;
             
            Console.WriteLine($"Cantidad de productos encontrados: {productosEncontrados.Count}");
            Console.WriteLine($"¿Se encontraron productos?: {(hayProductos ? "Sí, correcto" : "No, error")}");
            Console.WriteLine($"URL actual: {Driver.Url}");

            // Verificar que SÍ se encontraron productos (Count debe ser mayor a 0 para MacBook)
            Assert.IsTrue(hayProductos, $"Se esperaba encontrar productos de MacBook, pero se encontraron {productosEncontrados.Count}");
        }    
    }
}
