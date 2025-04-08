using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace qa_dotnet_cucumber.Pages
{
    public class SkillsPage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;
        public IWebDriver Driver { get { return _driver; } }

        public SkillsPage(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(_driver,TimeSpan.FromSeconds(5));
        }
        //Locators
        private readonly By ProfileTab = By.XPath("//a[normalize-space()='Profile']");
        private readonly By SkillsTab = By.XPath("//a[normalize-space()='Skills']");

        //Add New Skills
        private readonly By SkillsTable = By.XPath("//table[@class='ui fixed table'][.//th[normalize-space(text())='Skill']]");
        //private readonly By AddNewButton = By.XPath(".//div[@class='ui teal button ' and normalize-space(text())='Add New']");
        private readonly By AddSkillsField = By.XPath("//input[@placeholder='Add Skill']");
        private readonly By SelectSkillLevel = By.XPath("//select[@name='level']");
        private readonly By AddButton = By.XPath("//input[@value='Add']");
        private readonly By CancelButton = By.XPath("//input[@value='Cancel']");

        //Edit
        private readonly By AddSkillsForUpdateField = By.XPath(".//input[@type='text']");
        private readonly By UpdateButton = By.XPath(".//input[@value='Update']");
        private readonly By CancelUpdateButton = By.XPath("//span[@class='buttons-wrapper']//input[@value='Cancel']");

        //Action Methods
        public void NavigateToTheProfilePage()
        {
            //Profile 
            var profileElement = _wait.Until(ExpectedConditions.ElementToBeClickable(ProfileTab));
            profileElement.Click();

            //Skills
            var skillsElement = _wait.Until(ExpectedConditions.ElementToBeClickable(SkillsTab));
            skillsElement.Click();
        }
        public void AddNewSkillsAndLevel(string skills, string skilllevel)
        {
            //Add New Skills
            var skillsTable = _wait.Until(ExpectedConditions.ElementIsVisible(SkillsTable));
            var addNewElement = skillsTable.FindElement(By.XPath(".//div[@class='ui teal button']"));
            addNewElement.Click();

            //Enter Skills
            var addSkillsElement = _wait.Until(ExpectedConditions.ElementToBeClickable(AddSkillsField));
            addSkillsElement.SendKeys(skills);

            //Select Skill Level
            var selectSkillLevelDropDown = _wait.Until(ExpectedConditions.ElementToBeClickable(SelectSkillLevel));

            SelectElement selectElement = new SelectElement(selectSkillLevelDropDown);
            selectElement.SelectByText(skilllevel);
        }

        public void ClickAddButton()
        {
            //Click Add Button
            var addButton = _wait.Until(ExpectedConditions.ElementToBeClickable(AddButton));
            addButton.Click();
        }

        public void ClickCancelButton()
        {
            //Click Cancel Button
            var cancelButton = _wait.Until(ExpectedConditions.ElementToBeClickable(CancelButton));
            cancelButton.Click();
        }

        public void UpdateSkillsAndLevel(string existingSkill, string skillToUpdate, string skillLevelToUpdate)
        {
            try
            {
                var skillsTable = _wait.Until(ExpectedConditions.ElementIsVisible(SkillsTable));
                var row = skillsTable.FindElement(By.XPath($".//tr[td[normalize-space(text())='{existingSkill}']]"));
                var editIcon = row.FindElement(By.XPath(".//i[@class='outline write icon']"));
                editIcon.Click();

                var editableRow = skillsTable.FindElement(By.XPath($".//tr[.//input[@type='text' and @value='{existingSkill}']]"));
                //Update the Skill
                var addSkillsForUpdateElement = editableRow.FindElement(AddSkillsForUpdateField);
                addSkillsForUpdateElement.Clear();
                Thread.Sleep(1000);
                addSkillsForUpdateElement.SendKeys(skillToUpdate);

                //Update Skill Level
                var selectSkillLevelDropDown = editableRow.FindElement(By.XPath(".//select[@name='level']"));

                SelectElement selectElement = new SelectElement(selectSkillLevelDropDown);
                selectElement.SelectByText(skillLevelToUpdate);
                editableRow.FindElement(UpdateButton).Click();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public void ClickCancelUpdate()
        {
            //CancelUpdate
            var clickCancelUpdate = _wait.Until(ExpectedConditions.ElementToBeClickable(CancelUpdateButton));
            clickCancelUpdate.Click(); ;
        }


        public void DeleteAllSkills()
        {
            while (true)
            {
                var skillsTable = _wait.Until(ExpectedConditions.ElementIsVisible(SkillsTable));
                var deleteElements = skillsTable.FindElements(By.XPath(".//i[@class='remove icon']"));

                if (deleteElements.Count > 0)
                {
                    deleteElements[0].Click();
                    Thread.Sleep(500);
                }
                else
                {
                    break;
                }
            }
        }
    }
}


