﻿using System;

public class PopulationManager
{
    private List<Individual> population;
    private int chromosomeSize;

    private CompareByFitness comparer;

    private int popSize;

    // size of the newly generated individuals per generation
    private int generationSize;

    private double mutationRate;

	public PopulationManager(int popSize, int generationSize, int chromosomeSize, double mutationRate)
	{
        this.popSize = popSize;
        this.generationSize = generationSize;
        this.chromosomeSize = chromosomeSize;
        this.mutationRate = mutationRate;
        comparer = new CompareByFitness();
	}

    private void Survival()
    {
        sortPopulation();
        population = population.GetRange(0, popSize);
    }

    private void GenerateNewGeneration()
    {
        Dictionary<Individual, double> indToProbability = FitnessToProbability(population);

        for(int i = 1; i <= generationSize; i ++)
        {
            Random randomDouble = new Random();
            double probParent1 = randomDouble.nextDouble();
            double probParent2 = randomDouble.nextDouble();

            Individual parent1 = GetIndividualFormDictionary(probParent1, indToProbability);
            Individual parent2 = GetIndividualFormDictionary(probParent2, indToProbability);
            while(parent1 == parent2)
            {
                probParent2 = randomDouble.nextDouble();
                parent2 = GetIndividualFormDictionary(probParent2, indToProbability);
            }

            int cut = new Random().NextInt(1, chromosomeSize);
            List<Gene> partOfParent1 = parent1.getGenes().GetRange(0, cut);
            List<Gene> partOfParent2 = parent2.getGenes().GetRange(cut, (chromosomeSize - cut));
            List<Gene> child = partOfParent1.Concat(partOfParent2);

            double mutation = new Random().nextDouble();
            if(mutation <= mutationRate)
            {
                int mutatedGene = new Random().NextInt(chromosomeSize);
                child[mutatedGene] = new Gene();
            }
            Chromosome newChromosome = new Chromosome(child);
            Individual newChild = new Individual(newChromosome);
            population.add(newChild);
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
            indToProbability.add(ind, probability);
        }

        return indToProbability;
    }

    private Individual GetIndividualFormDictionary(double prob, Dictionary<Individual, double> dict)
    {
        double sum = 0.0;
        foreach(KeyValuePair<Individual, double> pair in dict)
        {
            sum += pair.Value;
            if(prob <= sum)
            {
                return pair.Key;
            }
        }
        return null;
    }

    private void writeWeights(Individual ind0, Individual ind1)
    {
        Streamwriter writer0 = new Streamwriter("spring-simulator/Test2/Weights0.txt");
        writer0.Write(ind0.ToString());
        writer0.Close();
        Streamwriter writer1 = new Streamwriter("spring-simulator/Test2/Weights1.txt");
        writer1.Write(ind1.ToString());
        writer1.Close();
    }

    private void sortPopulation()
    {
        population.sort(comparer);
    }
}
