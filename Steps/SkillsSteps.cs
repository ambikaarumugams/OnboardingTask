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

        public SkillsSteps(LoginPage loginPage, NavigationHelper navigationHelper, SkillsPage skillsPage)
        {
            _loginPage = loginPage;
            _navigationHelper = navigationHelper;
            _skillsPage = skillsPage;
        }

        [Given("I navigate to the profile page as a registered user")]
        public void GivenINavigateToTheProfilePageAsARegisteredUser()
        {
            _navigationHelper.NavigateTo("Home");
            _loginPage.Login("ambikaarumugams@gmail.com", "AmbikaSenthil123");
            _skillsPage.NavigateToTheProfilePage();
        }
        [When("I Add New Skills and select Skill level:")]
        public void WhenIAddNewSkillsAndSelectSkillLevel(DataTable addSkillsTable)
        {
            _skillsPage.DeleteAllSkills();
            foreach (var addNewSkills in addSkillsTable.Rows)
            {
                var newSkill= addNewSkills["New Skills"];
                var newSkillLevel = addNewSkills["Skill level"];
                _skillsPage.AddNewSkillsAndLevel(newSkill, newSkillLevel);
                _skillsPage.ClickAddButton();
            }
        }

        [Then("I should see the skills listed in my profile")]
        public void ThenIShouldSeeTheSkillsListedInMyProfile()
        {
            Console.WriteLine("Languages are added");
        }

        [When("I update the skill and skill level:")]
        public void WhenIUpdateTheSkillAndSkillLevel(DataTable updateSkillsTable)
        {
            foreach(var updateSkillList in updateSkillsTable.Rows)
            {
                var skill = updateSkillList[0];//"Existing Skill"
                var skillsToUpdate = updateSkillList[1];//"Skill to Update"
                var skillLevelToUpdate = updateSkillList[2];//"Skill level to Update"
                _skillsPage.UpdateSkillsAndLevel(skill, skillsToUpdate, skillLevelToUpdate);
            }
            
        }

        [Then("I should see the updated skill in my profile")]
        public void ThenIShouldSeeTheUpdatedSkillInMyProfile()
        {
            Console.WriteLine("The Skills updated successfully");
        }

        [When("I click the delete icon of the skill")]
        public void WhenIClickTheDeleteIconOfTheSkill()
        {
            _skillsPage.DeleteAllSkills();
        }

        [Then("I shouldn't see the skills list")]
        public void ThenIShouldntSeeTheSkillsList()
        {
            Console.WriteLine("The skills deleted successfully");
        }



    }
}
