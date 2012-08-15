Feature: Drupal create read update delete
	In order to work with data in Drupal
	As a bot
	I want to be able to perform basic operations

Background:
	Given credentials
	And a connection

Scenario: Create
	When I create a new test node
	Then I can determine that it succeeded
	And I get some data back


