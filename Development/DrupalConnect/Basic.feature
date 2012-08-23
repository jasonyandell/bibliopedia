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

Scenario: Create a test item
	Given sample data
	And a connection
	Then I can create a new node



#Scenario: Pointers
#	Given a node
#	And a citation
#	Then I can create a pointer to the cited item

#Scenario: Hydrating and referencing pointers 
#	Given a poiter
#	And a new node that is the derefeence of that point
#	Then I can hydrate the dereference

#Scenario: Node flyweight
#	Given a representation of a node
#	Then that node can be referenced as a Citation

#Scenario: Citation is a node
#	Given a citation
#	Then a node exists for the work

#Scenario: Citiation is a Pointer
#	Given a citation 
#	Then reference to the work it is citing is always done through a pointer

	