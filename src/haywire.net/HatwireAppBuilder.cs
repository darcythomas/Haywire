using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace haywire
{
    class HatwireIAppBuilder : IAppBuilder
    {
        public IDictionary<string, object> Properties
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public object Build(Type returnType)
        {
            throw new NotImplementedException();
        }

        public IAppBuilder New()
        {
            throw new NotImplementedException();
        }

        public IAppBuilder Use(object middleware, params object[] args)
        {
            throw new NotImplementedException();
        }
    }
}
