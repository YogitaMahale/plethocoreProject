using System;
using System.Collections.Generic;
using System.Text;

namespace plethocoreProject.services
{
    interface INationalInsuranceContributionService
    {
        decimal NIContribution(decimal totalAmount);
    }
}
