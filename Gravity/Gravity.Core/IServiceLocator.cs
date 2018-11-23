﻿namespace Gravity.Core
{
    public interface IServiceLocator
    {
        T GetInstance<T>();
        void Release(object obj);
    }
}
