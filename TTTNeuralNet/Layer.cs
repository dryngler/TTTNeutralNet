using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTTNeuralNet
{
    class Layer
    {
        public Neuron[] Neurons;

        [JsonIgnore]
        public double[] Output;

        public Layer(IActivation act, int inputNum, int neuronCount)
        {
            Neurons = new Neuron[neuronCount];
            Output = new double[neuronCount];
            for (int i = 0; i < Neurons.Length; i++)
            {
                Neurons[i] = new Neuron(act, inputNum);
            }
        }

        //public Layer()
        public void Randomize(Random rand)
        {
            //randomize each neuron
            for (int i = 0; i < Neurons.Length; i++)
            {
                Neurons[i].Randomize(rand);
            }
        }
        public double[] Compute(double[] inputs)
        {
            for (int i = 0; i < Neurons.Length; i++)
            {
                Output[i] = Neurons[i].Compute(inputs);
            }
            return Output;
        }
    }
}
