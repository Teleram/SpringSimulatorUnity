using System;

public class Gene
{
    private readonly float value;

	public Gene()
	{
        Random random = new Random();
        value = (float)(random.NextDouble() * 2) - 1;
	}

    public float GetValue()
    {
        return value;
    }

    public override string ToString()
    {
        return value.ToString();
    }
}
