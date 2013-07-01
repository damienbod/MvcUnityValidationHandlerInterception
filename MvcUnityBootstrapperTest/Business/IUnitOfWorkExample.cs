using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.EnterpriseLibrary.Validation.PolicyInjection;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;


namespace MvcUnityBootstrapperTest.Business
{
    public interface IUnitOfWorkExample : IDisposable
    {
        string HelloFromUnitOfWorkExample();

        void Deposit(decimal depositAmount);
    }
}
