using System.IO;

public class Agent
{
    PopulationManager popMan;

    public Agent(PopulationManager popMan)
    {
        this.popMan = popMan;
    }

    public void WriteToFile()
    {
        string serializationFile = "Gen_Alg_intermediate_Result.bin";
        using (Stream stream = File.Open(serializationFile, FileMode.Create))
        {
            var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

            bformatter.Serialize(stream, popMan);
        }
    }

    public void ReadFromFile(string serializationFile)
    {
        using (Stream stream = File.Open(serializationFile, FileMode.Open))
        {
            var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

            popMan = (PopulationManager)bformatter.Deserialize(stream);
        }
    }
}