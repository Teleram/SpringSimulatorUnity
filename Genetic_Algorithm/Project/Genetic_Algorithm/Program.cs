public class Program
{
    public static void Main(string[] args)
    {
        PopulationManager popMan = new PopulationManager(4, 4, 140, 0.05);
        Agent agent = new Agent(popMan);

        int iterations = 2;
        //if (!(int.TryParse(args[0], out iterations)))
        //{
        //    iterations = 10;
        //}

        agent.WriteWeightsOfCurrentGeneration();

        for (int i = 0; i < iterations; i++)
        {
            popMan.IterateGeneration();
            agent.WriteWeightsOfCurrentGeneration();
        }
    }
}