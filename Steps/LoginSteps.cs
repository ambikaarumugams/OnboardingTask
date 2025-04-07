using OpenQA.Selenium;
using Reqnroll;
using NUnit.Framework;
using OpenQA.Selenium.Support.UI;
using qa_dotnet_cucumber.Pages;

namespace qa_dotnet_cucumber.Steps
{
    [Binding]
    public class LoginSteps
    {
        private readonly LoginPage _loginPage;
        private readonly NavigationHelper _navigationHelper;
      

        public LoginSteps(LoginPage loginPage, NavigationHelper navigationHelper)
        {
            _loginPage = loginPage;
            _navigationHelper = navigationHelper;
        }

        [Given("I am on the home page")]
        public void GivenIAmOnTheHomePage()
        {
            _navigationHelper.NavigateTo("Home");
            Assert.That(_loginPage.IsAtHomePage(), Is.True, "Home page not loaded");
        }

        [When("I enter valid username and valid password")]
        public void WhenIEnterValidUsernameAndValidPassword()
        {
            _loginPage.Login(userName, password);
           
        }

        [When("I enter invalid username and valid password")]
        public void WhenIEnterInvalidUsernameAndValidPassword()
        {
            _loginPage.Login("admin123@gmail.com", "AmbikaSenthil123");
            
        }

        [When("I enter a valid username and invalid password")]
        public void WhenIEnterAValidUsernameAndInvalidPassword()
        {
            _loginPage.Login("ambikaarumugams@gmail.com", "Testanalyst");
        }

        [When("I enter empty credentials")]
        public void WhenIEnterEmptyCredentials()
        {
            _loginPage.Login("", "");
        }

        [Then("I should see the successful message")]
        public void ThenIShouldSeeTheSuccessfulMessage()
        {
            var successMessage = _loginPage.GetSuccessMessage();
            Assert.That(successMessage, Does.Contain("Hi"), "Profile page not loaded after login!");
        }

        [Then("I should see an error message")]
        public void ThenIShouldSeeAnErrorMessage()
        {
            // Use LoginPage's driver to wait for and verify the error message
            var wait = new WebDriverWait(_loginPage.Driver, TimeSpan.FromSeconds(10));

            var errorMessageElement = wait.Until(d => d.FindElement(By.CssSelector(".ns-type-error")));
            var errorMessage = errorMessageElement.Text;
            Console.WriteLine(errorMessage);

            Assert.That(errorMessage, Does.Match("Confirm your email|Please enter a valid email address"), "Should see an appropriate error message");
        }
    }
}