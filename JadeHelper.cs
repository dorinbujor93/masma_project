using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using jade.core;
using jade.wrapper;

namespace Lab4Example1
{
    class JadeHelper
    {
        public static jade.wrapper.AgentContainer CreateContainer(string containerName, bool isMainContainer, string hostAddress, string hostPort, string localPort)
        {
            ProfileImpl p = new ProfileImpl();

            if (containerName != String.Empty)
                p.setParameter(Profile.CONTAINER_NAME, containerName);

            p.setParameter(Profile.MAIN, isMainContainer.ToString());

            if (localPort != null)
                p.setParameter(Profile.LOCAL_PORT, localPort);

            if (hostAddress != String.Empty)
                p.setParameter(Profile.MAIN_HOST, hostAddress);

            if (hostPort != String.Empty)
                p.setParameter(Profile.MAIN_PORT, hostPort);

            if (isMainContainer == true)
            {
                return Runtime.instance().createMainContainer(p);
            }
            else
            {
                return Runtime.instance().createAgentContainer(p);
            }
        }

        public static jade.wrapper.AgentController CreateAgent(jade.wrapper.AgentContainer container, string agentName, string agentClass, object[] args)
        {
            return container.createNewAgent(agentName, agentClass, args);
        }
    }
}
