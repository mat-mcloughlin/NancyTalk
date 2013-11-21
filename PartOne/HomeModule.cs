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

/*
    public class HomeModule : NancyModule
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
    }
*/
}