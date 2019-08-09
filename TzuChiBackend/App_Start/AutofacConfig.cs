using Autofac;
using Autofac.Integration.Mvc;
using System.Web.Mvc;
using TzuChiBackend.Controllers;

using System.Configuration;

namespace TzuChiBackend
{
	public class AutofacConfig
	{
		public static void Register()
		{
			var builder = new ContainerBuilder();

			builder.RegisterControllers(typeof(SchoolDiaryController).Assembly);


			builder.RegisterType<BlogContext>()
				.WithParameter("connectionString", ConfigurationManager.ConnectionStrings["BlogContext"].ConnectionString)
				.InstancePerRequest();

			builder.RegisterGeneric(typeof(BlogRepository<>)).As(typeof(IBlogRepository<>)).InstancePerRequest();

			builder.RegisterType<PostService>().As<IPostService>().InstancePerRequest();

			var container = builder.Build();
			DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
		}
	   
	}
}