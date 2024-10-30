using TFL.Specs.PageObjects;
using TechTalk.SpecFlow;

namespace TFL.Specs.Hooks
{
    [Binding]
    public sealed class TestExecutionHooks
    {
        [BeforeScenario]
        public void BeforeScenario(PlanYourJourneyPage planYourJourneyPage)
        {
            planYourJourneyPage.Goto();
            planYourJourneyPage.AcceptAllCookies();
        }

        [AfterScenario]
        public void AfterScenario(PlanYourJourneyPage planYourJourneyPage)
        {
           planYourJourneyPage.CloseBrowser();
        }

    }
}
