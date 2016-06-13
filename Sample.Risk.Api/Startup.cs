using Microsoft.Owin;

using Sample.Risk.Api;

[assembly: OwinStartup(typeof(Startup))]

namespace Sample.Risk.Api
{
    using System.Web.Http;

    using Owin;

    public class Startup
    {
        #region Public Methods

        public void Configuration(IAppBuilder appBuilder)
        {
            HttpConfiguration config = new HttpConfiguration();
            //ToDo: All cruscut concerns like, exception handlig, security, authentication and authorization middlewares should be defined here
            config.MapHttpAttributeRoutes();

            appBuilder.UseWebApi(config);
        }

        #endregion
    }
}