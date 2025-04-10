using OpenQA.Selenium;
using OpenQA.Selenium.Internal;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;


namespace qa_dotnet_cucumber.Pages
{
    public class LanguagePage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;
        public IWebDriver Driver => _driver;         

        //Constructor
        public LanguagePage(IWebDriver driver)       // Inject IWebDriver directly
        { 
            _driver = driver;
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        }

        //Locators
        private readonly By ProfileTab = By.XPath("//a[normalize-space()='Profile']");
        private readonly By LanguagesTab = By.XPath("//a[normalize-space()='Languages']");
        //Add
        private readonly By AddLanguagesField = By.XPath("//input[@placeholder='Add Language']");
        private readonly By SelectLanguageLevel = By.XPath("//select[@name='level']");
        private readonly By AddButton = By.XPath("//input[@value='Add']");
        private readonly By CancelButton = By.XPath("//input[@value='Cancel']");
        private readonly By LanguageTable = By.XPath("//table[@class='ui fixed table'][.//th[normalize-space(text())='Language']]");       //whole table
        //Edit
        private readonly By AddLanguageForUpdateField = By.XPath(".//input[@type='text']");
        private readonly By UpdateButton = By.XPath(".//input[@value='Update']");
        private readonly By CancelUpdateButton = By.XPath("//span[@class='buttons-wrapper']//input[@value='Cancel']");
     //   private readonly By SuccessMessage = By.XPath("//div[@class='ns-box-inner']");


        //Action Methods
        public void NavigateToTheProfilePage()
        {
            //Profile 
            var profileElement = _wait.Until(ExpectedConditions.ElementToBeClickable(ProfileTab));
            profileElement.Click();

            //Languages
            var languagesElement = _wait.Until(ExpectedConditions.ElementToBeClickable(LanguagesTab));
            languagesElement.Click();
        }

        public void AddNewLanguageAndLevel(string language,string languagelevel)
        {
            //Add New Languages
            var languageTable = _wait.Until(ExpectedConditions.ElementIsVisible(LanguageTable));
            var addNewElement = languageTable.FindElement(By.XPath(".//div[@class='ui teal button ' and normalize-space(text())='Add New']"));
            addNewElement.Click();

            //Enter Language
            var addLanguagesElement = _wait.Until(ExpectedConditions.ElementToBeClickable(AddLanguagesField));
            addLanguagesElement.SendKeys(language);

            //Select Language Level
            var selectLanguageLevelDropDown = _wait.Until(ExpectedConditions.ElementToBeClickable(SelectLanguageLevel));

            SelectElement selectElement = new SelectElement(selectLanguageLevelDropDown);
            selectElement.SelectByText(languagelevel);
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
            var cancelButtonElement=_wait.Until(ExpectedConditions.ElementToBeClickable(CancelButton));
            cancelButtonElement.Click();
        }

        public void UpdateLanguageAndLevel(string existingLanguage, string languageToUpdate, string levelToUpdate)
        {
            try
            {
                var languageTable = _wait.Until(ExpectedConditions.ElementIsVisible(LanguageTable));
                var row = languageTable.FindElement(By.XPath($".//tr[td[normalize-space(text())='{existingLanguage}']]"));
                var editIcon = row.FindElement(By.XPath(".//i[@class='outline write icon']"));
                editIcon.Click();

                var editableRow = languageTable.FindElement(By.XPath($".//tr[.//input[@type='text' and @value='{existingLanguage}']]"));
                //Update the language
                var addLanguageForUpdateElement = editableRow.FindElement(AddLanguageForUpdateField);
                addLanguageForUpdateElement.Clear();
                Thread.Sleep(1000);
                addLanguageForUpdateElement.SendKeys(languageToUpdate);

                //Update Language Level
                var selectLanguageLevelDropDown = editableRow.FindElement(By.XPath(".//select[@name='level']"));

                SelectElement selectElement = new SelectElement(selectLanguageLevelDropDown);
                selectElement.SelectByText(levelToUpdate);

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

        public void DeleteAllLanguages()
        {
            while (true)
            {
                var languageTable = _wait.Until(ExpectedConditions.ElementIsVisible(LanguageTable));
                var deleteElements = languageTable.FindElements(By.XPath(".//i[@class='remove icon']"));

                if(deleteElements.Count > 0)
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

        public string GetSuccessMessageForAddNew(string language)
        {
            try
            {
                var successMessage = _wait.Until(ExpectedConditions.ElementIsVisible(By.XPath($"//div[@class='ns-box-inner' and  contains(text(), '{language} has been added to your languages')]")));
                return successMessage.Text;
            }
            catch
            {
                return string.Empty;
            }
        }

        public List<string> GetAllAddedLanguages()
        {
            try
            {
                var languageTable = _wait.Until(ExpectedConditions.ElementIsVisible(LanguageTable));
                var addedLanguages = new List<string>();
                var rows = languageTable.FindElements(By.XPath(".//tbody/tr"));
                foreach (var row in rows)
                {
                    var languageCell = row.FindElement(By.XPath("./td[1]"));
                    addedLanguages.Add(languageCell.Text.Trim());
                }
                return addedLanguages;
            }
            catch
            {
                return new List<string>();
            }
        }

        public string GetSuccessMessageForUpdate(string language)
        {
            try
            {
                var successMessage = _wait.Until(ExpectedConditions.ElementIsVisible(By.XPath($"//div[@class='ns-box-inner' and  contains(text(), '{language} has been added to your languages')]")));
                return successMessage.Text;
            }
            catch
            {
                return string.Empty;
            }
        }

        public List<string> GetAllUpdatedLanguages()
        {
            try
            {
                var languageTable = _wait.Until(ExpectedConditions.ElementIsVisible(LanguageTable));
                var addedLanguages = new List<string>();
                var rows = languageTable.FindElements(By.XPath(".//tbody/tr"));
                foreach (var row in rows)
                {
                    var languageCell = row.FindElement(By.XPath("./td[1]"));
                    addedLanguages.Add(languageCell.Text.Trim());
                }
                return addedLanguages;
            }
            catch
            {
                return new List<string>();
            }
        }
    }
}


