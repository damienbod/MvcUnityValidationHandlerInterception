using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcUnityBootstrapperTest.Logging;
using MvcUnityBootstrapperTest.UnityExtensions;
using Microsoft.Practices.EnterpriseLibrary.Validation.PolicyInjection;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;


namespace MvcUnityBootstrapperTest.Business
{
    [UnityIoCTransientLifetimeAttribute]
    public class UnitOfWorkExample : IUnitOfWorkExample
    {
        static int _counter = 0; 
        public UnitOfWorkExample()
        {
            _counter++;
            UnityEventLogger.Log.CreateUnityMessage("UnitOfWorkExampleTest " + _counter );
        }

        [ValidationCallHandler()]
        [DiagnosisHandlerAttribute()]
        public virtual void Deposit([RangeValidator(typeof(Decimal), "1.0", RangeBoundaryType.Exclusive, "2.0", RangeBoundaryType.Exclusive)] decimal depositAmount)
        {
            string x = "";
        }

        private bool _disposed = false;

        public void Dispose()
        {
            if (!_disposed)
            {
                _counter--;
                UnityEventLogger.Log.DisposeUnityMessage("UnitOfWorkExampleTest " + _counter);
                _disposed = true;
            }
        }

        public string HelloFromUnitOfWorkExample()
        {
            UnityEventLogger.Log.LogUnityMessage("Hello UnitOfWorkExampleTest " + _counter);
            return "HelloFromUnitOfWorkExample";
        }
    }
}