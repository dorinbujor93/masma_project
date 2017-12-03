using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using jade.core;
using jade.wrapper;

namespace Lab4Example1
{
    class Program
    {
        static void Main(string[] args)
        {
            Profile p = new ProfileImpl();
            String index = String.Empty;
            Runtime rt = Runtime.instance();
            int[,] matrix = MatrixGenerator.GetMatrix();

            jade.wrapper.AgentContainer distrCont = JadeHelper.CreateContainer("DistributorContainer", true, "localhost", "", "1153");
            AgentController distrAgent = JadeHelper.CreateAgent(distrCont, "DistributorAgent", "Lab4Example1.DistributorAgent", null);
            distrCont.start();
            distrAgent.start();

            /*jade.wrapper.AgentContainer agentCont = JadeHelper.CreateContainer("ProcessorContainer", true, "localhost", "", "1152");
            AgentController agent = JadeHelper.CreateAgent(agentCont, "ProcessorAgent", "Lab4Example1.PrcessorAgent", null);
            agentCont.start();
            agent.start();
            Console.Write("");*/
            /*jade.wrapper.AgentContainer dispCont = JadeHelper.CreateContainer("DispatcherContainer", false, "localhost", "", "1150");
            AgentController dispAgent = JadeHelper.CreateAgent(dispCont, "DispatcherAgent", "Lab4Example1.MobileAgent", null);

            List<jade.wrapper.AgentContainer> procCont = new List<jade.wrapper.AgentContainer>();
            List<jade.wrapper.AgentController> procAgents = new List<AgentController>();
            for(int i = 0; i < Constants.ProcessorNumber;i++)
            {
                index = (i < 9)?"0"+i:i.ToString();
                procCont.Add(JadeHelper.CreateContainer("container" + i, false, "localhost", "", "10" + index));
                procAgents.Add(JadeHelper.CreateAgent(procCont[i], "ProcessorAgent" + i, "Lab4Example1.MobileAgent", null));
            }*/




            /*
            jade.wrapper.AgentContainer c1 = JadeHelper.CreateContainer("container1", false, "localhost", "", "1091");
            mc.start();
            c1.start();
            AgentController ag1 = JadeHelper.CreateAgent(mc, "Agent1", "Lab4Example1.MobileAgent", null);
            AgentController ag2 = JadeHelper.CreateAgent(mc, "Agent2", "Lab4Example1.CloningAgent", null);
            ag1.start();
            ag2.start();*/
        }
    }
}
