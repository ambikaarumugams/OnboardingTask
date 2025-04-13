using Reqnroll;
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
            _loginPage.Login("ambikaarumugams@gmail.com", "AmbikaSenthil123");
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

        [When("I enter empty username")]
        public void WhenIEnterEmptyUsername()
        {
            _loginPage.Login("", "password");
        }

        [When("I enter empty password")]
        public void WhenIEnterEmptyPassword()
        {
            _loginPage.Login("a@b.com", "");
        }


        [Then("I should see the successful message")]
        public void ThenIShouldSeeTheSuccessfulMessage()
        {
            var successMessage = _loginPage.GetSuccessMessage();
            Assert.That(successMessage, Does.Contain("Hi"), "Profile page not loaded after login!");
        }

        [Then("I should see {string} error message")]
        public void ThenIShouldSeeErrorMessage(string expectedPopUpMessage)
        {
            Assert.That(_loginPage.IsErrorMsgDisplayed(expectedPopUpMessage), Is.True, $"Error Message \"{expectedPopUpMessage}\" should be displayed");
        }

        [Then("I should see {string} and {string} validation message")]
        public void ThenIShouldSeeAndValidationMessage(string expectedValidationMessageForEmail, string expectedValidationMessageForPassword)
        {
            Assert.That(_loginPage.IsValidationMsgDisplayed(expectedValidationMessageForEmail), Is.True, $"Validation Message \"{expectedValidationMessageForEmail}\" should be displayed");
            Assert.That(_loginPage.IsValidationMsgDisplayed(expectedValidationMessageForPassword), Is.True, $"Validation Message \"{expectedValidationMessageForPassword}\" should be displayed");
        }

        [Then("I should see {string} validation message")]
        public void ThenIShouldSeeValidationMessage(string validationMessage)
        {
            Assert.That(_loginPage.IsValidationMsgDisplayed(validationMessage), Is.True, $"Validation Message \"{validationMessage}\" should be displayed");
        }

    }
}