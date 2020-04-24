using System;
using System.Collections.Generic;
using System.Text;

namespace plethocoreProject.services
{
    public interface ITaxservices
    {
        decimal TaxAmount(decimal TotalAmount);
    }
}
