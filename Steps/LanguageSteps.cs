using DocumentFormat.OpenXml.Drawing.Charts;
using qa_dotnet_cucumber.Pages;
using RazorEngine;
using Reqnroll;

namespace qa_dotnet_cucumber.Steps
{
    [Binding]
    [Scope(Feature = "Language")]
    [Scope(Feature = "LanguageNegative")]
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
                var successMessageAfterLanguageIsBeingAdded = _languagePage.GetSuccessMessageForAddNew(addNewList.NewLanguage);
                _actualAddLanguages.Add(successMessageAfterLanguageIsBeingAdded);
            }
            _scenarioContext.Set(_actualAddLanguages, "ActualAddLanguages");
            _scenarioContext.Set(_expectedAddLanguages, "ExpectedAddLanguages");
        }

        [When("I should see the languages and verify it has been added successfully")]   //Success message validation
        public void WhenIShouldSeeTheLanguagesAndVerifyItHasBeenAddedSuccessfully()
        {
            var _actualAddLanguages = _scenarioContext.Get<List<string>>("ActualAddLanguages");
            var _expectedAddLanguages = _scenarioContext.Get<List<string>>("ExpectedAddLanguages");

            foreach (var expectedAddList in _expectedAddLanguages)
            {
                Assert.That(_actualAddLanguages.Any(actual => actual.Contains(expectedAddList)),
                Is.True, $"Expected a message contains'{expectedAddList}',but not found");
            }
        }

        [Then("I should see the languages listed in my profile and verify it")]    //Table data list validation after adding
        public void ThenIShouldSeeTheLanguagesListedInMyProfileAndVerifyIt()
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

        [When("I Add an existing language and select a language level:")]
        public void WhenIAddAnExistingLanguageAndSelectALanguageLevel(Table languageTable)
        {
            _languagePage.DeleteAllLanguages(); //Delete all the languages in the list before adding new

            var languagesToAdd = languageTable.CreateSet<AddLanguage>();
            foreach (var addNewList in languagesToAdd)
            {
                _languagePage.AddNewLanguageAndLevel(addNewList.NewLanguage, addNewList.NewLanguageLevel);
            }
        }

        [When("I click Add New button, leave the language field empty,choose the language level and click the Add button")]
        public void WhenIClickAddNewButtonLeaveTheLanguageFieldEmptyChooseTheLanguageLevelAndClickTheAddButton()
        {
            _languagePage.DeleteAllLanguages();
            _languagePage.LeaveTheLanguageFieldEmptyForAdd();
        }

        [When("I click Add New button, enter the language field, not choosing the language level and click the Add button")]
        public void WhenIClickAddNewButtonEnterTheLanguageFieldNotChoosingTheLanguageLevelAndClickTheAddButton()
        {
            _languagePage.DeleteAllLanguages();
            _languagePage.NotChoosingLanguageLevelForAdd();
        }

        [When("I click Add New button, empty the language field, not choosing the language level and click the Add button")]
        public void WhenIClickAddNewButtonEmptyTheLanguageFieldNotChoosingTheLanguageLevelAndClickTheAddButton()
        {
            _languagePage.DeleteAllLanguages();
            _languagePage.LeaveTheLanguageFieldEmptyAndNotChoosingLanguageLevelForAdd();
        }

        [When("I add language {string} and it's level {string}")]
        public void WhenIAddLanguageAndItsLevel(string language, string level)
        {
            _languagePage.DeleteAllLanguages();
            _languagePage.AddNewLanguageAndLevel(language, level);
            Thread.Sleep(3000);
        }

        [Then("I click edit icon of {string}, leave the language field empty,choose the language level and click the Update button")]
        public void ThenIClickEditIconOfLeaveTheLanguageFieldEmptyChooseTheLanguageLevelAndClickTheUpdateButton(string existingLanguage)
        {
            _languagePage.LeaveTheLanguageFieldEmptyForUpdate(existingLanguage);
        }

        [Then("I click edit icon of {string}, enter the language field, not choosing the language level and click the Update button")]
        public void ThenIClickEditIconOfEnterTheLanguageFieldNotChoosingTheLanguageLevelAndClickTheUpdateButton(string existingLanguage)
        {
            _languagePage.NotChoosingLanguageLevelForUpdate(existingLanguage);
        }

       [When("I click edit icon of {string}, empty the language field, not choosing the language level and click the Update button")]
        public void WhenIClickEditIconOfEmptyTheLanguageFieldNotChoosingTheLanguageLevelAndClickTheUpdateButton(string existingLanguage)
        {
            _languagePage.LeaveTheLanguageFieldEmptyAndNotChoosingLanguageLevelForUpdate(existingLanguage);
        }     

        [Then("I should see {string} error message")]
        public void ThenIShouldSeeErrorMessage(string error)
        {
             Assert.That(_languagePage.IsErrorMessageDisplayed(error), Is.True, $"Error Message '{error}' should be displayed");
        }

        [When("I click Add New button, enter the language and it's level or leave empty and click Cancel button")]
        public void WhenIClickAddNewButtonEnterTheLanguageAndItsLevelOrLeaveEmptyAndClickCancelButton()
        {
            _languagePage.DeleteAllLanguages();
            _languagePage.LeaveTheLanguageFieldEmptyForAdd();
            // _languagePage.NotChoosingLanguageLevelForAdd();
            // _languagePage.LeaveTheLanguageFieldEmptyAndNotChoosingLanguageLevelForAdd();
            _languagePage.ClickCancelButton();
        }

        [Then("I should able to Cancel the operation and verify that no changes has happened")]
        public void ThenIShouldAbleToCancelTheOperationAndVerifyThatNoChangesHasHappened()
        {
            Assert.That(_languagePage.IsCancelButtonNotDisplayed(), Is.True, $"Cancel button is Displayed!");
        }

        [When("I update the language {string} with same value")]
        public void WhenIUpdateTheLanguageWithSameValue(string newLanguage)
        {
            _languagePage.DeleteAllLanguages();
            _languagePage.AddNewLanguageAndLevel(newLanguage, "Basic");
            _languagePage.UpdateLanguageAndLevelWithSameValue(newLanguage, newLanguage);
        }

        [When("I want to add any languages when the session is expired")]
        public void WhenIWantToAddAnyLanguagesWhenTheSessionIsExpired()
        {
            _languagePage.DeleteAllLanguages();
            _languagePage.ExpireSession();
            _languagePage.AddNewLanguageAndLevel("Russian", "Basic");
        }

        [When("I want to update any languages when the session is expired")]
        public void WhenIWantToUpdateAnyLanguagesWhenTheSessionIsExpired()
        {
            _languagePage.DeleteAllLanguages();
            _languagePage.AddNewLanguageAndLevel("Russian", "Basic");
            _languagePage.ExpireSession();
            _languagePage.UpdateLanguageAndLevel("Russian", "Chinese", "Basic");
        }

        [When("I want to delete any languages when the session is expired")]  
        public void WhenIWantToDeleteAnyLanguagesWhenTheSessionIsExpired()
        {
            _languagePage.DeleteAllLanguages();
            _languagePage.AddNewLanguageAndLevel("Russian", "Basic");
            _languagePage.ExpireSession();
            _languagePage.DeleteSpecificLanguage("Russian");
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
