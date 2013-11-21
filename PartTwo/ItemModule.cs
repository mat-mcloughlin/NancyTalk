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
            this.Get["/"] = _ => this.Negotiate.WithModel(repo.All()).WithView("Index");

            this.Get["/Create"] = _ => this.View["Create", new Item()];

            this.Post["/Create"] = _ =>
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

                    repo.Add(model);

                    return this.Negotiate
                        .WithMediaRangeModel("application/json", model)
                        .WithMediaRangeModel("text/html", this.Response.AsRedirect("/"));
                };
        }
    }

/*
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
    }
*/

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
        private static readonly List<Item> Items = new List<Item>
            {
                new Item { Id = 1, Task = "Start Talk" }, 
                new Item { Id = 2, Task = "Freak out a little about people watching me type" }
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