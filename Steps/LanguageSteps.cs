using qa_dotnet_cucumber.Pages;
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
        private readonly ScenarioContext _scenarioContext;

        //Constructor
        public LanguageSteps(LoginPage loginPage, NavigationHelper navigationHelper, LanguagePage languagePage, ScenarioContext scenarioContext)
        {
            _loginPage = loginPage;
            _navigationHelper = navigationHelper;
            _languagePage = languagePage;
            _scenarioContext = scenarioContext;
        }

        [Given("I navigate to the profile page as a registered user")]    //Login and navigated to the profile page
        public void GivenINavigateToTheProfilePageAsARegisteredUser()
        {
            _navigationHelper.NavigateTo("Home");
            _loginPage.Login("ambikaarumugams@gmail.com", "AmbikaSenthil123"); 
            _languagePage.NavigateToTheProfilePage();
        }

        [When("I Add the following New Language and select New Language level:")]   //Adding new language and it's level 
        public void WhenIAddTheFollowingNewLanguageAndSelectNewLanguageLevel(Table languageTable)
        {
            _languagePage.DeleteAllLanguages(); //Delete all the languages in the list before adding new

            var languagesToAdd = languageTable.CreateSet<AddLanguage>();     
            var _actualAddLanguages = new List<string>();
            var _expectedAddLanguages = new List<string>();
            foreach (var addNewList in languagesToAdd)
            {
                _expectedAddLanguages.Add(addNewList.NewLanguage);
                _languagePage.AddNewLanguageAndLevel(addNewList.NewLanguage, addNewList.NewLanguageLevel);
                _languagePage.ClickAddButton();
                var successMessageAfterLanguageIsBeingAdded = _languagePage.GetSuccessMessageForAddNew(addNewList.NewLanguage);
                _actualAddLanguages.Add(successMessageAfterLanguageIsBeingAdded);
            }
            _scenarioContext.Set(_actualAddLanguages, "ActualAddLanguages");
            _scenarioContext.Set(_expectedAddLanguages, "ExpectedAddLanguages");
        }

        [When("I should verify the languages has been added successfully")]   //Success message validation
        public void WhenIShouldVerifyTheLanguagesHasBeenAddedSuccessfully()
        {
            var _actualAddLanguages = _scenarioContext.Get<List<string>>("ActualAddLanguages");
            var _expectedAddLanguages = _scenarioContext.Get<List<string>>("ExpectedAddLanguages");
            foreach (var expectedAddList in _expectedAddLanguages)
            {
                Assert.That(_actualAddLanguages.Any(actual => actual.Contains(expectedAddList)),
                Is.True, $"Expected a message contains'{expectedAddList}',but not found");
            }
        }

        [Then("I should verify the languages listed in my profile")]    //Table data list validation after adding
        public void ThenIShouldVerifyTheLanguagesListedInMyProfile()
        {
            var _actual = _languagePage.GetAllAddedLanguages();
            var _expectedAddLanguages = _scenarioContext.Get<List<string>>("ExpectedAddLanguages");
            Assert.That(_actual, Is.EqualTo(_expectedAddLanguages), "There is a mismatch");
        }

        [When("I update the language and language level:")]    //To update language and it's level
        public void WhenIUpdateTheLanguageAndLanguageLevel(Table updateLanguageTable)
        {
            var languagesToUpdate = updateLanguageTable.CreateSet<UpdateLanguage>();
            var _actualUpdatedLanguages = new List<string>();
            var _expectedUpdatedLanguages = new List<string>();
            foreach (var addUpdateList in languagesToUpdate)
            {
                _languagePage.UpdateLanguageAndLevel(addUpdateList.ExistingLanguage, addUpdateList.LanguageToUpdate, addUpdateList.LanguageLevelToUpdate);
                var successMessageForUpdate = _languagePage.GetSuccessMessageForUpdate(addUpdateList.LanguageToUpdate);
                Console.WriteLine(successMessageForUpdate);
                _actualUpdatedLanguages.Add(successMessageForUpdate);
                _expectedUpdatedLanguages.Add(addUpdateList.LanguageToUpdate);
            }
            _scenarioContext.Set(_actualUpdatedLanguages, "ActualUpdatedLanguages");
            _scenarioContext.Set(_expectedUpdatedLanguages, "ExpectedUpdatedLanguages");
        }

        [Then("I should see the success message and updated language in my profile")]   //success message and table data list validation after updating
        public void ThenIShouldSeeTheSuccessMessageAndUpdatedLanguageInMyProfile()
        {
            var _actualUpdatedLanguages = _scenarioContext.Get<List<string>>("ActualUpdatedLanguages");
            var _expectedUpdatedLanguages = _scenarioContext.Get<List<string>>("ExpectedUpdatedLanguages");

            foreach (var expectedUpdateLanguage in _expectedUpdatedLanguages)
            {
                Assert.That(_actualUpdatedLanguages.Any(actual => actual.Contains(expectedUpdateLanguage)),
                Is.True, $"Expected a message contains'{expectedUpdateLanguage}',but not found");
            }
            Assert.That(_languagePage.GetAllUpdatedLanguages(), Is.SupersetOf(_expectedUpdatedLanguages), "The language hasn't updated successfully");
        }

        [When("I click the delete icon corresponding to the following languages:")]   //To delete the languages
        public void WhenIClickTheDeleteIconCorrespondingToTheFollowingLanguages(Table deleteLanguageTable)
        {
            var languagesToDelete = deleteLanguageTable.CreateSet<DeleteLanguage>();
            var _expectedLanguagesToDelete = new List<string>();
            var _actualDeletedLanguages = new List<string>();
            foreach (var deleteList in languagesToDelete)
            {
                _expectedLanguagesToDelete.Add(deleteList.LanguageToBeDeleted);
                _languagePage.DeleteSpecificLanguage(deleteList.LanguageToBeDeleted);
                var deleteSuccessMessage = _languagePage.GetSuccessMessageForDelete(deleteList.LanguageToBeDeleted);
                _actualDeletedLanguages.Add(deleteSuccessMessage);
            }
            _scenarioContext.Set(_actualDeletedLanguages, "ActualDeletedLanguages");
            _scenarioContext.Set(_expectedLanguagesToDelete, "ExpectedLanguagesToDelete");
        }

        [Then("I should see a success message for each deleted language")]   //Success message for deleting the languages
        public void ThenIShouldSeeASuccessMessageForEachDeletedLanguage()
        {
            var _actualDeletedLanguages = _scenarioContext.Get<List<string>>("ActualDeletedLanguages");
            var _expectedLanguagesToDelete = _scenarioContext.Get<List<string>>("ExpectedLanguagesToDelete");
            foreach (var expectedDeleteList in _expectedLanguagesToDelete)
            {
                Assert.That(_actualDeletedLanguages.Any(actual => actual.Contains(expectedDeleteList)),
               Is.True, $"Expected a message contains'{expectedDeleteList}',but not found");
            }
        }

        [Then("the languages table should be empty if all languages have been deleted")]   //To delete all the languages and check the table is empty 
        public void ThenTheLanguagesTableShouldBeEmptyIfAllLanguagesHaveBeenDeleted()
        {
            _languagePage.DeleteAllLanguages();
            Assert.That(_languagePage.IsLanguageTableEmpty(), Is.True, "Language table is not empty after deletions.");
        }

        public class AddLanguage    //Property class to add language
        {
            public string NewLanguage { get; set; }
            public string NewLanguageLevel { get; set; }
        }

        public class UpdateLanguage    //Property class to update language
        {
            public string ExistingLanguage { get; set; }
            public string LanguageToUpdate { get; set; }
            public string LanguageLevelToUpdate { get; set; }
        }

        public class DeleteLanguage   //Property class to delete language
        { 
            public string LanguageToBeDeleted { get; set; }
        }
    }
}
