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

        public List<ResponseComponent> ExecuteSolutionComponentQuery(Guid solutionId)
        {
            var collComponents = new List<ResponseComponent>();
            var query = new QueryExpression("solutioncomponent")
            {
                ColumnSet = new ColumnSet(true) // Retrieve all columns
            };

            var filter = new FilterExpression(LogicalOperator.And);
            // Add the condition for solutionid
            filter.AddCondition("solutionid", ConditionOperator.Equal, solutionId);
            query.Criteria.AddFilter(filter);
            var results = OrganizationService.RetrieveMultiple(query);

            // Process the returned rows
            foreach (var entity in results.Entities)
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

                collComponents.Add(component);
            }

            return collComponents;
        }

        public List<ResponseComponent> GetSolutionComponentDefinitions(Guid solutionId)
        {
            var collComponents = new List<ResponseComponent>();
            // Create the QueryExpression
            var query = new QueryExpression("solutioncomponentdefinition")
            {
                // Set the column set to retrieve all columns
                ColumnSet = new ColumnSet(true)
            };

            var filter = new FilterExpression(LogicalOperator.And);
            // Add the condition for solutionid
            filter.AddCondition("solutionid", ConditionOperator.Equal, solutionId);
            query.Criteria.AddFilter(filter);

            var results = OrganizationService.RetrieveMultiple(query);

            // Process the returned rows
            foreach (var entity in results.Entities)
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
