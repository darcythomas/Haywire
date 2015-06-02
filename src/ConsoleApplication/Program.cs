using haywire;
using Owin;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            string url = "http://localhost:8080";

            HaywireWebApp app = new HaywireWebApp();
           // using (var q = app.Start(url, builder => builder.xyz()))
            using (var q = app.Start(url, builder => builder.UseFileServer(enableDirectoryBrowsing: true)     ))
            {
                Debug.WriteLine("hi");
                Console.ReadKey();
            }
        }

       
    }
    public static class Startupx
    {
        public static void xyz(this IAppBuilder app)
        {
            app.UseHandler((request, res) =>
            {
                res.ContentType = "text/plain";
                return res.WriteAsync("Hello, World!!!!");
            });
        }
    }
}
