using FluentAssertions;
using Microsoft.Extensions.Hosting;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SpecFlow.Actions.Selenium;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Xml.Linq;

namespace TFL.Specs.PageObjects
{
    public class PlanYourJourneyPage
    {

        private const string PageUrl = "http://www.tfl.gov.uk";

        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;
        private readonly IBrowserInteractions _browserInteractions;

        public PlanYourJourneyPage()
        {
            _driver = new ChromeDriver();
            _wait = new WebDriverWait(this._driver, TimeSpan.FromSeconds(20));
        }

        private IWebElement ButtonAcceptCookies => _driver.FindElement(By.XPath("//*[text()='Accept all cookies']/parent::button"));
        private IWebElement PlanJourneyFrom => _driver.FindElement(By.Id("InputFrom"));
        private IWebElement PlanJourneyTo => _driver.FindElement(By.Id("InputTo"));
        private IWebElement PlanJourneyButton => _driver.FindElement(By.Id("plan-journey-button"));
        private IWebElement FromError => _driver.FindElement(By.Id("InputFrom-error"));
        private IWebElement ToError => _driver.FindElement(By.Id("InputTo-error"));
        private IWebElement EditPreferences => _driver.FindElement(By.XPath("//button[text()='Edit preferences']"));
        private IWebElement DeselectAll => _driver.FindElement(By.XPath("//button[text()='deselect all']"));
        private IWebElement LeastWalking => _driver.FindElement(By.XPath("//input[@type='radio' and @value='leastwalking']"));
        private IWebElement LeastWalkingLabel => _driver.FindElement(By.XPath("//label[text()='Routes with least walking']"));
        private IWebElement UpdateJourneyButton => _driver.FindElement(By.XPath("//*[@id='more-journey-options']//*[@value='Update journey' ]"));
        private IWebElement ViewDetailsButton => _driver.FindElement(By.XPath("//*[@id='option-1-content']//button[text()='View details']"));
        private IWebElement UpStairs => _driver.FindElement(By.XPath("//*[contains(text(),'Covent Garden Underground')]/ancestor::div[@class='journey-detail-step footpath']//div[@class='access-information']/a[1]"));
        private IWebElement UpLift => _driver.FindElement(By.XPath("//*[contains(text(),'Covent Garden Underground')]/ancestor::div[@class='journey-detail-step footpath']//div[@class='access-information']/a[2]"));
        private IWebElement LevelWalkway => _driver.FindElement(By.XPath("//*[contains(text(),'Covent Garden Underground')]/ancestor::div[@class='journey-detail-step footpath']//div[@class='access-information']/a[3]"));
        private IWebElement JourneyTime => _driver.FindElement(By.XPath("//*[@id='option-1-heading']//div[contains(@class,'journey-time')]"));
        private IWebElement ErrorMessage => _driver.FindElement(By.XPath("//li[@class='field-validation-error']"));
        private IWebElement WalkingAndCyclingLabel => _driver.FindElement(By.XPath("//h2[.='Walking and cycling']"));
        private IWebElement WalkingJourneyTime => _driver.FindElement(By.XPath("//a[@class='journey-box walking']//div[contains(@class,'journey-info')]"));
        private IWebElement CyclingJourneyTime => _driver.FindElement(By.XPath("//a[@class='journey-box cycling']//div[contains(@class,'journey-info')]"));
        private IWebElement JorneyResultSummary => _driver.FindElement(By.ClassName("journey-result-summary"));
        public void Goto()
        {
            _driver.Navigate().GoToUrl(PageUrl);
            _driver.Manage().Window.Maximize();
        }
        public void CloseBrowser()
        {
            _driver.Close();
        }

        public void AcceptAllCookies()
        {
            ButtonAcceptCookies.Click();
            Thread.Sleep(2000);
        }
        public void EnterPlanJourneyFrom(string location)
        {
            PlanJourneyFrom.Click();
            PlanJourneyFrom.SendKeys(Keys.Control + "a");
            PlanJourneyFrom.SendKeys(Keys.Delete);
            PlanJourneyFrom.SendKeys(location);
            Thread.Sleep(1000);
            PlanJourneyFrom.SendKeys(Keys.ArrowDown);
            PlanJourneyFrom.SendKeys(Keys.Tab);
        }

