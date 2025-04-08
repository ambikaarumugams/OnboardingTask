using AngleSharp.Text;
using qa_dotnet_cucumber.Pages;
using RazorEngine;
using Reqnroll;

namespace qa_dotnet_cucumber.Steps
{
    [Binding]
    [Scope(Feature = "Language Feature")]
    public class LanguageSteps
    {
        private readonly LoginPage _loginPage;
        private readonly NavigationHelper _navigationHelper;
        private readonly LanguagePage _languagePage;

        public LanguageSteps(LoginPage loginPage, NavigationHelper navigationHelper, LanguagePage languagePage)
        {
            _loginPage = loginPage;
            _navigationHelper = navigationHelper;
            _languagePage = languagePage;
        }

        [Given("I navigate to the profile page as a registered user")]
        public void GivenINavigateToTheProfilePageAsARegisteredUser()
        {
            _navigationHelper.NavigateTo("Home");
            _loginPage.Login("ambikaarumugams@gmail.com", "AmbikaSenthil123");
            _languagePage.NavigateToTheProfilePage();
        }

        [When("I Add New Language and select New Language level:")]
        public void WhenIAddNewLanguageAndSelectNewLanguageLevel(DataTable addLanguageTable)
        {
            _languagePage.DeleteAllLanguages(); //Delete all the languages in the list before adding new
            foreach (var addNewList in addLanguageTable.Rows)
            {
                var newLanguage = addNewList["New Language"];
                var newLanguageLevel = addNewList["New Language level"];
                _languagePage.AddNewLanguageAndLevel(newLanguage, newLanguageLevel);
                _languagePage.ClickAddButton();
            }
        }

        [Then("I should see the languages listed in my profile")]
        public void ThenIShouldSeeTheLanguagesListedInMyProfile()
        {
            Assert.That(_languagePage.GetSuccessMessage(), Does.Contain("Tamil"), "There is a mismatch!");
        }

        [When("I update the language and language level:")]
        public void WhenIUpdateTheLanguageAndLanguageLevel(DataTable updateLanguageTable)
        {
            foreach (var list in updateLanguageTable.Rows)
            {
                var language = list["Existing Language"];
                var languageToUpdate = list["Language to Update"];
                var languagelevelToUpdate = list["Language level to Update"];
                _languagePage.UpdateLanguageAndLevel(language, languageToUpdate, languagelevelToUpdate);
            }
        }

        [Then("I should see the updated language in my profile")]
        public void ThenIShouldSeeTheUpdatedLanguageInMyProfile()
        {
            Console.WriteLine("The Language updated successfully");

        }

        [When("I click the delete icon of the language")]
        public void WhenIClickTheDeleteIconOfTheLanguage()
        {
            _languagePage.DeleteAllLanguages();   
        }

        [Then("I shouldn't see the languages list")]
        public void ThenIShouldntSeeTheLanguagesList()
        {
            Console.WriteLine("The Language deleted successfully");
        }

        

    }
}
