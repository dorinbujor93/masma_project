using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using jade.core;
using jade.core.behaviours;
using jade.lang.acl;


namespace Lab4Example1
{
    class HelperAgentRecieve : CyclicBehaviour
    {
        private HelperAgent helperAgent;
        List<AID> incoming = new List<AID>();

        public HelperAgentRecieve(HelperAgent a)
            :base(a)
        {
            helperAgent = a;
        }

        public override void action()
        {
            ACLMessage message = helperAgent.receive();
            if (message != null )
            {
                incoming.Add(message.getSender());
                //Console.WriteLine("*****************************" + message.getSender() + message.getContent());
                helperAgent.ProcessData(message.getContent(), message.getSender());
            }
            else
            {
                block();
            }

        }
    }
}
