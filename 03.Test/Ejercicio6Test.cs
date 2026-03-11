using EjerciciosTest.Test;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using static System.Net.WebRequestMethods;

namespace EjerciciosTest._03.Test
{
    [TestClass]
    public class Ejercicio6Test : Test_Base
    {
        [TestMethod]
        public void Test_AgregarProductoAlCarrito()
        {
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
        }
    }
}
