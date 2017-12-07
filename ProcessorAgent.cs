using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using jade.core;
using jade.wrapper;

namespace Project_MASMA
{
    class ProcessorAgent:Agent
    {
        public List<string> matrix1ArrayRemained = new List<string>();
        public List<string> matrix2ArrayRemained = new List<string>();
        public bool needHelp = false;
        public string processorResult = String.Empty;
        public string helpersSum = String.Empty;
        public Dictionary<string, string> helpersResults = new Dictionary<string, string>();
        public string messageForDistributor = String.Empty;

        public override void setup()
        {
            //Console.WriteLine("Processor agent with AID:{0} started...", getAID().getName());
            Constants.aids.Add(getAID());
            addBehaviour(new ProcessorAgentRecieve(this));
            addBehaviour(new ProcessorAgentSend(this));
        }

        public void ProcessRecvData(string matrix1, string matrix2)
        {
            var matrix1Array = matrix1.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries).ToList();
            matrix1Array.RemoveAt(0);
            var matrix2Array = matrix2.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries).ToList();
            matrix2Array.RemoveAt(0);

            var length = matrix1Array.Count;
            var length2 = matrix2Array.Count;

            
            if(length == length2 && length > Constants.ProcessorCapacity)
            {
                var localMatrix1 = matrix1Array.GetRange(0, Constants.ProcessorCapacity);
                var localMatrix2 = matrix2Array.GetRange(0, Constants.ProcessorCapacity);
                matrix1ArrayRemained = matrix1Array.GetRange(Constants.ProcessorCapacity, matrix1Array.Count - Constants.ProcessorCapacity);
                matrix2ArrayRemained = matrix2Array.GetRange(Constants.ProcessorCapacity, matrix2Array.Count - Constants.ProcessorCapacity);
                needHelp = true;

                CalculateSum(localMatrix1, localMatrix2);
            }
            else
            {
                CalculateSum(matrix1Array, matrix2Array);
            }
            //Console.WriteLine(length);
        }


        private void CalculateSum(List<string> matrix1, List<string> matrix2)
        {
            for(int i = 0; i<matrix1.Count;i++)
            {
                processorResult += " " + Sum(matrix1[i], matrix2[i]).ToString();
            }
        }

        private int Sum(string nr1, string nr2)
        {
            return Convert.ToInt32(nr1) + Convert.ToInt32(nr2);
        }
        //***********************************************************************************************

        public void JoinResults()
        {
            messageForDistributor = processorResult + " ";
            for (int i = 0; i < helpersResults.Count; i++)
            {
                foreach (var element in helpersResults)
                {
                    if(element.Key.Contains("HelperAgent" + i))
                    {
                        messageForDistributor += element.Value + " ";
                    }
                }
            }
        }
    }
}
