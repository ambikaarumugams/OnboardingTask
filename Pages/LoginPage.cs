using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Reqnroll.BoDi;
// MUST USE with ExpectedConditions
using SeleniumExtras.WaitHelpers;

namespace qa_dotnet_cucumber.Pages
{
    public class LoginPage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;
        public IWebDriver Driver => _driver;

        // Locators
        private readonly By SignIn = By.CssSelector(".item");
        private readonly By UsernameField = By.CssSelector("input[name='email']");
        private readonly By PasswordField = By.CssSelector("input[name='password']");
        private readonly By LoginButton = By.XPath("//button[normalize-space()='Login']");
        private readonly By SuccessMessage = By.XPath("//span[@class='item ui dropdown link ']");

        public LoginPage(IWebDriver driver) // Inject IWebDriver directly (Constructor)
        {
            _driver = driver;
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10)); // 10-second timeout
        }
        
        //Action Methods
        public void Login(string username, string password)
        {
            var signInLink = _wait.Until(ExpectedConditions.ElementToBeClickable(SignIn));
            signInLink.Click();

            var usernameElement = _wait.Until(ExpectedConditions.ElementIsVisible(UsernameField));
            usernameElement.SendKeys(username);

            var passwordElement = _wait.Until(d => d.FindElement(PasswordField));              //Lamda Expression
            passwordElement.SendKeys(password);

            var loginButtonElement = _wait.Until(ExpectedConditions.ElementToBeClickable(LoginButton));
            loginButtonElement.Click();
        }

       
        public string GetSuccessMessage()
        {
            var element = _driver.FindElement(SuccessMessage);
            return _wait.Until(d => d.FindElement(SuccessMessage)).Text;
        }

        public bool IsAtHomePage()
        {
            return _driver.Title.Contains("Home");
        }
    }
}