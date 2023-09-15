using Microsoft.Xrm.Sdk;
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
