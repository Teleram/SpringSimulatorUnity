﻿using System;

public class Individual
{
    private readonly Chromosome chromosome;
    private int fitness;

    public Individual(int numberOfGenes)
    {
        chromosome = new Chromosome(numberOfGenes);
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

    public void setFitness(int newFitness)
    {
        fitness = newFitness;
    }

    public override string ToString()
    {
        return chromosome.ToString();
    }
}
