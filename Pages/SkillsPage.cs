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

        public SkillsPage(IWebDriver driver)  //Inject the driver directly
        {
            _driver = driver;
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
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

        public void AddNewSkillsAndLevel(string skills, string skillLevel)
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
            selectElement.SelectByText(skillLevel);
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

        public void ClickCancelUpdate()  //CancelUpdate
        {
            var clickCancelUpdate = _wait.Until(ExpectedConditions.ElementToBeClickable(CancelUpdateButton));
            clickCancelUpdate.Click();
        }

        public void DeleteSpecificSkills(string skillsToBeDeleted)    //To delete the specific skill 
        {
            if (skillsToBeDeleted == null || !skillsToBeDeleted.Any()) //To avoid object reference null exception
                throw new ArgumentException("Language list is empty or null");

            var languageTable = _wait.Until(ExpectedConditions.ElementIsVisible(SkillsTable));
            var row = languageTable.FindElement(By.XPath($".//tr[td[normalize-space(text())='{skillsToBeDeleted}']]"));
            var deleteIconElement = row.FindElement(By.XPath(".//i[@class='remove icon']"));
            deleteIconElement.Click();
        }

        public void DeleteAllSkills()    //To delete all skills 
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

        public string GetSuccessMeassageForAddSkill(string newSkill)  //To get the success message after adding skill for validation
        {
            var successMessageForAddSkill = _wait.Until(ExpectedConditions.ElementIsVisible(By.XPath($"//div[@class='ns-box-inner' and contains(text(), '{newSkill} has been added to your skills')]")));
            return successMessageForAddSkill.Text;
        }

        public List<string> GetAllAddedSkillList()    //To get all the skills list after adding for validation
        {
            try
            {
                var skillsTable = _wait.Until(ExpectedConditions.ElementIsVisible(SkillsTable));
                var addedSkills = new List<string>();
                var rows = skillsTable.FindElements(By.XPath(".//tbody/tr"));
                foreach (var row in rows)
                {
                    var skillCell = row.FindElement(By.XPath("./td[1]"));
                    addedSkills.Add(skillCell.Text.Trim());
                }
                return addedSkills;
            }
            catch
            {
                return new List<string>();
            }
        }

        public string GetUpdatedSkillSuccessMessage(string updateSkill)   //To get success message after updating skills for validation
        {
            var successMessageForUpdateSkill = _wait.Until(ExpectedConditions.ElementIsVisible(By.XPath($"//div[@class='ns-box-inner' and contains(text(), '{updateSkill} has been updated to your skills')]")));
            return successMessageForUpdateSkill.Text;
        }

        public List<string> GetAllUpdatedSkillsList()    //To get all the updated list for validation
        {
            try
            {
                var skillsTable = _wait.Until(ExpectedConditions.ElementIsVisible(SkillsTable));
                var updatedSkills = new List<string>();
                var rows = skillsTable.FindElements(By.XPath(".//tbody/tr"));
                foreach (var row in rows)
                {
                    var skillCell = row.FindElement(By.XPath("./td[1]"));
                    updatedSkills.Add(skillCell.Text.Trim());
                }
                return updatedSkills;
            }
            catch
            {
                return new List<string>();
            }
        }

        public string GetSuccessMessageForDeleteSkill(string skillToBeDeleted)    //To get success message after deleting a skill 
        {
            Thread.Sleep(1000);
            var successMessageForDelete = _wait.Until(ExpectedConditions.ElementIsVisible(By.XPath($"//div[@class='ns-box-inner' and  contains(text(), '{skillToBeDeleted} has been deleted')]")));
            return successMessageForDelete.Text;
        }

        public bool IsSkillsTableEmpty()  //To check the table is empty after deleting all skills for validation
        {
            var skillsTable = _wait.Until(ExpectedConditions.ElementIsVisible(SkillsTable));
            var rows = skillsTable.FindElements(By.XPath(".//tbody/tr"));
            return rows.Count == 0;
        }

        public void LeaveTheSkillFieldEmpty()    //To leave the skill field empty
        {
            //Click Add New Button
            var skillsTable = _wait.Until(ExpectedConditions.ElementIsVisible(SkillsTable));
            var addNewElement = skillsTable.FindElement(By.XPath(".//div[@class='ui teal button']"));
            addNewElement.Click();

            //Enter Skills
            var addSkillsElement = _wait.Until(ExpectedConditions.ElementToBeClickable(AddSkillsField));
            addSkillsElement.SendKeys("");

            //Select Skill Level
            var selectSkillLevelDropDown = _wait.Until(ExpectedConditions.ElementToBeClickable(SelectSkillLevel));

            SelectElement selectElement = new SelectElement(selectSkillLevelDropDown);
            selectElement.SelectByText("Intermediate");
        }

        public void NotChoosingSkillLevel()   //Not choosing the skill level
        {
            //Click Add New Button
            var skillsTable = _wait.Until(ExpectedConditions.ElementIsVisible(SkillsTable));
            var addNewElement = skillsTable.FindElement(By.XPath(".//div[@class='ui teal button']"));
            addNewElement.Click();

            //Enter Skills
            var addSkillsElement = _wait.Until(ExpectedConditions.ElementToBeClickable(AddSkillsField));
            addSkillsElement.SendKeys("Dancing");

            //Select Skill Level
            var selectSkillLevelDropDown = _wait.Until(ExpectedConditions.ElementToBeClickable(SelectSkillLevel));

            SelectElement selectElement = new SelectElement(selectSkillLevelDropDown);
            selectElement.SelectByText("Choose Skill Level");
            //  selectElement.SelectByValue("");
        }

        public void LeaveTheSkillFieldEmptyAndNotChoosingSkillLevel()   //Not selecting both skill and it's level
        {
            //Click Add New Button
            var skillsTable = _wait.Until(ExpectedConditions.ElementIsVisible(SkillsTable));
            var addNewElement = skillsTable.FindElement(By.XPath(".//div[@class='ui teal button']"));
            addNewElement.Click();

            //Enter Skills
            var addSkillsElement = _wait.Until(ExpectedConditions.ElementToBeClickable(AddSkillsField));
            addSkillsElement.SendKeys("");

            //Select Skill Level
            var selectSkillLevelDropDown = _wait.Until(ExpectedConditions.ElementToBeClickable(SelectSkillLevel));

            SelectElement selectElement = new SelectElement(selectSkillLevelDropDown);
            selectElement.SelectByText("Choose Skill Level");
            // selectElement.SelectByValue("")
        }

        public bool IsErrorMessageDisplayed(string error)  //Error Message for both and any of the fields empty
        {
            try
            {
                var popUpMessageElement = _wait.Until(ExpectedConditions.ElementIsVisible(By.XPath($"//div[contains(@class, 'ns-box-inner') and contains(text(), '{error}')]")));
                return true;// Found the Error message
            }
            catch
            {
                return false;
            }
        }

        public bool IsCancelButtonNotDisplayed()       //Ckeck the visiblity of cancel button 
        {
            try
            {
                var cancelButtonNotVisible = _wait.Until(ExpectedConditions.InvisibilityOfElementLocated(CancelButton));
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}




