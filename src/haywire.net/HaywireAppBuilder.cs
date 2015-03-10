using Owin;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace haywire
{
    class HaywireAppBuilder : IAppBuilder
    {

        private Dictionary<String, object> _properties = new Dictionary<string, object>() { { "owin.Version", "1.0" } };
        private List<Func<IDictionary<string, object>, Task>> pipeline = new List<Func<IDictionary<string, object>, Task>>();

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


            Func<IDictionary<string, object>, Task> typedMiddleWare = middleware as Func<IDictionary<string, object>, Task>;

            pipeline.Add(typedMiddleWare);

            return this;
        }

        public  void GetRoot(HaywireRequest request, IntPtr response, IntPtr state)
        {
            var resp = new HaywireResponse(response);
            resp.SetCode(StatusCodes.HTTP_STATUS_200);
            resp.SetHeader("Content-Type", "text/html");
            resp.SetHeader("Connection", "Keep-Alive");
            resp.SetHeader("foo", "bar");
            resp.SetBody("hello world");
            resp.Send(ResponseComplete);
        }
        private   void ResponseComplete(IntPtr state)
        {
            Debug.WriteLine("Response Complete callback. pointer: {0}", state);
        }

    }
}
