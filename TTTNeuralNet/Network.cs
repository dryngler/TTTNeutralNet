using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTTNeuralNet
{
    class Network
    {
        public Layer[] Layers;

        [JsonIgnore]
        public double[] Output;

        [JsonProperty]
        private Activations actType;
        [JsonProperty]
        private int inputCount;
        [JsonProperty]
        private int[] layerNeurons;

        [JsonConstructor]
        public Network(Activations actType, int inputCount, params int[] layerNeurons)
        {
            this.actType = actType;
            this.inputCount = inputCount;
            this.layerNeurons = layerNeurons;


            IActivation act;
            switch (actType)
            {
                case Activations.BinaryStep:
                    act = new Binary();
                    break;
                default:
                case Activations.Sigmoid:
                    act = new Sigmoid();
                    break;
            }


            Layers = new Layer[layerNeurons.Length];
            Output = new double[layerNeurons[layerNeurons.Length - 1]];
            Layers[0] = new Layer(act, inputCount, layerNeurons[0]);

            for (int i = 1; i < Layers.Length; i++)
            {
                Layers[i] = new Layer(act, Layers[i - 1].Neurons.Length, layerNeurons[i]);
            }
        }

        public void Randomize(Random rand)
        {
            for (int i = 0; i < Layers.Length; i++)
            {
                Layers[i].Randomize(rand);
            }
        }

        public double[] Compute(double[] inputs)
        {
            Output = inputs;
            for (int i = 0; i < Layers.Length; i++)
            {
                Output = Layers[i].Compute(Output);
            }

            return Output;
        }

        public void Mutate(Random rand, double rate)
        {
            foreach (Layer layer in Layers)
            {
                foreach (Neuron neuron in layer.Neurons)
                {
                    if (rand.NextDouble() < rate)
                    {
                        int mutateType = rand.Next(3);
                        switch(mutateType)
                        {
                            case 0:
                                neuron.Bias *= ChangeSign(rand);
                                break;
                            case 1:
                                neuron.Bias *= rand.NextDouble() + .5;
                                break;
                            case 2:
                                neuron.Bias += rand.NextDouble() * ChangeSign(rand);
                                break;
                        }
                    }

                    for (int i = 0; i < neuron.Weights.Length; i++)
                    {
                        if (rand.NextDouble() < rate)
                        {
                            int mutateType = rand.Next(3);
                            switch (mutateType)
                            {
                                case 0:
                                    neuron.Weights[i] *= ChangeSign(rand);
                                    break;
                                case 1:
                                    neuron.Weights[i] *= rand.NextDouble() + .5;
                                    break;
                                case 2:
                                    neuron.Weights[i] += rand.NextDouble() * ChangeSign(rand);
                                    break;
                            }
                        }
                    }
                }
            }
        }
        public void Crossover(Random rand, Network other)
        {
            for (int i = 0; i < Layers.Length; i++)
            {
                int cutPoint = rand.Next(Layers[i].Neurons.Length);
                int flip = rand.Next(2);
                for (int j = flip == 0 ? 0 : cutPoint; j < (flip == 0 ? cutPoint : Layers[i].Neurons.Length); j++)
                {
                    var neuron = Layers[i].Neurons[j];
                    var otherNeuron = other.Layers[i].Neurons[j];

                    otherNeuron.Weights.CopyTo(neuron.Weights, 0);
                    neuron.Bias = otherNeuron.Bias;
                }
            }
        }
        public int ChangeSign(Random rand)
        {
            return rand.Next(2) == 0 ? 1 : -1;
        }
    }
}
