This project contains a working examples of how to test web application using SpecFlow, .Net 6, C# and Visual Studio 2022.

TFL web application has a PlanYourJourney widget needs tested on different scenarios.

SpecFlowTFL.Specs is a test project using the NUnit framework containing 5 simple tests in TFL.feature and step definitions defined in PlanYourJourneyStepDefinitions.cs

Design:

This test project uses a standard SpecFlow .Net 6 C# template to acheive this coding challenge.
Every scenario opens a new browser, accepts cookies so cookies will persist per session.
Driver wait time is set to 20 seconds so the program will wait for 20 seconds for elememts to appear in some scenarios(Example: Search Results)
Recent tab does not bring the search history for the first time when Chrome is running under test mode. This scenario is searching for the journey twice to mock the results.
