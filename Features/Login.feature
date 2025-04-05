Feature: Login Functionality
  As a user, I want to log in to the application to access restricted content.


  Background: 
   Given I am on the home page
 
  Scenario: Perform a successful login
   When I enter valid username and valid password
   Then I should see the successful message 

  Scenario: Fail login with invalid username
   When I enter invalid username and valid password
   Then I should see an error message
    
  Scenario: Fail login with invalid password
   When I enter a valid username and invalid password
   Then I should see an error message
 
  Scenario: Fail login with empty credentials
   When I enter empty credentials
   Then I should see an error message

