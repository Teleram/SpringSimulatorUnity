using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NeuralNetwork;


namespace NeuralNetwork
{
    public class AIBrain : MonoBehaviour
    {
        private int inputs;

        private int[] layerSizes = new int[2];

        private float[] weights;

        private NeuralNetwork myNN;

        void Start()
        {
            layerSizes[0] = 10;
            layerSizes[1] = 4;

            myNN = new NeuralNetwork(inputs, layerSizes);
            for (int i = 0; i < myNN.N_Layers; i++)
            {
                Layer presentLayer = myNN[i];
                for (int j = 0; j < presentLayer.N_Neurons; j++)
                {
                    Neuron presentNeuron = presentLayer[j];
                    for (int k = 0; k < presentNeuron.N_Inputs; k++)
                    {
                        // TODO get the weight from the weight-array
                        float weight = 1.0f;
                        presentNeuron[k] = weight;
                    }
                    presentNeuron.Threshold = 0;
                }
            }
        }

        public DecisionObject getDecisionForTargetscript(Vector3 position, GameObject[] targetsInRange)
        {
            float[] inputs = CalculateFloats(position, targetsInRange);

            float[] outputs = myNN.Output(inputs);

            int indexOfTarget;
            int numberOfTargets = targetsInRange.Length;
            if (numberOfTargets == 0)
            {
                indexOfTarget = -1;
            }
            else
            {
                indexOfTarget = ValueToIndex(numberOfTargets, outputs[3]);
            }

            string functionName = "";
            GameObject target;
            if ((indexOfTarget < 0) || (outputs[0] >= 0))
            {
                functionName = "setDestination";
                target = null;
            }
            else
            {
                functionName = "setTarget";
                target = targetsInRange[indexOfTarget];
            }

            Vector3 newPosition;
            newPosition.x = (outputs[1] + 1) * 250;
            newPosition.y = 0;
            newPosition.z = (outputs[2] + 1) * 250;


            return new DecisionObject(functionName, newPosition, target);

        }

        private float[] CalculateFloats(Vector3 position, GameObject[] targetsInRange)
        {
            float[] result = new float[8];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = 1.0f;
            }
            return result;
        }

        private int ValueToIndex(int numberOfTargets, float value)
        {
            value += 1.0f;
            float step = 2.0f / numberOfTargets;
            for (int i = (numberOfTargets - 1); i >= 0; i--)
            {
                if (value >= i * step)
                {
                    return i;
                }
            }
            return -1;
        }
    }
}
