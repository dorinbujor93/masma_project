using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using jade.core;
using jade.wrapper;

namespace Lab4Example1
{
    class CloningAgent : Agent
    {
        public override void setup()
        {
            //Console.WriteLine("Agent {0} started...", getAID().getName());
            Thread.Sleep(1000);
            doClone(new ContainerID("container1", null), "Agent_Clone");
        }

        public override void beforeClone()
        {
            //Console.WriteLine("Cloning agent {0}...", getAID().getName());
        }

        public override void afterClone()
        {
            //Console.WriteLine("Clone {0} ready...", getAID().getName());
        }

    }
}
