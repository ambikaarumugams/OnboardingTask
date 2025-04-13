using qa_dotnet_cucumber.Pages;
using Reqnroll;


namespace qa_dotnet_cucumber.Steps
{
    [Binding]
    [Scope(Feature = "Skills Feature")]
    public class SkillsSteps
    {
        private readonly LoginPage _loginPage;
        private readonly NavigationHelper _navigationHelper;
        private readonly SkillsPage _skillsPage;
        private readonly ScenarioContext _scenarioContext;

        public SkillsSteps(LoginPage loginPage, NavigationHelper navigationHelper, SkillsPage skillsPage, ScenarioContext scenarioContext)
        {
            _loginPage = loginPage;
            _navigationHelper = navigationHelper;
            _skillsPage = skillsPage;
            _scenarioContext = scenarioContext;
        }

        [Given("I navigate to the profile page as a registered user")]
        public void GivenINavigateToTheProfilePageAsARegisteredUser()
        {
            _navigationHelper.NavigateTo("Home");
            _loginPage.Login("ambikaarumugams@gmail.com", "AmbikaSenthil123");
            _skillsPage.NavigateToTheProfilePage();
        }

        [When("I Add the following NewSkills and select SkillLevel:")]
        public void WhenIAddTheFollowingNewSkillsAndSelectSkillLevel(Table addSkillsTable)
        {
            _skillsPage.DeleteAllSkills();
            var skillsToAdd = addSkillsTable.CreateSet<AddSkills>();
            var _actualAddedSkills = new List<string>();
            var _expectedSkillsToAdd = new List<string>();
            foreach (var skill in skillsToAdd)
            {
                _expectedSkillsToAdd.Add(skill.NewSkills);
                _skillsPage.AddNewSkillsAndLevel(skill.NewSkills, skill.SkillLevel);
                _skillsPage.ClickAddButton();
                var successMessageAfterAddingSkill =_skillsPage.GetSuccessMeassageForAddSkill(skill.NewSkills);
                _actualAddedSkills.Add(successMessageAfterAddingSkill);
            }
            _scenarioContext.Set(_actualAddedSkills, "ActualSkillsAdded");
            _scenarioContext.Set(_expectedSkillsToAdd, "ExpectedSkillsToAdd");
        }

        [When("I should verify the skills has been added successfully")]
        public void WhenIShouldVerifyTheSkillsHasBeenAddedSuccessfully()
        {
            var _actualAddedSkills = _scenarioContext.Get<List<string>>("ActualSkillsAdded");
            var _expectedSkillsToAdd = _scenarioContext.Get<List<string>>("ExpectedSkillsToAdd");
            foreach(var skill in _expectedSkillsToAdd)
            {
                Assert.That(_actualAddedSkills.Any(actual => actual.Contains(skill)),
                Is.True, $"Expected a message contains'{skill}',but not found");
            }
        }

        [Then("I should verify the skills listed in my profile")]
        public void ThenIShouldVerifyTheSkillsListedInMyProfile()
        {
            var _actualSkills = _skillsPage.GetAllAddedSkillList();
            var _expectedSkillsToAdd = _scenarioContext.Get<List<string>>("ExpectedSkillsToAdd");
            Assert.That(_actualSkills, Is.EqualTo(_expectedSkillsToAdd), "There is a mismatch!");
        }

        [When("I update the skill and skill level:")]
        public void WhenIUpdateTheSkillAndSkillLevel(Table updateSkillsTable)
        {
            var skillsToUpdate = updateSkillsTable.CreateSet<UpdateSkills>();
            var _actualUpdatedSkills = new List<string>();
            var _expectedSkillsToUpdate = new List<string>();
            foreach(var updateSkill in skillsToUpdate)
            {
                _expectedSkillsToUpdate.Add(updateSkill.SkillToUpdate);
                _skillsPage.UpdateSkillsAndLevel(updateSkill.ExistingSkill, updateSkill.SkillToUpdate, updateSkill.SkillLevelToUpdate);
                var successMessageAfterUpdatingSkill = _skillsPage.GetUpdatedSkillSuccessMessage(updateSkill.SkillToUpdate);
                _actualUpdatedSkills.Add(successMessageAfterUpdatingSkill);
            }
            _scenarioContext.Set(_actualUpdatedSkills, "ActualSkillsUpdated");
            _scenarioContext.Set(_expectedSkillsToUpdate, "ExpectedSkillsToUpdate");
        }


