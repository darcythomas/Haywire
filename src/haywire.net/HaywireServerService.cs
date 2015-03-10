using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace haywire
{
    class HaywireServerService : IDisposable
    {

        private HaywireAppBuilder appBuilder = new HaywireAppBuilder();

        private HaywireRequestCallback rootCallback;


        HaywireServer server;
        public HaywireServerService(Uri url, Action<IAppBuilder> startup)
        {           
            SetUpHaywire(url,startup);
        }

        private void SetUpHaywire(Uri url, Action<IAppBuilder> startup)
        {

            HaywireConfiguration config;
            config.ListenAddress = url.Host;
            config.ListenPort = url.Port;
            server = new HaywireServer(config);

            rootCallback = new HaywireRequestCallback(appBuilder.GetRoot);
            server.AddRoute("/", rootCallback);

            startup.Invoke(appBuilder);
            appBuilder.Build(typeof(HaywireWebApp));
            server.StartAcceptingRequests();
        }
        public void Dispose()
        {
            server.Shutdown();
        }
    }
}
