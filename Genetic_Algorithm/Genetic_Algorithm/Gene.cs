using System;

public class Gene
{
    private readonly float value;

	public Gene()
	{
        Random random = new Random();
        value = (float)(random.nextDouble() * 2) - 1;
	}

    public float getValue()
    {
        return value;
    }
}
