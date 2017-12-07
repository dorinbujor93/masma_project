using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using jade.core;

namespace Project_MASMA 
{
    class MobileAgent : Agent
    {
        public override void setup()
        {
            Console.WriteLine("Agent {0} started...", getAID().getName());
            doMove(new ContainerID("container1", null));
        }

        public override void beforeMove()
        {
            Console.WriteLine("Agent {0} began moving...", getAID().getName());
        }

        public override void afterMove()
        {
            Console.WriteLine("Agent {0} finished moving...", getAID().getName());
        }
    }
}
