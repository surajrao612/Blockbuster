# Blockbuster API Project

### Project Details:

The task is to develop a web application enabling users to compare movie prices from two providers and display cheapest price for the movie.

## Backend

Following Concepts used:

1) API rate limiter using Sliding Window Algorithm
2) Clean Architecture with CQRS
3) Data Cache when Provider API fails
4) Retrieve API data parallelly
5) TDD and BDD using xUnit and SpecFlow
6) Use Polly on Http
7) Automapper for Data Transfer Objects


Future Implentation:
1) Pagination
2) Improve on the caching to retrieve from cache using a session id to prevent current scenario,
To stop retrieve all from competitor api again to find the price

## FrontEnd

Front end application called blockbuster-app

Created using React+Vite with bootstrap