using Owin;

namespace Provider.Tests
{
    public class TestStartup
    {
        public void Configuration(IAppBuilder app)
        {
            var apiStartup = new Startup();
            apiStartup.Configuration(app);
        }

    }
}
