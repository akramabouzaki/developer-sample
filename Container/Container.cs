using System;
using System.Collections.Generic;
using System.Reflection;

namespace DeveloperSample.Container
{
    public class Container
    {
        private Dictionary<Type, Type> bindingList = new Dictionary<Type, Type>();
        public void Bind(Type interfaceType, Type implementationType)
        {
            bool isValidParams = (interfaceType != null) &&
                                 (implementationType != null) &&
                                 interfaceType.IsInterface && 
                                 implementationType.IsClass;
            if (!isValidParams)
            {
                throw new Exception("Args invalid!");
            }
            
            bool isInterfaceImplemnted = implementationType.GetInterface(interfaceType.Name) == interfaceType;
            
            if (!isInterfaceImplemnted)
            {
                throw new Exception("Interface is not inherited by class!");
            }
            
            bindingList.Add(interfaceType, implementationType);
        }
        public T Get<T>()
        {
            Type t = typeof(T);
            if (bindingList.ContainsKey(t))
            {
                return (T)Activator.CreateInstance(bindingList[t]);
            }
            else if (t.IsClass)
            {
                return (T)Activator.CreateInstance(t);
            }
            else if (t.IsInterface)
            {
                throw new Exception("Interface can not be initialized without binding to class first!");
            }
            else
            {
                throw new Exception("Type T should be a class or an interface!");
            }
        }
    }
}
