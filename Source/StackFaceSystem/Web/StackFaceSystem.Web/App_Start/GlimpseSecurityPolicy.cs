namespace StackFaceSystem.Web
{
    using Glimpse.AspNet.Extensions;
    using Glimpse.Core.Extensibility;

    public class GlimpseSecurityPolicy : IRuntimePolicy
    {
        public RuntimeEvent ExecuteOn => RuntimeEvent.EndRequest | RuntimeEvent.ExecuteResource;

        public RuntimePolicy Execute(IRuntimePolicyContext policyContext)
        {
            var httpContext = policyContext.GetHttpContext();
            if (!httpContext.User.IsInRole("Administrator"))
            {
                return RuntimePolicy.Off;
            }

            return RuntimePolicy.On;
        }
    }
}
