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
 * 7. Add in Boot strapper to make it all nice
 *  (TBA)
 * 
 * 8. Explain about TinyIOC and inject the repository
 *  (TBA)
 *   
 * 9. Testing
 *  (TBA)
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
        public ItemModule()
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

                    if (result.IsValid)
                    {
                        var repo = new Repository();
                        repo.Add(model);

                        return Response.AsRedirect("/");
                    }
                    else
                    {
                        return View["Create", model];
                    }
                };
        }
    }

    public class Item
    {
        public int Id { get; set; }

        [Required]
        public string Task { get; set; }
    }

    public class Repository
    {
        private static readonly List<Item> Items = new List<Item>
                                             {
                                                 new Item {Id = 1, Task = "Start Talk"},
                                                 new Item {Id = 2, Task = "Freak out a little about people watching me type"}
                                             };

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