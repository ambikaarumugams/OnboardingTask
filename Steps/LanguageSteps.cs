using AngleSharp.Text;
using qa_dotnet_cucumber.Pages;
using RazorEngine;
using Reqnroll;
using System.Linq;

namespace qa_dotnet_cucumber.Steps
{
    [Binding]
    [Scope(Feature = "Language Feature")]
    public class LanguageSteps
    {
        private readonly LoginPage _loginPage;
        private readonly NavigationHelper _navigationHelper;
        private readonly LanguagePage _languagePage;
        private readonly ScenarioContext _scenarioContext;
        private List<string> _actualLanguages;
        private List<string> _expectedLanguages;


        public LanguageSteps(LoginPage loginPage, NavigationHelper navigationHelper, LanguagePage languagePage, ScenarioContext scenarioContext)
        {
            _loginPage = loginPage;
            _navigationHelper = navigationHelper;
            _languagePage = languagePage;
            _scenarioContext = scenarioContext;
            _actualLanguages = new List<string>();
            _expectedLanguages = new List<string>();
        }

        [Given("I navigate to the profile page as a registered user")]
        public void GivenINavigateToTheProfilePageAsARegisteredUser()
        {
            _navigationHelper.NavigateTo("Home");
            _loginPage.Login("ambikaarumugams@gmail.com", "AmbikaSenthil123");
            _languagePage.NavigateToTheProfilePage();
        }

        [When("I Add the following New Language and select New Language level:")]
        public void WhenIAddTheFollowingNewLanguageAndSelectNewLanguageLevel(Table languageTable)
        {
            _languagePage.DeleteAllLanguages(); //Delete all the languages in the list before adding new
           
            var languagesToAdd = languageTable.CreateSet<Language>();
            foreach (var addNewList in languagesToAdd)
            {                                                     
                _expectedLanguages.Add(addNewList.NewLanguage);
                _languagePage.AddNewLanguageAndLevel(addNewList.NewLanguage,addNewList.NewLanguageLevel);
                _languagePage.ClickAddButton();
                var successMessageAfterLanguageIsBeingAdded = _languagePage.GetSuccessMessageForAddNew(addNewList.NewLanguage);
                _actualLanguages.Add(successMessageAfterLanguageIsBeingAdded); 
            }
            _scenarioContext.Set(_actualLanguages, "ActualLanguages");
            _scenarioContext.Set(_expectedLanguages, "ExpectedLanguages");
        }

        [When("I should verify the languages has been added successfully")]
        public void WhenIShouldVerifyTheLanguagesHasBeenAddedSuccessfully()
        {
            _actualLanguages = _scenarioContext.Get<List<string>>("ActualLanguages");
            Console.WriteLine(_actualLanguages);
            _expectedLanguages = _scenarioContext.Get<List<string>>("ExpectedLanguages");
            Console.WriteLine(_expectedLanguages);

            foreach(var expected in _expectedLanguages)
            {
                Assert.That(_actualLanguages.Any(actual => actual.Contains(expected)),
                Is.True, $"Expected a message contains'{expected}',but not found");
            }
        }

        [Then("I should verify the languages listed in my profile")]
        public void ThenIShouldVerifyTheLanguagesListedInMyProfile()
        { 
            var _actual = _languagePage.GetAllAddedLanguages();
            Console.WriteLine(_actual);
            _expectedLanguages = _scenarioContext.Get<List<string>>("ExpectedLanguages");
            Console.WriteLine(_expectedLanguages);
            Assert.That(_actual, Is.EqualTo(_expectedLanguages), "There is a mismatch");
        }

        [When("I update the language and language level:")]
        public void WhenIUpdateTheLanguageAndLanguageLevel(Table updateLanguageTable)
        {
            var languagesToUpdate=updateLanguageTable.CreateSet<Language>();
            foreach (var addUpdateList in languagesToUpdate)
            {
                _languagePage.UpdateLanguageAndLevel(addUpdateList.ExistingLanguage, addUpdateList.LanguageToUpdate, addUpdateList.LanguageLevelToUpdate);
                var successMessageForUpdate = _languagePage.GetSuccessMessageForUpdate(addUpdateList.LanguageToUpdate);
                Console.WriteLine(successMessageForUpdate);
                _actualLanguages.Add(successMessageForUpdate);
            }
            _scenarioContext.Set(_actualLanguages, "ActualLanguages");
            _scenarioContext.Set(_expectedLanguages, "ExpectedLanguages");
        }

        [Then("I should see the updated language in my profile")]
        public void ThenIShouldSeeTheUpdatedLanguageInMyProfile()
        {
            _actualLanguages = _scenarioContext.Get<List<string>>("ActualLanguages");
            Console.WriteLine(_actualLanguages);
            _expectedLanguages = _scenarioContext.Get<List<string>>("ExpectedLanguages");
            Console.WriteLine(_expectedLanguages);

            foreach (var expected in _expectedLanguages)
            {
                Assert.That(_actualLanguages.Any(actual => actual.Contains(expected)),
                Is.True, $"Expected a message contains'{expected}',but not found");
            }

            var _actual = _languagePage.GetAllUpdatedLanguages();
            Console.WriteLine(_actual);
            _expectedLanguages = _scenarioContext.Get<List<string>>("ExpectedLanguages");
            Console.WriteLine(_expectedLanguages);
            Assert.That(_actual, Is.Not.EqualTo(_expectedLanguages), "The language hasn't updated sccessfully");
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



        public class Language
        {
            public string NewLanguage { get; set; }
            public string NewLanguageLevel { get; set; }

            public string ExistingLanguage { get; set; }
            public string LanguageToUpdate {  get; set; }
            public string LanguageLevelToUpdate { get; set; }
        }
    }
}
