using System;
using System.IO;
using Microsoft.Extensions.Hosting;
using TechTalk.SpecFlow;

namespace TFL.Specs.Hooks
{
    [Binding]
    public sealed class EnvironmentSetupHooks
    {


        [BeforeTestRun]
        public static void BeforeTestRun()
        {
        }

        [AfterTestRun]
        public static void AfterTestRun()
        {
        }
    }
}
