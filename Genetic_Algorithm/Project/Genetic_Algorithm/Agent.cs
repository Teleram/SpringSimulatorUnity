using System;
using System.Collections.Generic;
using System.IO;

public class Agent
{
    PopulationManager popMan;

    public Agent(PopulationManager popMan)
    {
        this.popMan = popMan;
    }

    //public void WriteToFile()
    //{
    //    string serializationFile = "Gen_Alg_intermediate_Result.bin";
    //    using (Stream stream = File.Open(serializationFile, FileMode.Create))
    //    {
    //        var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

    //        bformatter.Serialize(stream, popMan);
    //    }
    //}

    //public void ReadFromFile(string serializationFile)
    //{
    //    using (Stream stream = File.Open(serializationFile, FileMode.Open))
    //    {
    //        var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

    //        popMan = (PopulationManager)bformatter.Deserialize(stream);
    //    }
    //}

    public void WriteWeightsOfCurrentGeneration()
    {
        string file = "resultWeights";
        List<Individual> population = popMan.GetPopulation();
        int popSize = population.Count;
        for(int i = 0; i < popSize; i++)
        {
            string myFile = file + i + ".txt";
            StreamWriter writer = new StreamWriter(myFile);

            Individual ind = population[i];
            List<string> weigths = ind.GetWeightsAsStrings();
            foreach (string weight in weigths)
            {
                writer.WriteLine(weight);
            }
            writer.WriteLine(ind.GetFitness());
            writer.Close();
        }
    }
}