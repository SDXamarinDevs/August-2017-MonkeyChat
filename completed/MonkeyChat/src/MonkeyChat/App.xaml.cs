using System;
using System.Threading.Tasks;
using MonkeyChat.Services;
using MonkeyChat.Views;
using DryIoc;
using Prism.DryIoc;
using MonkeyChat.Helpers;
using MonkeyChat.Data;
using AzureMobileClient.Helpers;
using AzureMobileClient.Helpers.Accounts;
using Microsoft.WindowsAzure.MobileServices;
using Prism.Logging;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using DebugLogger = MonkeyChat.Services.DebugLogger;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace MonkeyChat
{
    public partial class App : PrismApplication
    {
        /* 
         * NOTE: 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App()
            : this(null)
        {
        }

        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();
            SetupLogging();
            Resources.Add("dataContext", Container.Resolve<IAppDataContext>());

            await NavigationService.NavigateAsync("SplashScreenPage");
        }

        protected override void RegisterTypes()
        {
            // ICloudTable is only needed for Online Only data
            Container.Register(typeof(ICloudTable<>), typeof(AzureCloudTable<>), Reuse.Singleton);
            Container.Register(typeof(ICloudSyncTable<>), typeof(AzureCloudSyncTable<>), Reuse.Singleton);

            Container.UseInstance<IMobileServiceClient>(new MobileServiceClient(Secrets.AppServiceEndpoint));
            Container.RegisterMany<AppDataContext>(reuse: Reuse.Singleton,
                                                   serviceTypeCondition: type =>
                                                        type == typeof(IAppDataContext) ||
                                                        type == typeof(ICloudAppContext));

            Container.RegisterTypeForNavigation<NavigationPage>();
            Container.RegisterTypeForNavigation<SplashScreenPage>();
            Container.RegisterTypeForNavigation<ChatPage>();
            Container.RegisterTypeForNavigation<LoginPage>();
            Container.RegisterTypeForNavigation<MenuPage>();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle IApplicationLifecycle
            base.OnSleep();

            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle IApplicationLifecycle
            base.OnResume();

            // Handle when your app resumes
        }

        protected override ILoggerFacade CreateLogger() =>
            new DebugLogger();

        private void SetupLogging()
        {
            // By default, we set the logger to use the included DebugLogger,
            // which uses System.Diagnostics.Debug.WriteLine to print your message. If you have
            // overridden the default DebugLogger, you will need to update the Logger here to
            // ensure that any calls to your logger in the App.xaml.cs will use your logger rather
            // than the default DebugLogger.
            TaskScheduler.UnobservedTaskException += (sender, e) =>
            {
                Logger.Log(e.Exception.ToString(), Category.Exception, Priority.High);
            };
        }
    }
}
