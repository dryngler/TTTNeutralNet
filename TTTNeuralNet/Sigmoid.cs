using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTTNeuralNet
{
    class Sigmoid : IActivation
    {
        public double Function(double input)
        {
            float k = (float)Math.Exp(input);
            return k / (1.0f + k);
        }
        public double Derivative(double input)
        {
            return (Math.Exp(-input)) / (Math.Pow(1 + Math.Exp(-input), 2.0));
        }
    }
}
