using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedKernel.Events
{
    public class PurchaseItem
    {
        public Guid ProductId { get; set; }
        public int Qty { get; set; }
    }
}
