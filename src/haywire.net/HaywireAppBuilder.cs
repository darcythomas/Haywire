﻿using Owin;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace haywire
{
  public  class HaywireAppBuilder : IAppBuilder
    {

        private Dictionary<String, object> _properties = new Dictionary<string, object>() { { "owin.Version", "1.0" } };
        private List<Func<Func<IDictionary<string, object>, Task>, Func<IDictionary<string, object>, Task>>> pipeline = new List<Func<Func<IDictionary<string, object>, Task>, Func<IDictionary<string, object>, Task>>>();

        public IDictionary<string, object> Properties
        {
            get
            {
                return _properties;
            }
        }

        public object Build(Type returnType)
        {
          return  this.GetType();
        }

        public IAppBuilder New()
        {
            return new HaywireAppBuilder();
        }

        public IAppBuilder Use(object middleware, params object[] args )
        {
            //TODO work out what the whole params args thing is
            if (args != null) { args.ToList().ForEach(o => Debug.WriteLine(o)); };


            Func<Func<IDictionary<string, object>, Task>, Func<IDictionary<string, object>, Task>> typedMiddleWare = middleware as Func<Func<IDictionary<string, object>, Task>, Func<IDictionary<string, object>, Task>>;

            pipeline.Add(typedMiddleWare);

            return this;
        }

        public  void GetRoot(HaywireRequest request, IntPtr response, IntPtr state)
        {
            var resp = new HaywireResponse(response);

            var q = pipeline.First();

            Dictionary<string, object> owinrequest = new Dictionary<string, object>();

            owinrequest["owin.RequestBody"]  = new MemoryStream(Encoding.UTF8.GetBytes(request.body)); 

            owinrequest["owin.RequestHeaders"] = new Dictionary<string, string[]>();

            Dictionary<string, object> owinresponse = new Dictionary<string, object>();

            owinresponse["owin.ResponseBody"] = new MemoryStream();
            owinresponse["owin.ResponseHeaders"] = new Dictionary<string, string[]>();

             var qq =   q.Invoke(  DoneAsync);

            qq.Invoke(owinresponse).Wait();

            Stream bodyStream = owinresponse["owin.ResponseBody"] as Stream;
            bodyStream.Position = 0;

            resp.SetCode(StatusCodes.HTTP_STATUS_200);
            resp.SetHeader("Content-Type", "text/html");
            resp.SetHeader("Connection", "Keep-Alive");
            resp.SetHeader("foo", "bar");
            resp.SetBody(new StreamReader(bodyStream ).ReadToEnd());
            resp.Send(ResponseComplete);
        }
        private   void ResponseComplete(IntPtr state)
        {
            Debug.WriteLine("Response Complete callback. pointer: {0}", state);
        }

        private async Task DoneAsync(IDictionary<string, object> owinrequest)
        {
            Debug.WriteLine("done?");
         //  return Task.Delay(0);
        }

    }
}
