Feature: Blockbuster

This feature is used to test the blockuster mpvies functionality

@tag1
Scenario: Get All Movies
	Given User wants to get all movies
	When Performs an all movies search
	Then User receives the movies list

Scenario: Get movie details with valid movie Id
	Given User wants to get movie details with a valid id
	When Performs an get movie info search
	Then User receives the movie info

Scenario: Get movie details with invalid movie Id
	Given User wants to get movie details with an invalid id
	When Performs an get movie info search
	Then User receives not found
