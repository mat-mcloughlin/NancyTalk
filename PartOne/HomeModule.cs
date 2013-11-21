/* 1. Remove all references except Microsoft.CSharp.
 * 
 * 2. Install Nancy.Hosting.Aspnet.
 *  - Explain that there are other options
 *  - Note that it only installs two dlls
 * 
 * 3. Create HomeModule
 *  - Inherit from NancyModule. 
 *  - Explain that Nancy will pick up an class that inherits from NancyModule
 *  
 * 4. Create a basic get route. 
 *  - Explain that there is an indexer property for each HTTP Verb.
 *  - That it excepts a lambda expression.
 *  - All routes are defined within the constructor. Which has advantages to see later.
 *  
 * 5. Demonstrate route arguments.
 *  - Mention that there are multiple different ways of defining a route paramter including constraints
 *  - That the input argument on the lambda is a dynamic that gets populate all the route arguments. 
 *  
 * 6. Demonstrate Returning a view because a string isn't always ideal.
 *  - Explain the super simple view engine and its limitations
 *  - Explain the default folder structure for views and that it can be overridden using the Bootstrapper which you will learn about in Damians talk.
 *  - Mention about if you don't supply a view name it will look for one the same name as the model. (Why is this?)
 *  
 * 8. Show that you can pass the parent route into base
 *  - Cut down functions into expressions at this point
*/
namespace PartOne
{
    using Nancy;

    public class HomeModule : NancyModule
    {
        public HomeModule() : base("/Home")
        {
            this.Get["/"] = _ => "Hello NDC";

            this.Get["/{name}"] = arg => "Hello " + arg.Name;

            this.Get["/WithAView/{name}"] = arg => this.View["Index", new { arg.Name }];
        }
    }

    /* public class HomeModule : NancyModule
    {
        public HomeModule()
        {
            Get["/Home"] = _ =>
                {
                    return "Hello NDC";
                };

            Get["/Home/{name}"] = arg =>
                {
                    return "Hello " + arg.Name;
                };

            Get["/Home/WithAView/{name}"] = arg =>
                {
                    return View["Index", new { Name = arg.Name }];
                };
        }
    } */
}