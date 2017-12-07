using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using jade.core;
using jade.core.behaviours;
using jade.lang.acl;

namespace Project_MASMA
{
    class DispatcherAgentRecieve : CyclicBehaviour
    {
        private DispatcherAgent dispAgent;
        List<AID> incoming = new List<AID>();

        public DispatcherAgentRecieve(DispatcherAgent a)
            :base(a)
        {
            dispAgent = a;
        }

        public override void action()
        {
            ACLMessage message = dispAgent.receive();
            if (message != null && !incoming.Contains(message.getSender()))
            {
                incoming.Add(message.getSender());
                dispAgent.ProcessData(message.getContent(), message.getSender());
            }
            else
            {
                block();
            }

        }
    }
}
