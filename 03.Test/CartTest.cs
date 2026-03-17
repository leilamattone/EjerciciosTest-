using EjerciciosTest.Pages;
using EjerciciosTest.Test;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace EjerciciosTest.Test
{
    [TestClass]
    public class CartTest : Test_Base
    {
        [TestMethod]
        public void Test_CartConPOM_OK()
        {

            // ==================== ARRANGE ====================
            Console.WriteLine($"✓ Página principal cargada automáticamente: {Driver?.Url}");

            SearchPage searchPage = new(Driver);
            Product productPage = new(Driver);
            CartPage cartPage = new(Driver);

            // ==================== ACT ====================
            // Paso 1: Buscar "iPhone" usando la barra de búsqueda
            Console.WriteLine("✓ Buscar \"iPhone\" usando la barra de búsqueda");
            searchPage.SearchProduct("iPhone");
            int cantidad = searchPage.GetCantidadProductos();
            Console.WriteLine($"✓ Productos encontrados: {cantidad}");
            Assert.AreEqual(1, cantidad, $"La búsqueda de 'iPhone' debería retornar exactamente 1 resultado. Encontrados: {cantidad}");
            Console.WriteLine("✓ Assert OK: iPhone encontrado en los resultados");

            // Paso 2: Agregar el iPhone al carrito y verificar que se muestra la confirmación
            Console.WriteLine("✓ Agregando iPhone al carrito...");
            productPage.ClickBtnAddCart();
            bool exitoConfirmado = cartPage.EsperarYVerificarAlertaExito();
            Assert.IsTrue(exitoConfirmado, "No apareció el mensaje de éxito al agregar el iPhone al carrito");
            Console.WriteLine("✓ Assert OK: iPhone agregado al carrito - alerta de éxito confirmada");

            // Paso 3: Navegar al carrito y verificar que el iPhone aparece
            Console.WriteLine("✓ Abriendo carrito para verificar iPhone...");
            cartPage.IngresarAlCarrito();
            Thread.Sleep(500);
            int itemsEnCarrito = cartPage.GetCantidadItemsCarrito();
            Assert.AreEqual(1, itemsEnCarrito, $"El carrito debería tener exactamente 1 producto (iPhone). Encontrados: {itemsEnCarrito}");
            Console.WriteLine($"✓ Assert OK: carrito tiene {itemsEnCarrito} producto(s)");
            cartPage.MostrarProductosCarrito();

            // Paso 4: Agregar Cámara al carrito y verificar que se actualiza el resumen
            Console.WriteLine("✓ Agregando Canon al carrito con color Blue, cantidad 2...");
            cartPage.AgregarCamera("Canon", 2, "blue");
            bool exitoConfirmado2 = cartPage.EsperarYVerificarAlertaExito();
            Assert.IsTrue(exitoConfirmado2, "No apareció el mensaje de éxito al agregar las cámaras al carrito");
            Console.WriteLine("✓ Assert OK: Canon agregada al carrito - alerta de éxito confirmada");

            // Mostrar carrito luego de agregar las cámaras
            Console.WriteLine("✓ Mostrando carrito tras agregar cámaras...");
            cartPage.MostrarProductosCarrito();
            int itemsTrasAgregarCamara = cartPage.GetCantidadItemsCarrito();
            Assert.AreEqual(2, itemsTrasAgregarCamara, $"El carrito debería tener 2 filas (iPhone + Canon). Encontradas: {itemsTrasAgregarCamara}");
            Console.WriteLine($"✓ Assert OK: carrito tiene {itemsTrasAgregarCamara} filas (iPhone + Canon)");

            // Paso 4b: Agregar Nikon al carrito y verificar que NO está disponible en stock
            Console.WriteLine("✓ Agregando Nikon al carrito (se espera alerta de stock insuficiente)...");
            cartPage.AgregarCamera("Nikon", 1, "0");
            bool exitoNikon = cartPage.EsperarYVerificarAlertaExito();
            Assert.IsTrue(exitoNikon, "OpenCart debería confirmar que Nikon fue agregado al carrito");
            Console.WriteLine("✓ Assert OK: Nikon agregado al carrito (success alert confirmado)");

            // Navegar al carrito y verificar que Nikon figura como no disponible
            Driver!.Navigate().GoToUrl(UrlPrincipal + "/index.php?route=checkout/cart");
            Thread.Sleep(1000);
            bool nikanDisponible = cartPage.ProductoDisponibleEnCarrito();
            Assert.IsFalse(nikanDisponible, "Se esperaba que Nikon NO estuviera disponible (alerta de stock insuficiente)");
            Console.WriteLine("✓ Assert OK: alerta 'Products marked with *** are not available' confirmada para Nikon");

            // Eliminar Nikon del carrito antes de continuar
            cartPage.EliminarProducto("Nikon");
            Thread.Sleep(500);
            Console.WriteLine("✓ Nikon eliminado del carrito");
            int itemsTrasEliminarNikon = cartPage.GetCantidadItemsCarrito();
            Assert.AreEqual(2, itemsTrasEliminarNikon, $"Deberían quedar 2 productos (iPhone + Canon). Encontrados: {itemsTrasEliminarNikon}");
            Console.WriteLine($"✓ Assert OK: carrito tiene {itemsTrasEliminarNikon} productos tras eliminar Nikon");

            // Paso 5: Editar carrito
            Console.WriteLine("── Paso 5: Editar carrito (pendiente de implementar) ──");

            // Paso 6: Eliminar el iPhone del carrito y verificar que solo queda Canon
            Console.WriteLine("✓ Eliminando iPhone del carrito...");
            cartPage.EliminarProducto("iPhone");
            Thread.Sleep(500);
            int itemsTrasEliminar = cartPage.GetCantidadItemsCarrito();
            Console.WriteLine($"✓ Items en carrito tras eliminar iPhone: {itemsTrasEliminar}");
            Assert.AreEqual(1, itemsTrasEliminar, $"Solo debería quedar 1 producto (Canon). Encontrados: {itemsTrasEliminar}");
            Console.WriteLine("✓ Assert OK: iPhone eliminado, Canon sigue en el carrito");

            // Paso 7: Mostrar carrito luego de eliminar el iPhone
            Console.WriteLine("✓ Mostrando carrito tras eliminar iPhone...");
            cartPage.MostrarProductosCarrito();

            // Paso 8: Actualizar cantidad de Canon a 1 y verificar el carrito
            Console.WriteLine("✓ Actualizando cantidad de Canon a 1...");
            cartPage.ActualizarCarrito("Canon", 1);
            bool exitoActualizacion = cartPage.EsperarYVerificarAlertaExito();
            Assert.IsTrue(exitoActualizacion, "No apareció el mensaje de éxito al modificar el carrito");
            Console.WriteLine("✓ Assert OK: alerta 'You have modified your shopping cart!' confirmada");
            int cantidadActualizada = cartPage.GetCantidadProductoEnCarrito("Canon");
            Assert.AreEqual(1, cantidadActualizada, $"La cantidad de Canon debería ser 1. Actual: {cantidadActualizada}");
            Console.WriteLine($"✓ Assert OK: cantidad de Canon en el input del carrito = {cantidadActualizada}");

            // Paso 9: Mostrar carrito luego de editar cantidad de Canon
            Console.WriteLine("✓ Mostrando carrito tras actualizar cantidad de Canon...");
            cartPage.MostrarProductosCarrito();

            // ==================== ASSERT FINAL ====================
            // Verificar que queda exactamente 1 cámara Canon en el carrito
            string textoCarrito = cartPage.GetTextoCarrito();
            Console.WriteLine($"✓ Resumen del botón carrito: {textoCarrito}");
            Assert.IsTrue(textoCarrito.Contains("1 item(s)"),
                $"El carrito debería mostrar '1 item(s)' (1 Canon). Texto actual: {textoCarrito}");
            Console.WriteLine($"✓ Assert OK: carrito contiene exactamente 1 cámara Canon → {textoCarrito}");
        }

        [TestMethod]
        public void Test_VaciarCarrito_OK()
        {
            // ==================== ARRANGE ====================
            Console.WriteLine($"✓ Página principal cargada automáticamente: {Driver?.Url}");

            SearchPage searchPage = new(Driver);
            Product productPage  = new(Driver);
            CartPage cartPage    = new(Driver);

            // ==================== ACT ====================
            // Paso 1: Agregar iPhone al carrito
            Console.WriteLine("✓ Agregando iPhone al carrito...");
            searchPage.SearchProduct("iPhone");
            Thread.Sleep(1000);
            productPage.ClickBtnAddCart();
            bool exitoIphone = cartPage.EsperarYVerificarAlertaExito();
            Assert.IsTrue(exitoIphone, "No apareció el alert de éxito al agregar iPhone");
            Console.WriteLine("✓ Assert OK: iPhone agregado al carrito");

            // Paso 2: Agregar iMac al carrito
            Console.WriteLine("✓ Agregando iMac al carrito...");
            searchPage.SearchProduct("iMac");
            Thread.Sleep(1000);
            productPage.ClickBtnAddCart();
            bool exitoImac = cartPage.EsperarYVerificarAlertaExito();
            Assert.IsTrue(exitoImac, "No apareció el alert de éxito al agregar iMac");
            Console.WriteLine("✓ Assert OK: iMac agregado al carrito");

            // Paso 3: Verificar que el carrito tiene 2 productos
            Console.WriteLine("✓ Verificando que el carrito tiene 2 productos...");
            cartPage.MostrarProductosCarrito();
            int itemsAntes = cartPage.GetCantidadItemsCarrito();
            Assert.AreEqual(2, itemsAntes, $"El carrito debería tener 2 productos. Encontrados: {itemsAntes}");
            Console.WriteLine($"✓ Assert OK: carrito tiene {itemsAntes} producto(s) antes de vaciar");

            // Paso 4: Vaciar el carrito
            Console.WriteLine("✓ Vaciando el carrito...");
            cartPage.VaciarCarrito();
            Console.WriteLine("✓ Carrito vaciado");

            // Paso 5: Verificar que el carrito está vacío
            int itemsDespues = cartPage.GetCantidadItemsCarrito();
            Assert.AreEqual(0, itemsDespues, $"El carrito debería estar vacío. Items encontrados: {itemsDespues}");
            Console.WriteLine($"✓ Assert OK: carrito vacío - {itemsDespues} producto(s)");

            // ==================== ASSERT FINAL ====================
            string textoCarrito = cartPage.GetTextoCarrito();
            Console.WriteLine($"✓ Resumen del botón carrito: {textoCarrito}");
            Assert.IsTrue(textoCarrito.Contains("0 item(s)"),
                $"El botón del carrito debería mostrar '0 item(s)'. Texto actual: {textoCarrito}");
            Console.WriteLine($"✓ Assert OK: carrito vacío confirmado → {textoCarrito}");
        }
    }
}
