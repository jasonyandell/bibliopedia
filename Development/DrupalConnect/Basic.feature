Feature: Drupal
	In order to put data into Drupal
	As a bot
	I want to be able to perform basic operations

Background:
	Given a connection

Scenario: Connect
	Given credentials
	When I can perform a basic get
	Then I get some data back 

