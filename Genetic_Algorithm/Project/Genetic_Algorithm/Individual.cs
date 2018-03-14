using System;
using System.Collections.Generic;

public class Individual
{
    private readonly Chromosome chromosome;
    private int fitness;

    public Individual(int numberOfGenes)
    {
        chromosome = new Chromosome(numberOfGenes);
        fitness = 0;
    }

    public Individual(int numberOfGenes, Random random)
    {
        chromosome = new Chromosome(numberOfGenes, random);
        fitness = 0;
    }

    public Individual(Chromosome chromosome)
	{
        this.chromosome = chromosome;
        fitness = 0;
	}

    public Chromosome GetChromosome()
    {
        return chromosome;
    }

    public List<Gene> GetGenes()
    {
        return chromosome.GetGenes();
    }

    public int GetFitness()
    {
        return fitness;
    }

    public void SetFitness(int newFitness)
    {
        fitness = newFitness;
    }

    public override string ToString()
    {
        return chromosome.ToString();
    }

    public List<string> GetWeightsAsStrings()
    {
        return chromosome.GetWeightsAsStrings();
    }
}
