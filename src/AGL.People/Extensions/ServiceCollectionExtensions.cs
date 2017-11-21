namespace AGL.People.Extensions
{
    using AGL.People.Models.Configuration;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyModel;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// External load of services
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        private const string assemblySearch = "AGL.People";

        /// <summary>
        /// Method to load the services setup from the environment files.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="env"></param>
        /// <returns></returns>
        public static IServiceCollection ConfigureJsonServices(this IServiceCollection services, IHostingEnvironment env)
        {
            CheckEnvironmentFileExists(env);

            //ensure all assemblies has been loaded
            DependencyContext.Default.GetDefaultAssemblyNames().Where(x => x.Name.Contains(assemblySearch)).ToList().ForEach(y => Assembly.Load(y));

            List<Service> requiredServices = LoadService<Service>(env, "services");

            //add dynamic service per environment settings.
            foreach (var service in requiredServices)
            {
                services.Add(new ServiceDescriptor(serviceType: getTypeByName(service.ServiceType),
                                                   implementationType: getTypeByName(service.ImplementationType),
                                                   lifetime: service.Lifetime));
            }

            //garbage collect
            loadedAssemblies = null;

            return services;
        }

        private static List<T> LoadService<T>(IHostingEnvironment env, string environmentParameter)
        {
            string fileSetting = $"appsettings.{env.EnvironmentName}.json".Replace("'", "");

            var jsonServices = JObject.Parse(File.ReadAllText(fileSetting))[environmentParameter];
            var requiredServices = JsonConvert.DeserializeObject<List<T>>(jsonServices.ToString());
            return requiredServices;
        }

        /// <summary>
        /// Check environment file exists
        /// </summary>
        /// <param name="env"></param>
        private static void CheckEnvironmentFileExists(IHostingEnvironment env)
        {
            string fileSetting = $"appsettings.{env.EnvironmentName}.json".Replace("'", "");

            if (!File.Exists(fileSetting))
                throw new ArgumentException($"Configuration file for Service does not exist - {env.EnvironmentName} -");
        }

        //static for temporary load of assemblies.
        private static IEnumerable<Assembly> loadedAssemblies;

        /// <summary>
        /// Method to return the Assemblies to match for the dynamic service load.
        /// </summary>
        /// <param name="className"></param>
        /// <returns></returns>
        internal static Type getTypeByName(string className)
        {
            Type returnVal = null;

            if (loadedAssemblies == null)
            {
                loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies().Where(x => x.FullName.Contains(assemblySearch));
            }

            foreach (Assembly a in loadedAssemblies)
            {
                Type[] assemblyTypes = a.GetTypes();
                for (int j = 0; j < assemblyTypes.Length; j++)
                {
                    if (assemblyTypes[j].Name == className)
                    {
                        returnVal = assemblyTypes[j];
                        break;
                    }
                }
            }

            return returnVal;
        }
    }
}