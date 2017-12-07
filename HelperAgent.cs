using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using jade.core;
using jade.wrapper;

namespace Project_MASMA
{
    class HelperAgent:Agent
    {

        public Dictionary<AID, string> messageForProc = new Dictionary<AID, string>();

        public override void setup()
        {
            //Console.WriteLine("Helper Agent with AID:{0} started...", getAID().getName());
            //Constants.aids.Add(getAID());
            addBehaviour(new HelperAgentRecieve(this));
            addBehaviour(new HelperAgentSend(this));
        }

        public void ProcessData(string message, AID processorAgentId)
        {
            var partOfMessage = message.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
            var result = Sum(partOfMessage[1], partOfMessage[2]);
            messageForProc[new AID(partOfMessage[0], true)] = result;
        }

        public string Sum(string matrix1, string matrix2)
        {
            var matrixElements1 = matrix1.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            var matrixElements2 = matrix2.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            string summedMatrix = String.Empty;

            for(int i = 0; i < matrixElements1.Count(); i++)
            {
                summedMatrix += " " + (Convert.ToInt32(matrixElements1[i]) + Convert.ToInt32(matrixElements2[i])).ToString();
            }

            return summedMatrix;
        }
    }
}
