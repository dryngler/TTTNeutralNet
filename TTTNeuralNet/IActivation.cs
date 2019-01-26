using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTTNeuralNet
{
    interface IActivation
    {
        double Function(double input);
        double Derivative(double input);
    }
}
