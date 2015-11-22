using System;
using System.Collections.Generic;

namespace Funq
{
    public class Container
    {
        private Dictionary<ServiceKey, object> _factories = new Dictionary<ServiceKey, object>();

        public void Register<TService>(Func<Container, TService> factory)
        {
            RegisterImpl<TService,Func<Container, TService>>(factory);
        }

        public void Register<TService, TArg>(Func<Container, TArg, TService> factory)
        {
            RegisterImpl <TService,Func<Container, TArg, TService>>(factory);
        }

        public void Register<TService, TArg1, TArg2>(Func<Container, TArg1, TArg2, TService> factory)
        {
            RegisterImpl<TService, Func<Container, TArg1, TArg2, TService>>(factory);
        }

        private void RegisterImpl<TService, TFunc>(TFunc factory)
        {
            var key = new ServiceKey(typeof(TService), typeof(TFunc));
            _factories.Add(key, factory);
        }

        public TService Resolve<TService>()
        {
            return ResolveImpl<TService, Func<Container, TService>>(f => f(this));
        }

        public TService Resolve<TService, TArg>(TArg arg)
        {
            return ResolveImpl<TService, Func<Container, TArg, TService>>(f => f(this, arg));
        }

        public TService Resolve<TService, TArg1, TArg2>(TArg1 arg1, TArg2 arg2)
        {
            return ResolveImpl<TService, Func<Container, TArg1, TArg2, TService>>(f => f(this, arg1, arg2));
        }

        private TService ResolveImpl<TService, TFunc>(Func<TFunc, TService> invoker)
        {
            var key = new ServiceKey(typeof(TService), typeof(TFunc));
            TFunc factory =  (TFunc)_factories[key];
            var instance = invoker(factory);
            return instance;
        }
    }
}