using Autofac;
using Autofac.Integration.Mvc;
using LEON;
using LEON.BusinessClasses;
using LEON.Caching;
using LEON.Events;
using LEON.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace WB.Api
{
    public class Starter
    {
        /// <summary>
        /// 是否分布式
        /// </summary>
        private static bool distributedDeploy = false;


        /// <summary>
        /// 启动
        /// </summary>
        public static void Start()
        {
            InitializeDIContainer();

            InitializeMVC();
        }


        /// <summary>
        /// 初始化DI容器
        /// </summary>
        private static void InitializeDIContainer()
        {
            var containerBuilder = new ContainerBuilder();

            #region 运行环境及全局设置

            //获取web引用的所有LEON开头的程序集
            AssemblyName[] assemblyNames = Assembly.GetExecutingAssembly().GetReferencedAssemblies().Where(n => n.Name.StartsWith("LEON")).ToArray();
            List<Assembly> assemblyList = assemblyNames.Select(n => Assembly.Load(n)).ToList();
            assemblyList.AddRange(Assembly.GetExecutingAssembly().GetReferencedAssemblies().Where(n => n.Name.StartsWith("WB")).Select(n => Assembly.Load(n)).AsEnumerable());
            Assembly[] assemblies = assemblyList.ToArray();

            //批量注入所有的Service
            containerBuilder.RegisterAssemblyTypes(assemblies).Where(t => t.Name.EndsWith("Service")).AsSelf().AsImplementedInterfaces().SingleInstance().PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);

            //批量注入所有的Repository
            containerBuilder.RegisterAssemblyTypes(assemblies).Where(t => t.Name.EndsWith("Repository")).AsSelf().AsImplementedInterfaces().SingleInstance().PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);

            //批量注入所有的EventMoudle
            containerBuilder.RegisterAssemblyTypes(assemblies).Where(t => typeof(IEventMoudle).IsAssignableFrom(t)).As<IEventMoudle>().SingleInstance().PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);


            //注册运行环境
            containerBuilder.Register(c => new DefaultRunningEnvironment()).As<IRunningEnvironment>().SingleInstance();

            #endregion

            #region 范型注入

            containerBuilder.RegisterGeneric(typeof(SettingManager<>)).As(typeof(ISettingsManager<>)).SingleInstance().PropertiesAutowired();
            containerBuilder.RegisterGeneric(typeof(SettingsRepository<>)).As(typeof(ISettingsRepository<>)).SingleInstance().PropertiesAutowired();
            containerBuilder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)).SingleInstance().PropertiesAutowired();

            #endregion

            #region 业务逻辑实现

            //查询UserID与UserName的查询器
            containerBuilder.Register(c => new DefaultUserIdToUserNameDictionary()).As<UserIdToUserNameDictionary>().SingleInstance().PropertiesAutowired();

            //注册缓存
            if (distributedDeploy)
            {
                containerBuilder.Register(c => new DefaultCacheService(new MemcachedCache(), new RuntimeMemoryCache(), 1.0F, true)).As<ICacheService>().SingleInstance();
            }
            else
            {
                containerBuilder.Register(c => new DefaultCacheService(new RuntimeMemoryCache(), 1.0F)).As<ICacheService>().SingleInstance();
            }

            #endregion


            containerBuilder.RegisterControllers(assemblies).PropertiesAutowired();
            containerBuilder.RegisterSource(new ViewRegistrationSource());
            containerBuilder.RegisterModelBinders(assemblies);
            containerBuilder.RegisterModelBinderProvider();
            containerBuilder.RegisterFilterProvider();
            containerBuilder.RegisterModule(new AutofacWebTypesModule());

            IContainer container = containerBuilder.Build();

            //将Autofac容器中的实例注册到mvc自带DI容器中（这样才获取到每请求缓存的实例）
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            DIContainer.RegisterContainer(container);

        }


        /// <summary>
        /// 初始化MVC环境
        /// </summary>
        private static void InitializeMVC()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}