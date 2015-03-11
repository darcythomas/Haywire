using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace haywire
{
   public  class HaywireWebApp
    {

        

        public  IDisposable Start(String url, Action<IAppBuilder> startup)
        {
            Uri cleanUri;

            if (!Uri.TryCreate(url,UriKind.Absolute,out cleanUri))
            {
                throw new ArgumentException(String.Format("The url provided ({0}) is invalid."));
            }

            return Start(cleanUri, startup);
        }

        public static IDisposable Start(Uri url, Action<IAppBuilder> startup)
        {

            return new HaywireServerService(url, startup);
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
}
