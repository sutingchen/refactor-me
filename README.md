# Refactor Me - Xero Technical Assessment

The purpose of this project is to refactor the Web APIs with improved code quality (e.g., readability, extensibility and security).

## Major Changes

### 1. Use Entity Framework

#### Reason

* To work with domain-specific models without having to write code that requires understanding database tables and columns where the data is stored.
* Without using Entity Framework, there is no optimal way to write generic methods by using SqlCommands (especially for Insert and Update). Using Entity Framework helps to reduce development time and improve the CRUD performance.

### 2. Global Exception Handler
#### Reason
* To capture all the unhandled exceptions at the global level instead of implementing 'try catch' in every controller methods.


### 3. Dependency Injection (Unity Container)
Register service classes that process the information and return the result of a specific entity (e.g. Products) to the caller (e.g. ProductsController). 
#### Reason
* To remove the dependency between classes (e.g., loosely coupled controller class and entity class) by calling the service to process data. In a large project with multiple classes depending on other classes directly, there will be a lot of code changes if one of the dependending class is changed.
* To ease unit testing by using interface to abstract dependency implementation. Unit testing can pass different classes for multiple testing purposes.


### 4. Generic Services and Interfaces
#### Reason
* To reduce repetitive code