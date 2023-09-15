using Microsoft.Xrm.Sdk;
using System;

namespace APISolutionComponent
{
    /// <summary>
    /// Plugin development guide: https://docs.microsoft.com/powerapps/developer/common-data-service/plug-ins
    /// Best practices and guidance: https://docs.microsoft.com/powerapps/developer/common-data-service/best-practices/business-logic/
    /// </summary>
    public class SolutionComponentAPI : PluginBase
    {
        public SolutionComponentAPI(string unsecureConfiguration, string secureConfiguration)
            : base(typeof(SolutionComponentAPI))
        {
            // TODO: Implement your custom configuration handling
            // https://docs.microsoft.com/powerapps/developer/common-data-service/register-plug-in#set-configuration-data
        }

        // Entry point for custom business logic execution
        protected override void ExecuteDataversePlugin(ILocalPluginContext localPluginContext)
        {
            if (localPluginContext == null)
            {
                throw new ArgumentNullException(nameof(localPluginContext));
            }

            var tracingService = localPluginContext.TracingService;
            var organizationService = localPluginContext.InitiatingUserService;
            var context = localPluginContext.PluginExecutionContext;

            if (context.MessageName.Equals("hack_solutioncomponentapi") && context.Stage.Equals(30))
            {
                tracingService.Trace("Inside hack_solutioncomponentapi");
                var solutionId = context.InputParameters.Contains("hack_solutionid")
                                ? (string)context.InputParameters["hack_solutionid"]
                                : string.Empty;
                var componentType = context.InputParameters.Contains("hack_componenttype")
                                ? (string)context.InputParameters["hack_componenttype"]
                                : string.Empty;
            }
        }
    }
}
