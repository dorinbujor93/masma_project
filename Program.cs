using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using jade.core;
using jade.wrapper;

namespace Project_MASMA
{
    class Program
    {
        static void Main(string[] args)
        {
            Profile p = new ProfileImpl();
            String index = String.Empty;
            Runtime rt = Runtime.instance();

            jade.wrapper.AgentContainer distrCont = JadeHelper.CreateContainer("DistributorContainer", true, "localhost", "", "1153");
            AgentController distrAgent = JadeHelper.CreateAgent(distrCont, "DistributorAgent", "Project_MASMA.DistributorAgent", null);
            distrCont.start();
            distrAgent.start();
        }
    }
}
