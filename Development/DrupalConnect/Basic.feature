Feature: Drupal
	In connect to Drupal
	As a bot
	I want to be able to connect
	And I want to be able to perform basic GET on a Node

Scenario: Connect
	Given a connection
	And credentials
	When I can perform a basic get
	Then I get some data back 

