# e-conomic & sproom hiring task

A very simple Client and Api for adding Projects and adding time spent in project.

For more information regarding the exercise please read [Timelogger](https://github.com/e-conomic/timelogger) readme.

This was done using the [Clean Architecture Solution Template](https://github.com/jasontaylordev/CleanArchitecture) by [JasonTaylorDev](https://github.com/jasontaylordev)

A good video and good explanation about this is given by [Nick Chapsas](https://www.youtube.com/watch?v=YiVqwoFMieg).

Feel free to explore the application.

When running the application on debug a database in memory will be created and populated with test data.

In the web application you should be able to Add Project, Filter by the project name, register times in a project and see all the times registered in a specific project.

In the api you all the endpoints needed to manage projects and times registration.
For instance you can:

Get a paginated list of a project.
Delete a project
Update a project

Get a paginated list of a registered times by project id.
Delete a time registration
Update a time registration

There you already have the endpoints for a given operator (header: operatorId).

Get All Collisions Events  
Get A Single Collision Event  
Get All Collision Events in a Warning State (Collision Probability >= 0.75)  
Cancel A Collision Event  
Delete A Collision Event  

To run this project you will need both .NET 8 and Node installed on your environment.

## Considerations

I didn't had the time to cover the api with unit tests and integration tests, some were added but don't cover everything.

The client project structure and naming is not as pleasant as I would wanted to be.
Unfortenally I didn't had the time to add tests to client project, being my background most a Backend Developer and a C# it was a little time consuming understand how are things with react at this day and age.

