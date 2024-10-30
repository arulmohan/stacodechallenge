Feature: PlanMyJourney

@functional
Scenario: 1 Verify that a valid journey
	Given the user enter plan journey from Leicester Square Underground
	And the user enter plan journey to Covent Garden Underground
	When the user clicks the plan my journey button
	Then the user should verify the results
	Then the user should see the journey results with "From" as "Leicester Square Underground Station"
	Then the user should see the journey results with "To" as "Covent Garden Underground Station"
	And the user should verify the walking time displayed
	Then the user should verify the cycling time displayed

@functional
Scenario: 2 Validate the journey time for least walking
	Given the user enter plan journey from Leicester Square Underground
	And the user enter plan journey to Covent Garden Underground
	When the user clicks the plan my journey button
	Then the user should verify the results
	Then the user should see the journey results with "From" as "Leicester Square Underground Station"
	Then the user should see the journey results with "To" as "Covent Garden Underground Station"
	And the user should verify the walking time displayed
	Then the user should verify the cycling time displayed
	When the user clicks the edit preferences
	And the user clicks the deselect all
	And the user selects the routes with least walking radio button
	Then the user clicks the update journey button
	And the user should verify the journey time displayed

@functional
Scenario: 3 Verify complete access information at Covent Garden Underground Station
	Given the user enter plan journey from Leicester Square Underground
	And the user enter plan journey to Covent Garden Underground
	When the user clicks the plan my journey button
	Then the user should verify the results
	Then the user should see the journey results with "From" as "Leicester Square Underground Station"
	Then the user should see the journey results with "To" as "Covent Garden Underground Station"
	And the user should verify the walking time displayed
	Then the user should verify the cycling time displayed
	When the user clicks the edit preferences
	And the user clicks the deselect all
	And the user selects the routes with least walking radio button
	Then the user clicks the update journey button
	And the user should verify the journey time displayed
	And the user clicks the view details button
	Then the user should see the complete access information at Covent Garden Underground Station

@functional
Scenario: 4 Verify that the widget is unable to provide results when an invalid journey is planned.
	Given the user enter plan journey from 123456
	And the user enter plan journey to 765456
	When the user clicks the plan my journey button
	Then the user should see the error message as "Journey planner could not find any results to your search. Please try again"

@functional
Scenario: 5 Verify that the widget is unable to plan a journey if no locations are entered into the widget.
	When the user clicks the plan my journey button
	Then the user should see the error as "The From field is required."
	And the user should see the error as "The To field is required."
