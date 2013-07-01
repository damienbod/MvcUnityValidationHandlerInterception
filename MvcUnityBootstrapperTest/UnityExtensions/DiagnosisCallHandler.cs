using System;
using Microsoft.Practices.Unity.InterceptionExtension;
using MvcUnityBootstrapperTest.Logging;

namespace MvcUnityBootstrapperTest.UnityExtensions
{
    public class DiagnosisCallHandler : ICallHandler
    {
        public IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext)
        {
            DiagnosisEvents.Log.MethodEnter(String.Format("[{0}:{1}]", this.GetType().Name, "Invoke"));

            // BEFORE the target method execution
            DiagnosisEvents.Log.LogVerboseMessage(String.Format("{0} {1}", input.MethodBase.ToString(), input.Target.ToString()));

            // Yield to the next module in the pipeline
            var methodReturn = getNext().Invoke(input, getNext);

            // AFTER the target method execution
            if (methodReturn.Exception == null)
            {
                DiagnosisEvents.Log.MethodLeave(String.Format("Successfully finished {0} {1}", input.MethodBase.ToString(), input.Target.ToString()));
            }
            else
            {
                DiagnosisEvents.Log.MethodLeave(String.Format("Finished {0} with exception {1}: {2}", input.MethodBase.ToString(), methodReturn.Exception.GetType().Name, methodReturn.Exception.Message));
            }

            return methodReturn;
        }

        public int Order
        {
            get;
            set;
        }
    }
}