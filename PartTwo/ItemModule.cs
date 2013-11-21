/* 1. Explain that we'll do a little bit more advanced tutorial creating a todolist
 * 
 * 2. Pull in the static repository and create the Item model
 *  - Explain quickly what it does
 *  
 * 3. Create Get that returns a view with a list of products
 * 
 * 4. Create a Get Request for create page
 * 
 * 5. Create a Post request for the create whilst at the same time explaining about model binding
 *  - Explain that this.Bind is an extension method in the Nancy.ModelBinding namespace
 *  - Show inserting it into the repository
 *  - Explain the reponse object. "Just like in ASP.NET"
 *  
 * 6. Add in validation
 *  - Explain that there is a new get package for fluent and annotations
 *  - pull in nuget package Nancy.Validation.DataAnnotations
 *  - Demonstrate how to add the validation in
 *  - Explain that you can just remove the data annotations package and add the fluent package and you wont have to change anything.
 *  - NEED TO FIGURE OUT HOW TO HANDLE ERRORS
 *  
 * 7. Add in Twitter Bootstrap to make it all nice
 *  - Explain about the default convention for static files and that it can be changed
 *  - Remove fonts and scripts 
 *  - I should add in CSS when creating html. Maybe want some snippets for this
 * 
 * 8. Explain about TinyIOC and inject the repository
 *  - Explain that things are automatically resolved. 
 *  - Check that Tiny resolves to a list
 *  - create interface
 *  - mention about the advantage that defining routes in the constructor elimates the need for a lot of private fields
 * 
 * 9. Show content negotiation by extending the methods to accept json requests
 *  (TBA)
 *  
 * 10. Testing
 *  - Talk about the configurable bootstrapper, that you could use default but that would use default bindings plus load all modules. Using config allows mocking and is faster
 *  - Test get for html and json
 *  - Test post for html and json
 *  
*/

namespace PartTwo
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    using Nancy;
    using Nancy.ModelBinding;
    using Nancy.Validation;

    public class ItemModule : NancyModule
    {
        public ItemModule(IRepository repo)
        {
            Get["/"] = _ => this.Negotiate.WithModel(repo.All()).WithView("Index");

            Get["/Create"] = _ => this.View["Create", new Item()];

            Post["/Create"] = _ =>
                {
                    var model = this.Bind<Item>();
                    var result = this.Validate(model);

                    if (!result.IsValid)
                    {
                        this.ViewBag.Errors = result.Errors;
                        return this.Negotiate.WithModel(model).WithView("Create").WithMediaRangeModel("text/json", result.Errors);
                    }

                    repo.Add(model);

                    return this.Negotiate.WithMediaRangeModel("application/json", model).WithMediaRangeModel("text/html", this.Response.AsRedirect("/"));
                };
        }

        /* public ItemModule()
    {
        Get["/"] = _ =>
        {
            var repo = new Repository();
            return View["Index", repo.All()];
        };

        Get["/Create"] = _ => View["Create", new Item()];

        Post["/Create"] = _ =>
        {
            var model = this.Bind<Item>();
            var result = this.Validate(model);

            if (!result.IsValid)
            {
                this.ViewBag.Errors = result.Errors;
                return this.Negotiate.WithModel(model)
                    .WithView("Create")
                    .WithMediaRangeModel("text/json", result.Errors);
            }

            var repo = new Repository();
            repo.Add(model);

            return this.Negotiate
                .WithMediaRangeModel("application/json", model)
                .WithMediaRangeModel("text/html", this.Response.AsRedirect("/"));
        }; 
    } */

        public class Item
        {
            public int Id { get; set; }

            [Required]
            public string Task { get; set; }
        }

        public interface IRepository
        {
            List<Item> All();

            int Add(Item item);
        }

        public class Repository : IRepository
        {
            private static readonly List<Item> Items = new List<Item> { new Item { Id = 1, Task = "Start Talk" }, new Item { Id = 2, Task = "Freak out a little about people watching me type" } };

            public List<Item> All()
            {
                return Items;
            }

            public int Add(Item item)
            {
                var nextId = Items.Max(i => i.Id) + 1;
                item.Id = nextId;
                Items.Add(item);

                return nextId;

            }
        }
    }
}