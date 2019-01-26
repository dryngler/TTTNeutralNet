using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTTNeuralNet
{
    class Neuron
    {
        public double Bias;
        public double[] Weights;

        [JsonIgnore]
        public double[] Inputs { get; private set; }
        [JsonIgnore]
        public double Output { get; private set; }
        [JsonIgnore]
        public IActivation Activation;

        public Neuron(IActivation act, int inputNum)
        {
            Weights = new double[inputNum];
            Inputs = new double[inputNum];
            Activation = act;
        }

        //Randomize
        public void Randomize(Random rand)
        {
            for (int i = 0; i < Weights.Length; i++)
            {
                Weights[i] = rand.NextDouble(-0.5, 0.5);
            }
            Bias = rand.NextDouble(-0.5, 0.5);
        }
        //Compute
        public double Compute(double[] inputs)
        {
            if (Weights.Length != inputs.Length)
            {
                throw new ArgumentException("array size mismatch", "inputs");
            }

            inputs.CopyTo(Inputs, 0);

            double output = 0;
            for (int i = 0; i < Weights.Length; i++)
            {
                output += inputs[i] * Weights[i];
            }

            Output = Activation.Function(output + Bias);

            return Output;

        }
    }
}
