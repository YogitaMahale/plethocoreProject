using System;
using System.Collections.Generic;
using System.Text;

namespace plethocoreProject.services
{
   public  interface INationalInsuranceContributionService
    {
        decimal NIContribution(decimal totalAmount);
    }
}
