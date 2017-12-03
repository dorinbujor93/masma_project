using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using jade.core;

namespace Lab4Example1
{
    public static class Constants
    {
        public const int ProcessorCapacity = 10000;
        public const int HelperCapacity = 5000;
        public const int ProcessorNumber = 50;
        public const int MatrixSize = 100;

        public static List<AID> aids = new List<AID>();
        public static Dictionary<string, int> helpersNeeded = new Dictionary<string, int>();
        public static AID dispAid;
        public static AID distrAid;
    }
}
