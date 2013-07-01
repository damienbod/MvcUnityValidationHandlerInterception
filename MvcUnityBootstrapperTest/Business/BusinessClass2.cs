﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcUnityBootstrapperTest.Logging;
using MvcUnityBootstrapperTest.UnityExtensions;
using Microsoft.Practices.EnterpriseLibrary.Validation;

namespace MvcUnityBootstrapperTest.Business
{
    [UnityIoCPerRequestLifetimeAttribute]
    public class BusinessClass2 : IBusinessClass2
    {
        private readonly IUnitOfWorkExample _unitOfWorkExample;

        public BusinessClass2(IUnitOfWorkExample unitOfWorkExample)
        {
            _unitOfWorkExample = unitOfWorkExample;
            UnityEventLogger.Log.CreateUnityMessage("BusinessClass2");
        }

        private bool _disposed = false;

        public string Hello()
        {
            return "Hello from Business class2";
        }

        public string SayHello(string hello)
        {
            //throw new ArgumentNullException(hello);     
            _unitOfWorkExample.Deposit(1.5m);
            //_unitOfWorkExample.Deposit(0.0m);
            return "SayHello Business class2 " + hello;

        }

        public void Dispose()
        {
            _unitOfWorkExample.Dispose();
            UnityEventLogger.Log.DisposeUnityMessage("BusinessClass2");
            if (!_disposed)
            {
                _disposed = true;
            }
        }
    }
}