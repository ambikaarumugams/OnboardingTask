Feature: Skills Feature
As a registered user, I would like to add the skills into my profile page
So that others can see which skills I know


Background:
	Given I navigate to the profile page as a registered user
	When I Add New Skills and select Skill level:
		| New Skills      | Skill level  |
		| Problem Solving | Beginner     |
		| Team work       | Intermediate |
		| Communication   | Expert       |
		| Adaptability    | Intermediate |
	Then I should see the skills listed in my profile

Scenario: Add skills into the profile
	
Scenario: As a user, I want to Edit the existing skills in my profile
	When I update the skill and skill level:
		| Existing Skill | Skill to Update | Skill level to Update |
		| Communication  | Public Speaking | Expert                |
		| Adaptability   | Technical       | Expert                |
	Then I should see the updated skill in my profile

Scenario: As a user, I want to delete the existing skill
	When I click the delete icon of the skill
	Then I shouldn't see the skills list

