using System;
using System.Collections.Generic;
using System.Text;

namespace plethocoreProject.services
{
    interface ITaxservices
    {
        decimal TaxAmount(decimal TotalAmount);
    }
}
