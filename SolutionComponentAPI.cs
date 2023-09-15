using Microsoft.Xrm.Sdk;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ServiceModel;

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
            var componentApiResponse = "[]";
            try
            {
                if (context.MessageName.Equals("hack_componentapi") && context.Stage.Equals(30))
                {
                    tracingService.Trace("Inside hack_componentapi");
                    var solutionId = context.InputParameters.Contains("hack_solutionid")
                                    ? (string)context.InputParameters["hack_solutionid"]
                                    : string.Empty;
                    var componentType = context.InputParameters.Contains("hack_componenttype")
                                    ? (string)context.InputParameters["hack_componenttype"]
                                    : string.Empty;

                    using (var dvHelper = new DVHelper(organizationService, tracingService))
                    {
                        var collSolutionComponentResponse = dvHelper.ExecuteSolutionComponentQuery(new Guid(solutionId));
                        tracingService.Trace($"Count of collSolutionComponentResponse : {collSolutionComponentResponse.Count}");
                        var collComponentDefinitionResponse = dvHelper.GetSolutionComponentDefinitions(new Guid(solutionId));
                        tracingService.Trace($"Count of collComponentDefinitionResponse : {collComponentDefinitionResponse.Count}");

                        collSolutionComponentResponse.AddRange(collComponentDefinitionResponse);

                        componentApiResponse = JsonConvert.SerializeObject(collSolutionComponentResponse, Formatting.Indented);
                    }
                }
            }
            catch (FaultException<OrganizationServiceFault> fe)
            {
                tracingService.Trace("Fault Exception:");
                tracingService.Trace("Timestamp: {0}", fe.Detail.Timestamp);
                tracingService.Trace("Code: {0}", fe.Detail.ErrorCode);
                tracingService.Trace("Message: {0}", fe.Detail.Message);
                tracingService.Trace("Plugin Trace: {0}", fe.Detail.TraceText);
                tracingService.Trace("Inner Fault: {0}", null == fe.Detail.InnerFault ? "No Inner Fault" : "Has Inner Fault");
            }
            catch (TimeoutException te)
            {
                tracingService.Trace("Timeout Exception:");
                tracingService.Trace("Message: {0}", te.Message);
                tracingService.Trace("Stack Trace: {0}", te.StackTrace);
                tracingService.Trace("Inner Fault: {0}", null == te.InnerException.Message ? "No Inner Fault" : te.InnerException.Message);
            }
            catch (Exception ex)
            {
                tracingService.Trace($"Exception: {ex.Message}");
                // Display the details of the inner exception.
                if (ex.InnerException != null)
                {
                    tracingService.Trace(ex.InnerException.Message);

                    var fe = ex.InnerException as FaultException<OrganizationServiceFault>;
                    if (fe != null)
                    {
                        tracingService.Trace("Timestamp: {0}", fe.Detail.Timestamp);
                        tracingService.Trace("Code: {0}", fe.Detail.ErrorCode);
                        tracingService.Trace("Message: {0}", fe.Detail.Message);
                        tracingService.Trace("Plugin Trace: {0}", fe.Detail.TraceText);
                        tracingService.Trace("Inner Fault: {0}", null == fe.Detail.InnerFault ? "No Inner Fault" : "Has Inner Fault");
                    }
                }
            }
            finally
            {
                // Setting Output parameters
                context.OutputParameters["hack_componentapiresponse"] = componentApiResponse;
            }
        }
    }
}
