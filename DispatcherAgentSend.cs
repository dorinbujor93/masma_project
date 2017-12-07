using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using jade.core.behaviours;
using jade.lang.acl;

namespace Project_MASMA
{
    class DispatcherAgentSend:CyclicBehaviour
    {
        private DispatcherAgent dispAgent;

        public DispatcherAgentSend(DispatcherAgent a)
            :base(a)
        {
            dispAgent = a;
        }

        public override void action()
        {
            if (dispAgent.messagesToSend.Count > 0)
            {
                ACLMessage messageToSend = new ACLMessage(ACLMessage.REQUEST);

                messageToSend.setContent(dispAgent.messagesToSend.First().Value);
                messageToSend.addReceiver(dispAgent.messagesToSend.First().Key);
                dispAgent.messagesToSend.Remove(dispAgent.messagesToSend.First().Key);
                dispAgent.send(messageToSend);
            }


        }
    }
}
