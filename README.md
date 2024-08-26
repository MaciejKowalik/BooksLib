# BooksLib

BooksLib is a .NET library designed to manage and interact with book-related data. It provides functionalities for retrieving, managing, and interacting with book and order data, utilizing external APIs and internal caching mechanisms.

## Table of Contents

- [Overview](#overview)
- [Project Structure](#project-structure)
- [Configuration](#configuration)
- [Testing](#testing)

## Overview

The `BooksLib` library is built with a focus on flexibility and modularity, allowing easy integration with external services and APIs. It leverages `AutoMapper` for mapping data between different models and uses caching to optimize performance.

Key features include:
- Retrieving book and order data from external APIs.
- Caching frequently accessed data.
- Handling various types of errors, including validation, mapping, and deserialization errors.

## Project Structure

The project is organized into the following main directories:

- **BooksLib.Domain**: Contains core domain models and abstractions.
- **BooksLib.Infrastructure**: Includes implementations of domain abstractions, API service wrappers, and caching mechanisms.
- **BooksLib.Tests**: Unit tests for validating the functionality of the library.

## Configuration
The library allows for configuration through the BookLibOptions class, which can be provided via dependency injection. For example:

```
services.Configure<BookLibOptions>(options =>
{
    options.OrdersCacheExpirationTime = 10; // In minutes
});
```

Example configuration in appsettings.json:

```
{
  "BookLibOptions": {
    "OrdersCacheExpirationTime": 10
  }
}
```

## Testing
Unit tests are included in the BooksLib.Tests project. The tests are written using the xUnit framework, with Moq used for mocking dependencies. 
The tests cover various scenarios, including successful operations, validation failures, cache hits.
