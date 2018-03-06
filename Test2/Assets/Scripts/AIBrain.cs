using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
using System.IO;


namespace NeuralNetwork
{
    public class AIBrain : NetworkBehaviour
    {
        private int numberOfInputs;

        private int[] layerSizes = new int[2];

        private List<float> weights;

        private NeuralNetwork myNN;

        void Start()
        {
            numberOfInputs = 8;
            layerSizes[0] = 10;
            layerSizes[1] = 6;

            readWeights();

            myNN = new NeuralNetwork(numberOfInputs, layerSizes);
            int weightIndex = 0;
            for (int i = 0; i < myNN.N_Layers; i++)
            {
                Layer presentLayer = myNN[i];
                for (int j = 0; j < presentLayer.N_Neurons; j++)
                {
                    Neuron presentNeuron = presentLayer[j];
                    for (int k = 0; k < presentNeuron.N_Inputs; k++)
                    {
                        float weight = weights[weightIndex];
                        weightIndex++;
                        presentNeuron[k] = weight;
                    }
                    presentNeuron.Threshold = 0;
                }
            }
        }

        private void readWeights()
        {
            weights = new List<float>();
            StreamReader reader;
            if (isServer)
            {
                reader = new StreamReader("Weights0.txt");
            }
            else
            {
                reader = new StreamReader("Weights1.txt");
            }

            List<string> weightsAsStrings = new List<string>();
            while(!reader.EndOfStream)
            {
                weightsAsStrings.Add(reader.ReadLine());
            }

            foreach(string weightString in weightsAsStrings)
            {
                float weight = float.Parse(weightString);
                weights.Add(weight);
            }
            reader.Close();
        }


        public DecisionObject getDecisionForTargetscript(Vector3 position, List<GameObject> friendsInRange, List<GameObject> enemiesInRange, int myMainUnitIndex, int enemyMainUnitIndex)
        {
            float[] inputs = CalculateInputFloats(position, friendsInRange, enemiesInRange, myMainUnitIndex, enemyMainUnitIndex);

            float[] outputs = myNN.Output(inputs);
            foreach (float output in outputs)
            {
                Debug.Log(output);
            }

            string functionName = "";
            GameObject target;
            bool iSeeTargets = (friendsInRange.Count > 0) || (enemiesInRange.Count > 0);

            // checks which function to use
            if ((outputs[0] >= 0.5f) || !iSeeTargets)
            {
                functionName = "setDestination";
                target = null;
            }
            else
            {
                functionName = "setTarget";

                bool friendlyTarget;
                bool targetMainUnit;
                // if 1st output >= 0.5, we will target a friendly unit
                if (((outputs[1] >= 0.5f) && (friendsInRange.Count > 0)) || (enemiesInRange.Count == 0))
                {
                    friendlyTarget = true;
                }
                else
                {
                    friendlyTarget = false;
                }

                // if 2nd output >= 0.5, we will target a main unit
                if((outputs[2] >= 0.5f) && ((myMainUnitIndex >= 0) || (enemyMainUnitIndex >= 0)))
                {
                    targetMainUnit = true;
                }
                else
                {
                    targetMainUnit = false;
                }

                if(friendlyTarget)
                {
                    if (targetMainUnit)
                    {
                        target = friendsInRange[myMainUnitIndex];
                    }
                    else
                    {
                        int targetIndex = ValueToIndex(friendsInRange.Count, outputs[3]);
                        target = friendsInRange[targetIndex];
                    }
                }
                else
                {
                    if (targetMainUnit)
                    {
                        target = (GameObject)enemiesInRange[enemyMainUnitIndex];
                    }
                    else
                    {
                        int targetIndex = ValueToIndex(enemiesInRange.Count, outputs[3]);
                        target = (GameObject)enemiesInRange[targetIndex];
                    }
                }
            }            

            Vector3 newPosition;
            newPosition.x = outputs[4] * 500;
            newPosition.y = 0;
            newPosition.z = outputs[5] * 500;


            return new DecisionObject(functionName, newPosition, target);
        }

        private float[] CalculateInputFloats(Vector3 position, List<GameObject> friendsInRange, List<GameObject> enemiesInRange, int myMainUnitIndex, int enemyMainUnitIndex)
        {
            float[] result = new float[8];

            result[0] = (position.x / 500.0f);
            result[1] = (position.z / 500.0f);
            
            if(myMainUnitIndex == -1)
            {
                result[2] = 0;
                result[3] = 0;
                result[4] = friendsInRange.Count / 10.0f;
            }
            else
            {
                result[2] = 1;
                result[3] = (myMainUnitIndex + 1.0f) * (1.0f / friendsInRange.Count);
                result[4] = (friendsInRange.Count - 1.0f) / 10.0f;
            }

            if (enemyMainUnitIndex == -1)
            {
                result[5] = 0;
                result[6] = 0;
                result[7] = enemiesInRange.Count / 10.0f;
            }
            else
            {
                result[5] = 1;
                result[6] = (myMainUnitIndex + 1.0f) * (1.0f / enemiesInRange.Count);
                result[7] = (enemiesInRange.Count - 1.0f) / 10.0f;
            }

            return result;
        }

        private int ValueToIndex(int numberOfTargets, float value)
        {
            float step = 1.0f / numberOfTargets;
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
