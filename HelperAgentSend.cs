using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using jade.core.behaviours;
using jade.lang.acl;

namespace Project_MASMA
{
    class HelperAgentSend : CyclicBehaviour
    {
        private HelperAgent helpAgent;

        public HelperAgentSend(HelperAgent a)
            :base(a)
        {
            helpAgent = a;
        }

        public override void action()
        {
            if ( helpAgent.messageForProc.Count() > 0)
            {
                ACLMessage messageToSend = new ACLMessage(ACLMessage.REQUEST);
                messageToSend.setContent(helpAgent.messageForProc.First().Value);
                messageToSend.addReceiver(helpAgent.messageForProc.First().Key);
                helpAgent.messageForProc.Remove(helpAgent.messageForProc.First().Key);
                helpAgent.send(messageToSend);
            }


        }
    }
}
