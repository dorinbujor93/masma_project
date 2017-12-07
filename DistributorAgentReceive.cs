using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using jade.core;
using jade.core.behaviours;
using jade.lang.acl;

namespace Project_MASMA
{
    class DistributorAgentReceive : CyclicBehaviour
    {
        private DistributorAgent distrAgent;
        List<string> incoming = new List<string>();

        public DistributorAgentReceive(DistributorAgent a)
            : base(a)
        {
            distrAgent = a;
        }

        public override void action()
        {
            ACLMessage message = distrAgent.receive();
            if (message != null)
            {
               if (message.getSender().getName().Contains("ProcessorAgent"))
                {
                    distrAgent.processorsResults[message.getSender().getName()] = message.getContent();
                    distrAgent.JoinFinalResults();
                }
            }
            else
            {
                block();
            }

        }
    }
}
