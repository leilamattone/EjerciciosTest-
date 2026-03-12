using OpenQA.Selenium;

namespace EjerciciosTest.Pages
{
    internal class LoginPage
    {
        // Campo privado para almacenar el driver
        private readonly IWebDriver driver;

        // Constructor que recibe el driver desde los tests
        public LoginPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void IngresarEmail(string email)
        {
            driver.FindElement(By.Id("input-email")).SendKeys(email);
        }

        public void IngresarPassword(string password)
        {
            driver.FindElement(By.Id("input-password")).SendKeys(password);
        }

        public void HacerClickLogin()
        {
            driver.FindElement(By.CssSelector("input[value='Login']")).Click();
        }

        public void HacerClickLogOut()
        {
            driver.FindElement(By.LinkText("Logout")).Click();
        }

        public void Login(string email, string password)
        {
            IngresarEmail(email);
            IngresarPassword(password);
            HacerClickLogin();
        }
    }
}
