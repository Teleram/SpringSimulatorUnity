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
        genes = new List<Gene>();

        for (int i = 0; i < numberOfGenes; i++)
        {
            genes.Add(new Gene());
        }
    }

    public Chromosome(int numberOfGenes, Random random)
    {
        genes = new List<Gene>();

        for (int i = 0; i < numberOfGenes; i++)
        {
            genes.Add(new Gene(random));
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

    public List<string> GetWeightsAsStrings()
    {
        List<string> result = new List<string>();
        foreach(Gene gene in genes)
        {
            result.Add(gene.ToString());
        }
        return result;
    }
}
