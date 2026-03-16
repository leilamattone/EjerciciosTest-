using OpenQA.Selenium;

namespace EjerciciosTest.Pages
{
    //Clase con constructor incluido para recibir el driver y propiedades privadas con localizadores
    internal class LoginPage(IWebDriver driver)
    {
        // Campo privado para almacenar el driver
        private readonly IWebDriver driver = driver;

        // Forma simplificada de hacer un get/set en uno
        // Propiedad privada - Localizador del botón "My Account"
        private IWebElement BtnMyAccount => driver.FindElement(By.LinkText("My Account"));

        // Propiedad privada - Localizador del campo de email
        public IWebElement EmailInput => driver.FindElement(By.Id("input-email"));

        // Propiedad privada - Localizador del campo de password
        public IWebElement EnterPassInput => driver.FindElement(By.Id("input-password"));

        // Propiedad privada - Localizador del botón de login
        private IWebElement BtnLogin => driver.FindElement(By.CssSelector("input[value='Login']"));

        // Propiedad privada - Localizador del botón de logout
        private IWebElement BtnLogout => driver.FindElement(By.LinkText("Logout"));

        // Propiedad privada - Localizador del link "Forgotten Password"
        private IWebElement ForgottenPasswordLink => driver.FindElement(By.LinkText("Forgotten Password"));

        // Propiedad privada - Localizador del link "Login" en el menú desplegable
        private IWebElement LinkLogin => driver.FindElement(By.LinkText("Login"));

        // Propiedad privada - Localizador del contenedor principal
        private IWebElement MainContent => driver.FindElement(By.XPath("//div[@id='content']"));

        // Propiedad pública - Localizador del mensaje de error (para verificación en tests)
        public IWebElement ErrorMessage => driver.FindElement(By.CssSelector(".alert.alert-danger"));

        // Propiedad privada - Localizador del link "Register" en el menú de la derecha
        private IWebElement BtnRegister => driver.FindElement(By.LinkText("Register"));

        // Propiedad privada - Localizador del link "Register" en el menú de la derecha
        public IWebElement BtnCheckBox => driver.FindElement(By.Name("agree"));

        // Propiedad para registrarme como nuevo uusario, "boton" continue
        public IWebElement BtnContinue => driver.FindElement(By.XPath("//*[@id=\"content\"]/form/div/div/input[2]"));

        // ========== MÉTODOS DE ACCIÓN ==========
        public void IngresarEmail(string email)
        {
            
            EmailInput.Clear(); // Limpiar el campo antes de ingresar el email
            EmailInput.SendKeys(email);
        }

        public void IngresarPassword(string password)
        {
            EnterPassInput.Clear(); // Limpiar el campo antes de ingresar la contraseña
            EnterPassInput.SendKeys(password);
        }

        public void HacerClickLogin()
        {
            BtnLogin.Click();
        }

        public void HacerClickLogOut()
        {
            BtnLogout.Click();
        }

        public void HacerClickMyAccount()
        {
            BtnMyAccount.Click();
        }

        public void HacerClickLinkLogin()
        {
            LinkLogin.Click();
        }

        public void HacerClickRegister()
        {
            BtnRegister.Click();
        }

        public void HacerClickBtnCheckBox()
        {
            BtnCheckBox.Click();
        }

        public void HacerClickBtnContinue()
        {
            BtnContinue.Click();
        }
        /// <summary>
        /// Attempts to log in a user using the specified email address and password.
        /// </summary>
        /// <param name="email">The email address associated with the user account. Cannot be null or empty.</param>
        /// <param name="password">The password for the user account. Cannot be null or empty.</param>
        /// <exception cref="Exception">Thrown if the login container is not displayed, preventing interaction with the login fields.</exception>
        public void Login(string email, string password)
        {
            // Verificar que el contenedor principal se muestra antes de intentar interactuar con los campos
            // Si el contenedor no se muestra, lanzar una excepción para evitar errores posteriores
            if (!MainContent?.Displayed ?? false) { throw new Exception("No se muestra el contenedor del Login"); }
            IngresarEmail(email);
            IngresarPassword(password);
            HacerClickLogin();

        }
    }
}
