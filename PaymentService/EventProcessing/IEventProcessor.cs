using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentService.EventProcessing
{
    public interface IEventProcessor
    {
        void ProcessEvent(string message);
    }
}