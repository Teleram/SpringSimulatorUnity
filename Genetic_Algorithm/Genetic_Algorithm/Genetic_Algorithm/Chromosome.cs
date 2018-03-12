using System;
using System.Collections.Generic;

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
        RandomInitilisation();
    }

    private void RandomInitilisation()
    {
        for (int i = 0; i < genes.Capacity; i++)
        {
            genes[i] = new Gene();
        }
    }

    public int GetNumberOfGenes()
    {
        return genes.Count;
    }

    public Gene GetGene(int index)
    {
        return genes[index];
    }

    public List<Gene> GetGenes()
    {
        return genes;
    }

    public override string ToString()
    {
        string s = "";
        s += genes[0].ToString();
        for(int i = 1; i < genes.Count; i++)
        {
            s += "\n";
            s += genes[i].ToString();
        }
        return s;
    }
}
