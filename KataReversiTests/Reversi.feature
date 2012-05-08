Feature: Available moves
	In order to make it easier to play reversi
	As a player
	I want to be able to see all available moves

@mytag
Scenario: Starting position full check
	Given I have an starting board
	When I enter that it's blacks turn
	Then the result should be 'C5,D6,E3,F4'

Scenario: First move white player
	Given I have this board:
	| black | white |
	| E4,D4,D3,D5 | E5 |
	When I enter that it's whites turn
	Then the result should be 'C3,C5,E3'

Scenario: Second move black player
	Given I have this board:
	| black | white |
	| E4,D4,D3 | C5,D5,E5 |
	When I enter that it's whites turn
	Then the result should be 'B6,D6,E6,F6'