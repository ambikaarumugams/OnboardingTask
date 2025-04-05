using qa_dotnet_cucumber.Pages;
using Reqnroll;

namespace qa_dotnet_cucumber.Steps
{
    [Binding]
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
        }

        [When("I click Add New button")]
        public void WhenIClickAddNewButton()
        {
            _languagePage.NavigateToTheProfilePage();

        }

        [When("I Add New Language and select New Language level:")]
        public void WhenIAddNewLanguageAndSelectNewLanguageLevel(DataTable addLanguageTable)
        {
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
            Console.WriteLine("Languages are added");
        }

        [When("I click the edit icon of the language")]
        public void WhenIClickTheEditIconOfTheLanguage()
        {
            _languagePage.ClickEditIcon();
        }

        [When("I update the language and language level:")]
        public void WhenIUpdateTheLanguageAndLanguageLevel(DataTable updateLanguageTable)
        {
            foreach (var list in updateLanguageTable.Rows)
            {
                //Console.WriteLine(list);
                //var language_1 = list[0];
                //var language_2 = list[1];

                var language = list["Language"];
                var languagelevel = list["Language level"];

                _languagePage.UpdateLanguageAndLevel(language, languagelevel);
            }
        }

        [Then("I should see the updated language in my profile")]
        public void ThenIShouldSeeTheUpdatedLanguageInMyProfile()
        {
            Console.WriteLine("The Language updated successfully");

        }
    }
}
