using EjerciciosTest.Test;

namespace EjerciciosTest._03.Test
{
    [TestClass]
    public class Ejercicio1Test : Test_Base
    {
        [TestMethod]
        //Conexión a la página de OpenCart y búsqueda de "Your Store"
        public void Test_Ejercicio1()
        {
            // Arrange
            string urlEsperada = "http://opencart.abstracta.us/";

            // Verificar que el Driver fue inicializado correctamente
            Assert.IsNotNull(Driver, "El Driver no se inicializó correctamente en Setup()");

            // Act
            Driver.Navigate().GoToUrl(urlEsperada);
            string tituloActual = Driver.Title ?? "";
            bool contieneYourStore = tituloActual.Contains("Your Store");

            Console.WriteLine($"Título de la página: {tituloActual}");
            Console.WriteLine($"¿Contiene 'Your Store'?: {(contieneYourStore ? "Sí" : "No")}");

            // Assert
            Assert.IsTrue(contieneYourStore, $"El título '{tituloActual}' NO contiene 'Your Store'");
        }
    }
}
