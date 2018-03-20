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

    public PopulationManager(int popSize, int generationSize, int chromosomeSize, double mutationRate)
    {
        this.popSize = popSize;
        this.generationSize = generationSize;
        this.chromosomeSize = chromosomeSize;
        this.mutationRate = mutationRate;

        generation = 0;
        population = new List<Individual>();

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

        EvaluateFitness();
    }

    public void IterateGeneration()
    {
        GenerateNewGeneration();
        EvaluateFitness();
        Survival();
        generation++;
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
        for (int i = 0; i < population.Count; i++)
        {
            for (int j = (i + 1); j < population.Count; j++)
            {
                WriteWeights(population[i], population[j]);
                MatchStarter.StartMatch();
                ReadIntermediateFitness(i, j);
            }
        }

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

    private void WriteWeights(Individual ind0, Individual ind1)
    {
        StreamWriter writer0 = new StreamWriter("Weights0.txt");
        List<string> weigths0 = ind0.GetWeightsAsStrings();
        foreach(string weight in weigths0)
        {
            writer0.WriteLine(weight);
        }
        writer0.Close();

        StreamWriter writer1 = new StreamWriter("Weights1.txt");
        List<string> weigths1 = ind1.GetWeightsAsStrings();
        foreach (string weight in weigths1)
        {
            writer1.WriteLine(weight);
        }
        writer1.Close();
    }

    private void ReadIntermediateFitness(int ind0, int ind1)
    {
        StreamReader reader = new StreamReader("Results.txt");
        string fitnessString0 = reader.ReadLine();
        string fitnessString1 = reader.ReadLine();
        reader.Close();

        int strength0 = int.Parse(fitnessString0);
        int strength1 = int.Parse(fitnessString1);

        int fit0 = (offsetForFitness + strength0) - strength1;
        int fit1 = (offsetForFitness + strength1) - strength0;

        intermediateFitnessResults[ind0, ind1] = fit0;
        intermediateFitnessResults[ind1, ind0] = fit1;
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
}
