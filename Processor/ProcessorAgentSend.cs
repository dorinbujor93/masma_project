using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using jade.core.behaviours;
using jade.lang.acl;

namespace Project_MASMA
{
    class ProcessorAgentSend : CyclicBehaviour
    {
        private ProcessorAgent procAgent;

        public ProcessorAgentSend(ProcessorAgent a)
            :base(a)
        {
            procAgent = a;
        }

        public override void action()
        {
            if (procAgent.needHelp == true)
            {
                ACLMessage messageToSend = new ACLMessage(ACLMessage.REQUEST);

                String content1 = String.Empty;
                String content2 = String.Empty;
                for (int index = 0; index < procAgent.matrix1ArrayRemained.Count; index++)
                {
                    content1 += procAgent.matrix1ArrayRemained[index] + " ";
                    content2 += procAgent.matrix2ArrayRemained[index] + " ";
                }
                content1 = content1 + "|" + content2;

                messageToSend.setContent(content1);
                messageToSend.addReceiver(Constants.dispAid);
                procAgent.send(messageToSend);

            }
            if(Constants.helpersNeeded.ContainsKey(procAgent.getName()) && Constants.helpersNeeded[procAgent.getName()] == procAgent.helpersResults.Count() 
                && String.IsNullOrEmpty(procAgent.processorResult) == false)
            {
                procAgent.JoinResults();

                ACLMessage messageToSend = new ACLMessage(ACLMessage.REQUEST);
                messageToSend.setContent(procAgent.messageForDistributor);
                messageToSend.addReceiver(Constants.distrAid);
                procAgent.send(messageToSend);
            }

        }
    }
}
