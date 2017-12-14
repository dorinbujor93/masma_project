using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using jade.core;

namespace Project_MASMA
{
    public static class Constants
    {
        public const int ProcessorCapacity = 20;
        public const int HelperCapacity = 10;
        public const int ProcessorNumber = 10;
        public const int MatrixSize = 20;

        public static List<AID> aids = new List<AID>();
        public static Dictionary<string, int> helpersNeeded = new Dictionary<string, int>();
        public static AID dispAid;
        public static AID distrAid;
    }
}
