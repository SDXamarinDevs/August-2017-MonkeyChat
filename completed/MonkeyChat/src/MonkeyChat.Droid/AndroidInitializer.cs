using System;
using Android.App;
using AzureMobileClient.Helpers.Accounts;
using DryIoc;
using Prism.DryIoc;

namespace MonkeyChat.Droid
{
    public class AndroidInitializer : IPlatformInitializer
    {
        private Application CurrentApplication { get; }

        public AndroidInitializer(Application application)
        {
            CurrentApplication = application;
        }

        public void RegisterTypes(IContainer container)
        {
            // Register Any Platform Specific Implementations that you cannot 
            // access from Shared Code
            container.UseInstance(CurrentApplication);
            //container.Register<ISecureStore, SecureStore>(Reuse.Singleton);
        }
    }
}
