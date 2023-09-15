using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APISolutionComponent
{
    public class DVHelper : IDisposable
    {
        public IOrganizationService OrganizationService { get; set; }
        public ITracingService TracingService { get; set; }

        public DVHelper(IOrganizationService organizationService, ITracingService tracingService)
        {
            this.OrganizationService = organizationService;
            this.TracingService = tracingService;
        }

        /// <summary>
        /// Executes a query to retrieve solution components for a specified solution.
        /// </summary>
        /// <param name="solutionId">The unique identifier of the solution.</param>
        /// <returns>A list of ResponseComponent objects representing the solution components.</returns>
        public List<ResponseComponent> ExecuteSolutionComponentQuery(Guid solutionId)
        {
            var collComponents = new List<ResponseComponent>();
            var querySolutionComponent = new QueryExpression("solutioncomponent")
            {
                ColumnSet = new ColumnSet(true) // Retrieve all columns
            };

            var filter = new FilterExpression(LogicalOperator.And);
            // Add the condition for solutionid
            filter.AddCondition("solutionid", ConditionOperator.Equal, solutionId);
            querySolutionComponent.Criteria.AddFilter(filter);
            var componentDefinitionResponse = OrganizationService.RetrieveMultiple(querySolutionComponent);

            // Process the returned rows
            foreach (var entity in componentDefinitionResponse.Entities)
            {
                var component = new ResponseComponent();

                // Access entity attributes as needed
                var objectId = entity.GetAttributeValue<Guid>("objectid");
                var componentType = entity.GetAttributeValue<OptionSetValue>("componenttype");

                // Convert the integer to EntityObjectType enum
                var enumComponentType = (EntityObjectType)componentType.Value;
                component.ComponentName = enumComponentType.ToString();
                component.ComponentType = componentType.Value.ToString();

                if (componentType.Value == 1)
                {
                    var retrieveEntityRequest = new RetrieveEntityRequest
                    {
                        MetadataId = objectId,
                        EntityFilters = EntityFilters.Entity,
                        RetrieveAsIfPublished = true
                    };

                    // Send the request to the organization service
                    var response = (RetrieveEntityResponse)OrganizationService.Execute(retrieveEntityRequest);

                    // Get the entity metadata from the response
                    var entityMetadata = response.EntityMetadata;
                    component.EntityId = objectId.ToString();
                    component.EntityLogicalName = entityMetadata.LogicalName;
                    component.ComponentKeyAttributeName = entityMetadata.PrimaryIdAttribute;
                    component.ComponentNameAttributeName = entityMetadata.PrimaryNameAttribute;
                }
                else if (componentType.Value == 300) // Canvas App
                {
                    var canvasAppMetadata = OrganizationService.Retrieve("canvasapp", objectId, new ColumnSet("componentstate", "description", "displayname"));
                    component.ComponentStateAttributeName = canvasAppMetadata.Contains("componentstate") ? canvasAppMetadata.FormattedValues["componentstate"] : string.Empty;
                    component.ComponentName = canvasAppMetadata.Contains("displayname") ? canvasAppMetadata["displayname"].ToString() : string.Empty;
                    component.ComponentType = enumComponentType.ToString();
                }
                else if (componentType.Value == 80) // MDA
                {
                    var mdaAppMetadata = OrganizationService.Retrieve("appmodule", objectId, new ColumnSet("componentstate", "description", "name", "uniquename"));
                    component.ComponentStateAttributeName = mdaAppMetadata.Contains("componentstate") ? mdaAppMetadata.FormattedValues["componentstate"] : string.Empty;
                    component.ComponentName = mdaAppMetadata.Contains("name") ? mdaAppMetadata["name"].ToString() : string.Empty;
                    component.ComponentType = enumComponentType.ToString();
                }

                collComponents.Add(component);
            }

            return collComponents;
        }

        /// <summary>
        /// Retrieves solution component definitions for a specified solution.
        /// </summary>
        /// <param name="solutionId">The unique identifier of the solution.</param>
        /// <returns>A list of ResponseComponent objects representing the component definitions.</returns>
        public List<ResponseComponent> GetSolutionComponentDefinitions(Guid solutionId)
        {
            var collComponents = new List<ResponseComponent>();
            var queryComponentDefinition = new QueryExpression("solutioncomponentdefinition")
            {
                ColumnSet = new ColumnSet(true)
            };

            var filter = new FilterExpression(LogicalOperator.And);
            // Add the condition for solutionid
            filter.AddCondition("solutionid", ConditionOperator.Equal, solutionId);
            queryComponentDefinition.Criteria.AddFilter(filter);

            var componentDefinitionResponse = OrganizationService.RetrieveMultiple(queryComponentDefinition);

            // Process the returned rows
            foreach (var entity in componentDefinitionResponse.Entities)
            {
                var component = new ResponseComponent
                {
                    EntityLogicalName = entity.Contains("primaryentityname") ? entity["primaryentityname"].ToString() : string.Empty,
                    ComponentType = entity.Contains("objecttypecode") ? entity["objecttypecode"].ToString() : string.Empty
                };

                collComponents.Add(component);
            }

            return collComponents;
        }

        // Implement the Dispose method to release resources
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Dispose of managed resources
                if (OrganizationService != null)
                {
                    OrganizationService = null;
                }

                if (TracingService != null)
                {
                    TracingService = null;
                }
            }
        }
    }
}