        public void EnterPlanJourneyTo(string location)
        {
            PlanJourneyTo.Click();
            PlanJourneyTo.SendKeys(Keys.Control + "a");
            PlanJourneyTo.SendKeys(Keys.Delete);
            PlanJourneyTo.SendKeys(location);
            Thread.Sleep(1000);
            PlanJourneyTo.SendKeys(Keys.ArrowDown);
            PlanJourneyTo.SendKeys(Keys.Tab);
        }
        public void SubmitPlanMyJourney()
        {
            PlanJourneyButton.Click();
        }
        public void WaitUntilElementClickable(IWebElement element)
        {
            _wait.Until(driver =>
            {
                return element.Displayed && element.Enabled ? element : null;
            });
        }
        public void WaitUntilElementDisplayed(IWebElement element)
        {
            _wait.Until(driver =>
            {
                return element.Displayed ? element : null;
            });
        }
        public bool ValidateErrorMessage(string expectedError)
        {
            var fromActualError = FromError.Text;
            var toActualError = ToError.Text;
            if (expectedError.Equals(fromActualError) || expectedError.Equals(toActualError))
            {
                return true;
            }
            return false;
        }
        public void CheckJourneyResultExists()
        {
            WaitUntilElementDisplayed(JorneyResultSummary);
        }
        public void EditPreferncesLink()
        {
            EditPreferences.Click();
        }
        public void DeselectAllLink()
        {
            WaitUntilElementClickable(DeselectAll);
            DeselectAll.Click();
        }
        public void LeastWalkingRadioButton()
        {
            WaitUntilElementDisplayed(LeastWalkingLabel);

            if (!LeastWalking.Selected)
            {
                try
                {
                    LeastWalking.Click();
                }
                catch (ElementClickInterceptedException)
                {
                    IJavaScriptExecutor js = (IJavaScriptExecutor)_driver;
                    js.ExecuteScript("arguments[0].click();", LeastWalking);
                }
            }
        }
        public void ClickUpdateJourney()
        {
            UpdateJourneyButton.Click();
        }
        public void ViewDetails()
        {
            WaitUntilElementClickable(ViewDetailsButton);            
            ViewDetailsButton.Click();
        }
        public void ValidateAccessInformation()
        {
            WaitUntilElementClickable(UpStairs);
            UpStairs.Click();
            var upstairsValue = UpStairs.Text;
            Assert.AreEqual("Up stairs", upstairsValue);

            UpLift.Click();
            var upliftValue = UpLift.Text;
            Assert.AreEqual("Up lift", upliftValue);

            LevelWalkway.Click();
            var levelWalkwayValue = LevelWalkway.Text;
            Assert.AreEqual("Level walkway", levelWalkwayValue);

        }
        public bool ValidateResults(string expectedResult)
        {
            var actualResult = _driver.FindElement(By.XPath("//*[contains(text(),'" + expectedResult + "')]")).GetAttribute("value");

            if (expectedResult.Equals(actualResult))
            {
                return true;
            }
            return false;
        }
        public void ValidateJourneyResult(string label, string expectedValue)
        {
            var actualResult = _driver.FindElement(By.XPath("//*[contains(text(),'" + label + "')]/following-sibling::span/strong")).Text;
            Assert.AreEqual(expectedValue, actualResult);
        }
        public void ValidateCyclingResults()
        {
            WaitUntilElementDisplayed(WalkingAndCyclingLabel);
            var actualValue = CyclingJourneyTime.Text;
            actualValue = actualValue.Replace("\r\n", "");
            Regex regex = new Regex("[0-9]+mins");
            Assert.That(regex.IsMatch(actualValue), Is.True, "Invalid Cycling Journey Time");
        }
        public void ValidateWalkingResults()
        {
            WaitUntilElementDisplayed(WalkingAndCyclingLabel);
            var actualValue = WalkingJourneyTime.Text;
            actualValue = actualValue.Replace("\r\n", "");
            Regex regex = new Regex("[0-9]+mins");
            Assert.That(regex.IsMatch(actualValue), Is.True, "Invalid Walking Journey Time");
        }
        public void ValidateJourneyTime()
        {
            WaitUntilElementClickable(ViewDetailsButton);
            var actualValue = JourneyTime.Text;
            actualValue = actualValue.Replace("\r\n", "");
            Regex regex = new Regex("Total time:[0-9]+mins");
            Assert.That(regex.IsMatch(actualValue), Is.True, "Invalid Journey Time");
        }
        public void ValidateInvalidJourneyErrorMessage(string expectedValue)
        {
            WaitUntilElementDisplayed(ErrorMessage);
            var actualResult = ErrorMessage.Text;
            Assert.AreEqual(expectedValue, actualResult);
        }
    }

}
