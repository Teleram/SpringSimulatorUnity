﻿using System;

public class Chromosome
{
    private readonly List<Gene> genes;

    public Chromosome(List<Gene> genes)
    {
        this.genes = genes;
    }

	public Chromosome(int numberOfGenes)
	{
        genes = new List<Gene>(numberOfGenes);
        randomInitilisation();
    }

    private void randomInitilisation()
    {
        for (int i = 0; i < numberOfGenes; i++)
        {
            genes[i] = new Gene();
        }
    }

    public int getNumberOfGenes()
    {
        return genes.Count;
    }

    public Gene getGene(int index)
    {
        return genes[index];
    }

    public List<Gene> getGenes()
    {
        return genes;
    }

    public override string ToString()
    {
        string s = "";
        s += genes[0].ToString;
        for(int i = 1; i < genes.Count; i++)
        {
            s += "\n";
            s += genes[i].ToString;
        }
        return s;
    }
}
