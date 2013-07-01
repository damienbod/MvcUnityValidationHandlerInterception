using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace MvcUnityBootstrapperTest.UnityExtensions
{
    public class DiagnosisHandlerAttribute : HandlerAttribute
    {
        public override ICallHandler CreateHandler(IUnityContainer container)
        {
            return new DiagnosisCallHandler();
        }
    }
}