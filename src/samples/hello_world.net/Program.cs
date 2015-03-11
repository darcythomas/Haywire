using Owin;
using System;
using haywire;
using System.Diagnostics;

namespace haywire.samples.helloworld
{
    class Program
    {
        //private static HaywireRequestCallback rootCallback;

        static void Main(string[] args)
        {
           
            string url = "http;//localhost:8080";

            HaywireWebApp app = new HaywireWebApp();
            using (var q = app.Start(url, builder => builder.xyz()))
            {
                Debug.WriteLine("hi");
            }     

            //HaywireConfiguration config;
            //config.ListenAddress = "0.0.0.0";

            //if (args.Length > 0)
            //{
            //    config.ListenPort = int.Parse(args[0]);
            //}
            //else
            //{
            //    config.ListenPort = 8000;
            //}

            //var server = new HaywireServer(config);
            //rootCallback = new HaywireRequestCallback(GetRoot);
            //server.AddRoute("/", rootCallback);
            //server.StartAcceptingRequests();
        }


       

        //private static void GetRoot(HaywireRequest request, IntPtr response, IntPtr state)
        //{
        //    var resp = new HaywireResponse(response);
        //    resp.SetCode(StatusCodes.HTTP_STATUS_200);
        //    resp.SetHeader("Content-Type", "text/html");
        //    resp.SetHeader("Connection", "Keep-Alive");
        //    resp.SetHeader("foo", "bar");
        //    resp.SetBody("hello world");
        //    resp.Send(ResponseComplete);
        //}

        //private static void ResponseComplete(IntPtr state)
        //{
            
        //}
    }
    public static class Startup
    {
        public static void xyz(this IAppBuilder app)
        {
            app.UseHandler((request, res) =>
            {
                res.ContentType = "text/plain";
                return res.WriteAsync("Hello, World!");
            });
        }
    }
}
