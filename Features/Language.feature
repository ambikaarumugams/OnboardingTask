Feature: Language

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
	And I should see the languages and verify it has been added successfully
	Then I should see the languages listed in my profile and verify it

Scenario: Add languages into the profile
	
Scenario: As a user, I want to Edit the existing languages in my profile
	When I update the language and language level:
		| ExistingLanguage | LanguageToUpdate | LanguageLevelToUpdate |
		| Tamil            | Hindi            | Conversational        |
		| French           | Chinese          | Basic                 |
	Then I should see the success message and updated language in my profile

Scenario: As a user, I want to delete the existing languages from my profile
	When I click the delete icon corresponding to the following languages:
		| LanguageToBeDeleted |
		| French              |
		| German              |
	Then I should see a success message for each deleted language
	And the languages table should be empty if all languages have been deleted

Scenario: As a user, I want to Edit the existing languages by giving same language and different level in my profile
	When I update the language and language level:
		| ExistingLanguage | LanguageToUpdate | LanguageLevelToUpdate |
		| Tamil            | Tamil            | Conversational        |
	Then I should see the success message and updated language in my profile
	