using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using jade.core;
using jade.wrapper;
using jade.lang.acl;

namespace Project_MASMA
{
    class DistributorAgent:Agent
    {
        jade.wrapper.AgentContainer dispCont;
        AgentController dispAgent;
        List<jade.wrapper.AgentContainer> procCont = new List<jade.wrapper.AgentContainer>();
        List<jade.wrapper.AgentController> procAgents = new List<AgentController>();
        public Dictionary<string, string> processorsResults = new Dictionary<string, string>();
        int[,] matrix1 = new int[Constants.MatrixSize, Constants.MatrixSize];
        int[,] matrix2 = new int[Constants.MatrixSize, Constants.MatrixSize];
        List<int> bounds = new List<int>();
        public string finalResult = String.Empty;

        Random rnd = new Random();

        public override void setup()
        {
            Constants.distrAid = this.getAID();
            for(int i = 0; i < Constants.MatrixSize; i++)
            {
                for(int j=0;j< Constants.MatrixSize; j++)
                {
                    matrix1[i, j] = rnd.Next(1, 4);
                    matrix2[i, j] = rnd.Next(1, 4);
                }
            }
            addBehaviour(new DistributorAgentReceive(this));

            Calculate();
        }

        public void Calculate()
        {
            GenerateProcessorAgents();
            SplitData();
        }

        public void GenerateProcessorAgents()
        {
            String index;

            dispCont = JadeHelper.CreateContainer("DispatcherContainer", false, "localhost", null, "1150");
            dispAgent = JadeHelper.CreateAgent(dispCont, "DispatcherAgent", "Project_MASMA.DispatcherAgent", null);

            for (int i = 0; i < Constants.ProcessorNumber; i++)
            {
                index = (i < 9) ? "0" + i : i.ToString();
                procCont.Add(JadeHelper.CreateContainer("container" + i, false, "localhost", null, "11" + index));
                procAgents.Add(JadeHelper.CreateAgent(procCont[i], "ProcessorAgent" + i, "Project_MASMA.ProcessorAgent", null));
            }
            dispAgent.start();
            for (int i = 0; i < Constants.ProcessorNumber; i++)
            {
                procCont[i].start();
                procAgents[i].start();
            }
        }

        public void SplitData()
        {
            int size = (int)Math.Sqrt(matrix1.Length);

            int mediumOperations = (size * size) / Constants.ProcessorNumber;
            int lowerBound = (int)(0.75 * mediumOperations);
            int higherBound = (int)(1.25 * mediumOperations);

            bounds.Add(rnd.Next(lowerBound, higherBound));

            for (int i = 1; i < Constants.ProcessorNumber;i++)
            {
                bounds.Add(bounds[i - 1] + rnd.Next(lowerBound, higherBound));
            }
            int sum = bounds[0];

            for(int i = 1; i < Constants.ProcessorNumber; i++)
            {
                sum += bounds[i] - bounds[i-1];
            }

            Send(0, bounds[0], Constants.aids[0]);

            for (int i = 1;i<Constants.ProcessorNumber; i++)
            {
                Send(bounds[i - 1], bounds[i], Constants.aids[i]);
            }
        }

        void Send(int startIndex, int lastIndex,AID agentId)
        {
            ACLMessage toSendFirstArray = new ACLMessage(ACLMessage.REQUEST);
            ACLMessage toSendSecondArray = new ACLMessage(ACLMessage.REQUEST);

            String content1 = "FirstSubset ";
            String content2 = "SecondSubset ";
            for (int index = startIndex; index < lastIndex; index++)
            {
                content1 += matrix1[(index / Constants.MatrixSize), index % Constants.MatrixSize] + " ";
                content2 += matrix2[(index / Constants.MatrixSize), index % Constants.MatrixSize] + " ";
            }

            toSendFirstArray.setContent(content1);
            toSendFirstArray.addReceiver(agentId);
            this.send(toSendFirstArray);

            toSendSecondArray.setContent(content2);
            toSendSecondArray.addReceiver(agentId);
            this.send(toSendSecondArray);
        }

        public void JoinFinalResults()
        {
            for (int i = 0; i < Constants.ProcessorNumber; i++)
            {
                foreach (var element in processorsResults)
                {
                    if (element.Key.Contains("ProcessorAgent" + i))
                    {
                        finalResult += element.Value + " ";
                    }
                }
            }
            Console.WriteLine(finalResult);
            var result = finalResult.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            Console.WriteLine(result.Count());
        }
    }
}
