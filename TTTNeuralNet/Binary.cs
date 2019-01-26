using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTTNeuralNet
{
    class Binary : IActivation
    {
        public double Derivative(double input)
        {
            return 0; //what to return if its not 0; its undefined 
        }
        public double Function(double input)
        {
            return input > 1 ? 1 : 0;
        }
    }
}
