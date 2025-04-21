Feature: SkillsNegative

As a user, I shouldn't be able to add skills and it's level

Background:
	Given I navigate to the profile page as a registered user

@negative
Scenario: As a user, I shouldn't able to add skills and it's level by giving skill field empty
	When I click Add New button, leave the skill field empty,choose the skill level and click the Add button
	Then I should see "Please enter skill and experience level" error message

Scenario: As a user, I shouldn't able to add skills and it's level by not choosing the skill level
	When I click Add New button, enter the skill field, not choosing the skill level and click the Add button
	Then I should see "Please enter skill and experience level" error message

Scenario: As a user, I shouldn't able to add skills by empty the skill field and not choosing the skill level
	When I click Add New button, empty the skill field, not choosing the skill level and click the Add button
	Then I should see "Please enter skill and experience level" error message

Scenario: As a user, I should able to Cancel the operation (Add, Edit and Delete)
	When I click Add New button, enter the skill and it's level or leave empty and click Cancel button
	Then I should able to Cancel the operation and verify that no changes has happened

Scenario: As a user, I shouldn't able to add the same skill and different skill level times
	When I Add the following NewSkills and select SkillLevel:
		| NewSkills       | SkillLevel   |
		| Problem Solving | Beginner     |
		| Problem Solving | Intermediate |
	Then I should see the error message "Duplicated data"

Scenario: As a user, I shouldn't able to add the same skill and same skill level multiple times
	When I Add the following NewSkills and select SkillLevel:
		| NewSkills       | SkillLevel   |
		| Problem Solving | Beginner     |
		| Problem Solving | Beginner   |
	Then I should see the error message "This skill already exist in your skill list"
