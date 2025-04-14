using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
// MUST USE with ExpectedConditions
using SeleniumExtras.WaitHelpers;

namespace qa_dotnet_cucumber.Pages
{
    public class LoginPage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;
        public IWebDriver Driver => _driver;

        public LoginPage(IWebDriver driver) // Inject IWebDriver directly (Constructor)
        {
            _driver = driver;
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10)); // 10-second timeout
        }

        // Locators
        private readonly By SignIn = By.CssSelector(".item");
        private readonly By UsernameField = By.CssSelector("input[name='email']");
        private readonly By PasswordField = By.CssSelector("input[name='password']");
        private readonly By LoginButton = By.XPath("//button[normalize-space()='Login']");
        private readonly By SuccessMessage = By.XPath("//span[@class='item ui dropdown link ']");

        //Action Methods
        public void Login(string username, string password)
        {
            var signInLink = _wait.Until(ExpectedConditions.ElementToBeClickable(SignIn));
            signInLink.Click();

            var usernameElement = _wait.Until(ExpectedConditions.ElementIsVisible(UsernameField));
            usernameElement.SendKeys(username);

            var passwordElement = _wait.Until(d => d.FindElement(PasswordField));     //Lamda Expression
            passwordElement.SendKeys(password);

            var loginButtonElement = _wait.Until(ExpectedConditions.ElementToBeClickable(LoginButton));
            loginButtonElement.Click();
        }

        public bool IsAtHomePage()
        {
            return _driver.Title.Contains("Home");
        }

        public string GetSuccessMessage()
        {
            var element = _driver.FindElement(SuccessMessage);
            return _wait.Until(d => d.FindElement(SuccessMessage)).Text;
        }

        public bool IsErrorMsgDisplayed(string errorMessage)
        {
            try
            {
                var popUpMessageElement = _wait.Until(ExpectedConditions.ElementIsVisible(By.XPath($"//div[contains(@class, 'ns-box-inner') and contains(text(), '{errorMessage}')]")));
                return true;// Found the Error message
            }
            catch
            {
                return false;
            }
        }

        public bool IsValidationMsgDisplayed(string validationMeassage)
        {
            try
            {
                _wait.Until(d => d.FindElement(By.XPath($"//div[contains(text(),'{validationMeassage}')]")));
                return true; // Found the validation message
            }
            catch
            {
                return false;
            }
        }

        public bool IsVerificationOptionAvailable(string verificationOption)
        {
            try
            {
                _wait.Until(d => d.FindElement(By.XPath($"//button[@id='submit-btn' and normalize-space(text())='{verificationOption}']")));
                return true;// Found the veification o
            }
            catch
            {
                return false;
            }
        }

        public void ClickSendVerificationEmail(string sendVerificationEmail)
        {
            _wait.Until(d => d.FindElement(By.XPath($"//button[@id='submit-btn' and normalize-space(text())='{sendVerificationEmail}']"))).Click();
        }

        public bool IsVerificationMessageDisplayed(string verificationMessage)
        {
            try
            {
                _wait.Until(ExpectedConditions.ElementIsVisible(By.XPath($"//div[contains(@class, 'ns-box-inner') and contains(normalize-space(text()), '{verificationMessage}')]")));
                return true;
            }
            catch
            {
                return false;
            }
        }
        

    }
}