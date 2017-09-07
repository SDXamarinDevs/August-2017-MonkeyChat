using System;
using AzureMobileClient.Helpers.Accounts;
using DryIoc;
using Prism.DryIoc;

namespace MonkeyChat.iOS
{
    public class iOSInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainer container)
        {
            // Register Any Platform Specific Implementations that you cannot 
            // access from Shared Code
            container.Register<ISecureStore, SecureStore>(Reuse.Singleton);
        }
    }
}
