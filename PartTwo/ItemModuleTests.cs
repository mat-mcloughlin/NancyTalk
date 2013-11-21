namespace PartTwo
{
    using System.Collections.Generic;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using Nancy;
    using Nancy.Testing;

    [TestClass]
    public class ItemModuleTests
    {
        [TestMethod]
        public void ItemGetReturnsJson()
        {
            var repositoryMoq = new Mock<IRepository>();
            repositoryMoq.Setup(m => m.All()).Returns(new List<Item> { new Item { Id = 1, Task = "Start writing some tests" } });

            // var browser = new Browser(new DefaultNancyBootstrapper());
            var browser = new Browser(new ConfigurableBootstrapper(
            with =>
            {
                with.Module<ItemModule>();
                with.Dependency(repositoryMoq.Object);
            }));

            var response = browser.Get("/", with => with.Accept("application/json"));

            var actual = response.Body.DeserializeJson<IList<Item>>();

            Assert.AreEqual("Start writing some tests", actual[0].Task);
        }

        [TestMethod]
        public void ItemGetReturnsHtml()
        {
            var repositoryMoq = new Mock<IRepository>();
            repositoryMoq.Setup(m => m.All()).Returns(new List<Item> { new Item { Id = 1, Task = "Start writing some tests" } });

            var browser = new Browser(new ConfigurableBootstrapper(
            with =>
            {
                with.Module<ItemModule>();
                with.Dependency(repositoryMoq.Object);
            }));

            var response = browser.Get("/", with => with.Accept("text/html"));

            response.Body["ul li"].AllShouldContain("Start writing some tests");
        }

        [TestMethod]
        public void ItemGetsCreatedFromFormAndRedirectsToRoot()
        {
            var repositoryMoq = new Mock<IRepository>();
            repositoryMoq.Setup(m => m.Add(It.IsAny<Item>()));

            var browser = new Browser(new ConfigurableBootstrapper(
            with =>
            {
                with.Module<ItemModule>();
                with.Dependency(repositoryMoq.Object);
            }));

            var response = browser.Post(
                "/Create",
                with =>
                    {
                        with.Accept("text/html");
                        with.FormValue("Task", "Finally Finished Testing");
                    });

            response.ShouldHaveRedirectedTo("/");
            Assert.AreEqual(HttpStatusCode.SeeOther, response.StatusCode);
        }
    }
}