        [Then("I should see the success message and updated skills in my profile")]
        public void ThenIShouldSeeTheSuccessMessageAndUpdatedSkillsInMyProfile()
        {
            var _actualUpdatedSkills = _scenarioContext.Get<List<string>>("ActualSkillsUpdated");
            var _expectedSkillsToUpdate = _scenarioContext.Get<List<string>>("ExpectedSkillsToUpdate");
            foreach (var UpdatedSkill in _expectedSkillsToUpdate)
            {
                Assert.That(_actualUpdatedSkills.Any(actual => actual.Contains(UpdatedSkill)),
                Is.True, $"Expected a message contains'{UpdatedSkill}',but not found");
            }
          Assert.That(_skillsPage.GetAllUpdatedSkillsList(), Is.SupersetOf(_expectedSkillsToUpdate), "The skills haven't updated successfully");
        }

        [When("I click the delete icon corresponding to the following skills:")]
        public void WhenIClickTheDeleteIconCorrespondingToTheFollowingSkills(Table skillsToBeDeleted)
        {
            var skillsToDelete = skillsToBeDeleted.CreateSet<DeleteSkills>();
            var _actualDeletedSkills = new List<string>();
            var _expectedSkillsToDelete = new List<string>();
            foreach (var deleteSkill in skillsToDelete)
            {
                _expectedSkillsToDelete.Add(deleteSkill.SkillsToBeDeleted);
                _skillsPage.DeleteSpecificSkills(deleteSkill.SkillsToBeDeleted);
                var successMessageAfterDeletingSkill = _skillsPage.GetSuccessMessageForDeleteSkill(deleteSkill.SkillsToBeDeleted);
                _actualDeletedSkills.Add(successMessageAfterDeletingSkill);
            }
            _scenarioContext.Set(_actualDeletedSkills, "ActualDeletedSkills");
            _scenarioContext.Set(_expectedSkillsToDelete, "ExpectedSkillsToDelete");
         }
              
        [Then("I should see a success message for each deleted skill")]
        public void ThenIShouldSeeASuccessMessageForEachDeletedSkill()
        {
            var _actualDeletedSkills = _scenarioContext.Get<List<string>>("ActualDeletedSkills");
            var _expectedSkillsToDelete = _scenarioContext.Get<List<string>>("ExpectedSkillsToDelete");
            foreach (var DeletedSkill in _expectedSkillsToDelete)
            {
                Assert.That(_actualDeletedSkills.Any(actual => actual.Contains(DeletedSkill)),
                Is.True, $"Expected a message contains'{DeletedSkill}',but not found");
            }
        }

        [Then("the skills table should be empty if all skills have been deleted")]
        public void ThenTheSkillsTableShouldBeEmptyIfAllSkillsHaveBeenDeleted()
        {
            _skillsPage.DeleteAllSkills();
            Thread.Sleep(500);
            Assert.That(_skillsPage.IsSkillsTableEmpty(), Is.True, "Skills table is not empty after deletion");
        }

        private class AddSkills
        {
            public string NewSkills { get; set; }
            public string SkillLevel { get; set; }
        }

        private class UpdateSkills
        {
            public string ExistingSkill {  get; set; }
            public string SkillToUpdate { get; set; }
            public string SkillLevelToUpdate { get; set; }
        }

        private class DeleteSkills
        {
            public string SkillsToBeDeleted { get; set; }
        }




    }
}
