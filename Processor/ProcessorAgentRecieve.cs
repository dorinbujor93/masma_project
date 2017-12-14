using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using jade.core;
using jade.core.behaviours;
using jade.lang.acl;

namespace Project_MASMA
{
    class ProcessorAgentRecieve:CyclicBehaviour
    {
        private ProcessorAgent procAgent;
        List<string> incoming = new List<string>();

        public ProcessorAgentRecieve(ProcessorAgent a)
            :base(a)
        {
            procAgent = a;
        }

        public override void action()
        {
            ACLMessage message = procAgent.receive();
            if (message != null)
            {
                if (message.getSender().getName().Contains("DistributorAgent"))
                {
                    incoming.Add(message.getContent());
                    if (incoming.Count == 2)
                    {
                        if (incoming[0].StartsWith("FirstSubset") && incoming[1].StartsWith("SecondSubset"))
                        {
                            procAgent.ProcessRecvData(incoming[0], incoming[1]);
                        }
                        else if (incoming[0].StartsWith("SecondSubset") && incoming[1].StartsWith("FirstSubset"))
                        {
                            procAgent.ProcessRecvData(incoming[1], incoming[0]);
                        }
                    }
                    var result = message.getContent().Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                }
                else if(message.getSender().getName().Contains("HelperAgent"))
                {
                    procAgent.helpersResults[message.getSender().getName()] = message.getContent();
                }
            }
            else
            {
                block();
            }

        }
    }
}
