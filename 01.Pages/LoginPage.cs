using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

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

        // Propiedad para registrarme como nuevo usuario, "boton" continue
        public IWebElement BtnContinue => driver.FindElement(By.CssSelector("input[value='Continue']"));

        // Botón "Edit Account" en el panel derecho de la cuenta
        private IWebElement BtnEditAccount => driver.FindElement(By.CssSelector("#column-right > div > a:nth-child(2)"));

        // Botón "Password" en el panel derecho de la cuenta para editar la contraseña

        private IWebElement BtnPassword => driver.FindElement(By.CssSelector("#column-right > div > a:nth-child(3)"));
        //me dejo de machete el XPAth , lo hice con el CSS selector y funcionó, pero dejo el XPath comentado por si se quiere probar//*[@id="column-right"]/div/a[3]

        // ========== CAMPOS DEL FORMULARIO EDIT ACCOUNT ==========
        private IWebElement InputFirstName   => driver.FindElement(By.Id("input-firstname"));
        private IWebElement InputLastName    => driver.FindElement(By.Id("input-lastname"));
        private IWebElement InputEmailEdit   => driver.FindElement(By.Id("input-email"));
        private IWebElement InputTelephone   => driver.FindElement(By.Id("input-telephone"));

        // ========== CAMPOS DEL FORMULARIO CHANGE PASSWORD ==========
        private IWebElement InputPasswordEdit => driver.FindElement(By.Id("input-password"));
        private IWebElement InputConfirmEdit  => driver.FindElement(By.Id("input-confirm"));

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

        // Click en el botón "Password" del panel derecho de la cuenta para abrir el formulario de cambio de contraseña
        public void HacerClickBtnPassword()
        {
            BtnPassword.Click();
        }

        public void EditAccount()
        {
            BtnEditAccount.Click();
        }

        public void IngresarFirstNameEdit(string firstName)
        {
            InputFirstName.Clear();
            InputFirstName.SendKeys(firstName);
        }

        public void IngresarLastNameEdit(string lastName)
        {
            InputLastName.Clear();
            InputLastName.SendKeys(lastName);
        }

        public void IngresarEmailEdit(string email)
        {
            InputEmailEdit.Clear();
            InputEmailEdit.SendKeys(email);
        }

        public void IngresarTelephoneEdit(string telephone)
        {
            InputTelephone.Clear();
            InputTelephone.SendKeys(telephone);
        }

        public void IngresarPasswordEdit(string password)
        {
            InputPasswordEdit.Clear();
            InputPasswordEdit.SendKeys(password);
        }

        public void IngresarConfirmEdit(string confirm)
        {
            InputConfirmEdit.Clear();
            InputConfirmEdit.SendKeys(confirm);
        }

        // Edita los campos de la cuenta que no sean string vacío y confirma con el botón Continue
        public void EditarCuenta(string firstName, string lastName, string email, string telephone)
        {
            if (!string.IsNullOrEmpty(firstName))  IngresarFirstNameEdit(firstName);
            if (!string.IsNullOrEmpty(lastName))   IngresarLastNameEdit(lastName);
            if (!string.IsNullOrEmpty(email))      IngresarEmailEdit(email);
            if (!string.IsNullOrEmpty(telephone))  IngresarTelephoneEdit(telephone);
            HacerClickBtnContinue();
        }

        // Ingresa la nueva contraseña y la confirmación, omite el campo si es string vacío
        public void EditarContraseña(string password, string confirm)
        {
            if (!string.IsNullOrEmpty(password)) IngresarPasswordEdit(password);
            if (!string.IsNullOrEmpty(confirm))  IngresarConfirmEdit(confirm);
            HacerClickBtnContinue();
        }

        // Retorna el valor actual del campo First Name del formulario Edit Account
        public string GetFirstNameValue() => InputFirstName.GetAttribute("value");

        // Espera hasta 10 segundos a que aparezca el alert de éxito y verifica su texto
        public bool EsperarAlertaExito(int segundos = 10)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(segundos));
            IWebElement alerta = wait.Until(d => d.FindElement(By.CssSelector(".alert-success")));
            return alerta.Displayed && alerta.Text.Contains("Success");
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
