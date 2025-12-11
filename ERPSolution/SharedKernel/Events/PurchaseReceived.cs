using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedKernel.Events
{
    public class PurchaseReceived
    {
        public Guid PurchaseId { get; set; }
        public List<PurchaseItem> Items { get; set; }
    }
}
