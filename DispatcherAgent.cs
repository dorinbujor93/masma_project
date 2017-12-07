using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using jade.core;
using jade.wrapper;

namespace Project_MASMA
{

    public struct Disp
    {
        public AID sender;
        public string message;
        Disp(AID sender, string message)
        {
            this.sender = sender;
            this.message = message;
        }
    }

    public class DispatcherAgent:Agent
    {
        int avaliableAgents = 0;
        public Dictionary<AID, string> messagesToSend = new Dictionary<AID, string>();
        List<jade.wrapper.AgentController> helperAgents = new List<jade.wrapper.AgentController>();
        List<jade.wrapper.AgentContainer> helperContainers = new List<jade.wrapper.AgentContainer>();
        public override void setup()
        {
            Constants.dispAid = getAID();
            addBehaviour(new DispatcherAgentRecieve(this));
            addBehaviour(new DispatcherAgentSend(this));
        }

        public void ProcessData(string message, AID processorAgentId)
        {
 

            var arrays = message.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
            var array1 = arrays[0].Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            var array2 = arrays[1].Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);

            var necessaryNrOfHelpers = array1.Count() / Constants.HelperCapacity;
            Constants.helpersNeeded[processorAgentId.getName()] = necessaryNrOfHelpers;
            necessaryNrOfHelpers = (array1.Count() % Constants.HelperCapacity != 0) ? necessaryNrOfHelpers + 1 : necessaryNrOfHelpers;

            GenerateHelperAgents(processorAgentId, necessaryNrOfHelpers);

            GenerateDataForHelperAgents(array1.ToList(), array2.ToList(), necessaryNrOfHelpers, processorAgentId);
        }

        public void GenerateHelperAgents(AID processorAgentId, int number)
        {
            String index;

            for (int i = 0; i < number; i++)
            {
                index = (i < 9) ? "0" + i : i.ToString();
                helperContainers.Add(JadeHelper.CreateContainer("container" + i, false, "localhost", null, "14" + index));
                helperAgents.Add(JadeHelper.CreateAgent(helperContainers[i], "HelperAgent" + i + processorAgentId.getLocalName(), "Project_MASMA.HelperAgent", null));
            }

            for (int i = 0; i < number; i++)
            {
                helperContainers[i].start();
                helperAgents[i].start();
            }
        }

        public void GenerateDataForHelperAgents(List<string> array1, List<string> array2, int nrNeccessaryNrOfHelp, AID processorAgentId)
        {
            string result = String.Empty;
            for(int i = 0; i < nrNeccessaryNrOfHelp; i++)
            {
                result = String.Empty;
                if (i < nrNeccessaryNrOfHelp - 1)
                {
                    var firstMatrix = array1.GetRange(i * Constants.HelperCapacity, Constants.HelperCapacity);
                    var secondMatrix = array2.GetRange(i * Constants.HelperCapacity, Constants.HelperCapacity);
                    messagesToSend[new AID(helperAgents[i].getName(), true)] = processorAgentId.getName() + "|" + GetMessage(firstMatrix,secondMatrix);

                }
                else
                {
                    var firstMatrix = array1.GetRange(i * Constants.HelperCapacity, array1.Count - i * Constants.HelperCapacity);
                    var secondMatrix = array2.GetRange(i * Constants.HelperCapacity, array2.Count - i * Constants.HelperCapacity);
                    messagesToSend[new AID(helperAgents[i].getName(), true)] = processorAgentId.getName() + "|"  + GetMessage(firstMatrix, secondMatrix);
                }
            }
        }

        private string GetMessage(List<string> array1, List<string> array2)
        {
            String firstMatrix = String.Empty;
            String secondMatrix = String.Empty;
            for (int i = 0; i < array1.Count; i++)
            {
                firstMatrix += array1[i] + " ";
                secondMatrix += array2[i] + " ";
            }

            return firstMatrix + "|" + secondMatrix;
        }
    }
}
