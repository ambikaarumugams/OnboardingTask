Feature: Language Feature

As a registered user, I would like to add the languages into my profile page
So that others can see which languages I know

Background:
	Given I navigate to the profile page as a registered user
	When I Add the following New Language and select New Language level:
		| NewLanguage | NewLanguageLevel |
		| Tamil       | Native/Bilingual |
		| English     | Fluent           |
		| French      | Basic            |
		| German      | Conversational   |
	And I should verify the languages has been added successfully
	Then I should verify the languages listed in my profile

Scenario: Add languages into the profile
	
Scenario: As a user, I want to Edit the existing languages in my profile
	When I update the language and language level:
		| ExistingLanguage | LanguageToUpdate | LanguageLevelToUpdate |
		| Tamil            | Hindi            | Conversational        |
		| French           | Chinese          | Basic                 |
	Then I should see the updated language in my profile

Scenario: As a user, I want to delete the existing language
	When I click the delete icon of the language
	Then I shouldn't see the languages list
