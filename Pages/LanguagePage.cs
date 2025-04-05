using OpenQA.Selenium;
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
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(15));
        }

        //Locators
        private readonly By ProfileTab = By.XPath("//a[normalize-space()='Profile']");
        private readonly By LanguagesTab = By.CssSelector(".item.active");
        //Add
        // private readonly By AddNewButton = By.XPath("//div[text()='Add New']");
        private readonly By AddNewButton = By.XPath("//table[contains(@class,'ui fixed')]/thead[1]/tr[1]/th[3]/div[1]");
        private readonly By AddLanguagesField = By.XPath("//input[@placeholder='Add Language']");
        private readonly By SelectLanguageLevel = By.XPath("//select[@name='level']");
        private readonly By AddButton = By.XPath("//input[@value='Add']");
        private readonly By CancelButton = By.XPath("//input[@value='Cancel']");
        //Edit
        private readonly By EditIcon = By.XPath("//table[@class='ui fixed table']/tbody/tr[1]/td[3]//i[@class='outline write icon'][1]");
        private readonly By AddLanguageForUpdateField = By.XPath("//table[@class='ui fixed table']//tbody/tr//div//input[@type='text']");
        private readonly By SelectLanguageLevelForUpdate = By.XPath("//table[@class='ui fixed table']//tbody/tr//div//select[@name='level']");
        private readonly By UpdateButton = By.XPath("//input[@value='Update']");
        private readonly By CancelUpdateButton = By.XPath("//span[@class='buttons-wrapper']//input[@value='Cancel']");
        //Delete
        private readonly By DeleteIcon = By.XPath("//table[@class='ui fixed table']/tbody/tr[1]/td[3]//i[@class='remove icon']");

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
            var addNewElement = _wait.Until(ExpectedConditions.ElementToBeClickable(AddNewButton));
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

        public void ClickEditIcon()
        {
            //Click Edit Icon
            var clickEditIcon = _wait.Until(ExpectedConditions.ElementToBeClickable(EditIcon));
            clickEditIcon.Click();
        }

        public void UpdateLanguageAndLevel(string updateLanguage, string updateLanguageLevel)
        {
            try
            {
                //Update the language
                var addLanguageForUpdateElement = _wait.Until(ExpectedConditions.ElementToBeClickable(AddLanguageForUpdateField));
                addLanguageForUpdateElement.Clear();
                Thread.Sleep(1000);
                addLanguageForUpdateElement.SendKeys(updateLanguage);

                //Update Language Level
                var selectLanguageLevelDropDown = _wait.Until(ExpectedConditions.ElementToBeClickable(SelectLanguageLevelForUpdate));

                SelectElement selectElement = new SelectElement(selectLanguageLevelDropDown);
                selectElement.SelectByText(updateLanguageLevel);

                _wait.Until(ExpectedConditions.ElementToBeClickable(UpdateButton)).Click();
            }
            catch(Exception ex)
            { 
                Console.WriteLine(ex);
            }
        }

        public void ClickCancelUpdate()
        {
            var clickCancelUpdate = _wait.Until(ExpectedConditions.ElementToBeClickable(CancelUpdateButton));
            clickCancelUpdate.Click(); ;
        }

        public void DeleteLanguageAndLevel()
        {
            var clickDeleteIcon = _wait.Until(ExpectedConditions.ElementToBeClickable(DeleteIcon));
            clickDeleteIcon.Click();
        }

    }
}


