using System;
using System.Collections.Generic;
using System.IO;


public class PopulationManager
{
    private List<Individual> population;

    private int[,] intermediateFitnessResults;

    private readonly int chromosomeSize;

    private readonly CompareByFitness comparer;

    private readonly int popSize;

    // size of the newly generated individuals per generation
    private readonly int generationSize;

    private readonly double mutationRate;

    // the offset is used because we want to have only positive values for fitness
    private readonly int offsetForFitness;

    private int generation;

    private int ind0;
    private int ind1;

    public PopulationManager(int popSize, int generationSize, int chromosomeSize, double mutationRate)
    {
        this.popSize = popSize;
        this.generationSize = generationSize;
        this.chromosomeSize = chromosomeSize;
        this.mutationRate = mutationRate;

        generation = 0;
        population = new List<Individual>();
        ind0 = 0;
        ind1 = 1;

        offsetForFitness = 13;

        int maxPop = popSize + generationSize;
        intermediateFitnessResults = new int[maxPop, maxPop];
        for (int i = 0; i < maxPop; i++)
        {
            for (int j = 0; j < maxPop; j++)
            {
                intermediateFitnessResults[i, j] = 0;
            }
        }
        GenerateInitialPopulation();
        comparer = new CompareByFitness();
    }

    private void GenerateInitialPopulation()
    {
        Random random = new Random();
        for (int i = 0; i < popSize; i++)
        {
            Individual ind = new Individual(chromosomeSize, random);
            population.Add(ind);
        }
    }

    //public void IterateGeneration()
    //{
    //    GenerateNewGeneration();
    //    EvaluateFitness();
    //    Survival();
    //    generation++;
    //}

    public void NextMatch(int strength0, int strength1)
    {
        int fit0 = (offsetForFitness + strength0) - strength1;
        int fit1 = (offsetForFitness + strength1) - strength0;

        intermediateFitnessResults[ind0, ind1] = fit0;
        intermediateFitnessResults[ind1, ind0] = fit1;

        ind1++;
        if(ind1 == population.Count)
        {
            ind0++;
            if(ind0 == population.Count)
            {
                EvaluateFitness();
                Survival();
                WriteWeightsOfCurrentGeneration();
                generation++;
                GenerateNewGeneration();
                ind0 = 0;
            }
            ind1 = ind0 + 1;
        }
    }

    private void WriteWeightsOfCurrentGeneration()
    {
        string file = "resultWeights_Gen" + generation + "_ind";
        int popSize = population.Count;
        for (int i = 0; i < popSize; i++)
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

    private void Survival()
    {
        SortPopulation();
        population = population.GetRange(0, popSize);
    }

    private void GenerateNewGeneration()
    {
        Dictionary<Individual, double> indToProbability = FitnessToProbability(population);

        for (int i = 1; i <= generationSize; i++)
        {
            Random randomDouble = new Random();
            double probParent1 = randomDouble.NextDouble();
            double probParent2 = randomDouble.NextDouble();

            Individual parent1 = GetIndividualFormDictionary(probParent1, indToProbability);
            Individual parent2 = GetIndividualFormDictionary(probParent2, indToProbability);
            while (parent1 == parent2)
            {
                probParent2 = randomDouble.NextDouble();
                parent2 = GetIndividualFormDictionary(probParent2, indToProbability);
            }

            int cut = new Random().Next(1, chromosomeSize);
            List<Gene> partOfParent1 = parent1.GetGenes().GetRange(0, cut);
            List<Gene> partOfParent2 = parent2.GetGenes().GetRange(cut, (chromosomeSize - cut));
            List<Gene> child = partOfParent1;
            child.AddRange(partOfParent2);

            double mutation = new Random().NextDouble();
            if (mutation <= mutationRate)
            {
                int mutatedGene = new Random().Next(chromosomeSize);
                child[mutatedGene] = new Gene();
            }
            Chromosome newChromosome = new Chromosome(child);
            Individual newChild = new Individual(newChromosome);
            population.Add(newChild);
        }
    }

    private Dictionary<Individual, double> FitnessToProbability(List<Individual> inds)
    {
        double sumFit = 0.0;
        foreach (Individual ind in population)
        {
            sumFit += ind.GetFitness();
        }

        Dictionary<Individual, double> indToProbability = new Dictionary<Individual, double>();
        foreach (Individual ind in population)
        {
            double probability = ind.GetFitness() / sumFit;
            indToProbability.Add(ind, probability);
        }

        return indToProbability;
    }

    private Individual GetIndividualFormDictionary(double prob, Dictionary<Individual, double> dict)
    {
        double sum = 0.0;
        foreach (KeyValuePair<Individual, double> pair in dict)
        {
            sum += pair.Value;
            if (prob <= sum)
            {
                return pair.Key;
            }
        }
        return null;
    }

    private void EvaluateFitness()
    {
        //for (int i = 0; i < population.Count; i++)
        //{
        //    for (int j = (i + 1); j < population.Count; j++)
        //    {
        //        WriteWeights(population[i], population[j]);
        //        MatchStarter.StartMatch();
        //        ReadIntermediateFitness(i, j);
        //    }
        //}

        for (int i = 0; i < population.Count; i++)
        {
            int fitness = 0;
            for (int j = 0; j < population.Count; j++)
            {
                fitness += intermediateFitnessResults[i, j];
            }
            population[i].SetFitness(fitness);
        }
    }

    private void SortPopulation()
    {
        population.Sort(comparer);
        population.Reverse();
    }

    public List<Individual> GetPopulation()
    {
        return population;
    }

    public int GetGeneration()
    {
        return generation;
    }

    public List<float> GetWeightsForInd(int ind)
    {
        if(ind == 0)
        {
            return population[ind0].GetWeightList();
        }
        return population[ind1].GetWeightList();
    }
}
