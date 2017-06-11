using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using FundsLibrary.InterviewTest.Web.Repositories;
using System.Configuration;

namespace FundsLibrary.InterviewTest.Web.Installers
{
    public class RepositoriesInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            var serviceAppUrl = ConfigurationManager.AppSettings["fundsLibraryUrl"];
            var authToken = ConfigurationManager.AppSettings["authToken"];

            container.Register(Classes.FromThisAssembly()
                .Pick().If(t => t.Name.EndsWith("Repository"))
                .WithService.AllInterfaces());

            container.Register(Component
                .For<IODataClientWrapper>()
                .UsingFactoryMethod(_ => new ODataClientWrapper(serviceAppUrl, authToken)));

            container.Register(Component
                .For<IHttpClientWrapper>()
                .UsingFactoryMethod(_ => new HttpClientWrapper("http://localhost:50135/Service/")));
        }
    }
}
