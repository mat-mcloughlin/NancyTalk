THINGS TO SORT
==============

- Slides
- Adaptors for Mac
- Screen Resolutions
- Clean install
- Run through without resharper
- Default colour schemes ?
- Water

???

INTRO TO NANCY
==============

- When it was created and by who
- Based on sinatra
- Super Duper Happy Path???
- Advantages
 1. Concise, application in a tweet
 2. Testability
 3. Community, bug fixes within the day, Jabbr

PART ONE
========

1. Install Nancy.Hosting.Aspnet.
 - Explain that there are other hosting options
 - Note that it only installs two dlls

2. Create HomeModule
 - Inherits from NancyModule
 - Explain that Nancy will pick up any class that inherits from NancyModule
 
3. Create a basic get route.
 - Explain that there is an indexer property for each HTTP Verb
 - That it excepts a lambda expression
 - All routes are defined within the constructor. Which has soom advantages such as lack of private fields
 
4. Demonstrate route arguments
 - Mention that there are multiple different ways of defining a route argument including constraints
 - That the input parameter on the lambda is a dynamic that gets populate with all the route arguments (Think its case insensitive?) 
 
5. Demonstrate returning a view
 - Explain about the super simple view engine and its limitations
 - Talk about the default folder structure for views and that it can be overridden using the Bootstrapper which you will learn about in Damians talk.
 - Mention that if you don't supply a view name it will look for one the same name as the model. (Why is this?)
 
6. Show that you can pass the parent route into base
 - Cut down functions into expressions at this point

PART TWO
========

1. Explain that we'll do a little bit more advanced tutorial creating a todo list

2. Pull in the static repository and create the Item model
 - Explain quickly what it does
 
3. Create Get that returns a view with a list of products

4. Create a Get Request for create page

5. Create a Post request for the Create whilst at the same time explaining about model binding
 - Explain that this.Bind is an extension method in the Nancy.ModelBinding namespace
 - Show inserting it into the repository
 - Explain the Response object.
 
6. Add in validation
 - Explain that there are two nuget packages for fluent and annotations
 - pull in nuget package Nancy.Validation.DataAnnotations
 - Demonstrate how to add the validation in
 - Explain that you can just remove the data annotations package and add the fluent package and you wont have to change anything.
 - VIEWBAG FUNCTIONALITY TO BE ADDED TO SUPER SIMPLE VIEW ENGINE BEFORE DEMO FOR RETURNING ERRORS TO VIEW
 
7. Add in Twitter Bootstrap to make it all nice
 - Explain about the default convention for static files and that it can be changed
 - Remove fonts and scripts 
 - I should add in CSS when creating html. Maybe want some snippets for this

8. Explain about TinyIOC and inject the repository
 - Explain that things are automatically resolved (Check that Tiny resolves to IList when interface used multiple times)
 - create interface and replace usage of concrete. Refactor where possible

9. Show content negotiation by extending the methods to accept json requests
 - Explain about the WithMediaRangeModel allows you to specify
 
10. Testing
 - Talk about the configurable bootstrapper, that you could use default but that would use default bindings plus load all modules. Using config allows mocking and is faster as you can limit modules loaded
 - Test get for html and json
 - Test post for html to make sure it redirects 

11. Adding to exisitng MVC App
 - Create demo MVC app
 - Add in Nancy
