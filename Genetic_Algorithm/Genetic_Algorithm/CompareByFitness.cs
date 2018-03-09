using System;

public class CompareByFitness : IComparer<Individual>
{
	public CompareByFitness()
	{
	}


    // sorts decendend
    public int Compare(Individual ind1, Individual ind2)
    {
        int fit1 = ind1.GetFitness();
        int fit2 = ind2.GetFitness();
        return fit2 - fit1;
    }
}
