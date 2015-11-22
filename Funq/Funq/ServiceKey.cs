using System;

namespace Funq
{
    internal class ServiceKey
    {
        public ServiceKey(Type serviceType, Type factoryType)
        {
            ServiceType = serviceType;
            FactoryType = factoryType;
        }

        public Type ServiceType { get; private set; }
        public Type FactoryType { get; private set; }

        public override bool Equals(object obj)
        {
            return ServiceKey.Equals(this, obj as ServiceKey);
        }

        public static bool Equals(ServiceKey obj1, ServiceKey obj2)
        {
            if (obj2 == null || obj1.GetType() != obj2.GetType())
            {
                return false;
            }

            return obj1.ServiceType == obj2.ServiceType && obj1.FactoryType == obj2.FactoryType;
        }

        public override int GetHashCode()
        {
            int hash = ServiceType.GetHashCode();
            hash ^= FactoryType.GetHashCode();
            return hash;
        }
    }
}