using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using NeuralNetwork;

public class Eval : MonoBehaviour {

    private AIBrain_Simulator aIBrain0;
    private AIBrain_Simulator aIBrain1;

    public GameObject gameMasterPrefab;
    private GameMasterScript_Singleplayer gameMaster;

    public GameObject playerObjectPrefab;
    private GameObject playerObject0;
    private GameObject playerObject1;

    private bool newSetUp;

    private int gen;
    private int ind;
    private string path;

    // Use this for initialization
    void Start()
    {
        path = "Eval/";
        SetUp();
    }

    private void SetUp()
    {
        string redTeam = path + "resultWeights_Gen" + gen + "_ind0.txt";
        StreamReader reader0 = new StreamReader(redTeam);
        List<string> weightStrings0 = new List<string>();
        while (!reader0.EndOfStream)
        {
            weightStrings0.Add(reader0.ReadLine());
        }
        reader0.Close();
        List<float> weights0 = new List<float>();
        foreach (string weightString in weightStrings0)
        {
            float weight = float.Parse(weightString);
            weights0.Add(weight);
        }

        string blueTeam = path + "resultWeights_Gen0_ind" + ind + ".txt";
        StreamReader reader1 = new StreamReader(blueTeam);
        List<string> weightStrings1 = new List<string>();
        while (!reader1.EndOfStream)
        {
            weightStrings1.Add(reader1.ReadLine());
        }
        reader1.Close();
        List<float> weights1 = new List<float>();
        foreach (string weightString in weightStrings1)
        {
            float weight = float.Parse(weightString);
            weights1.Add(weight);
        }

        GameObject tmp = Instantiate(gameMasterPrefab);
        gameMaster = tmp.GetComponent<GameMasterScript_Singleplayer>();

        playerObject0 = Instantiate(playerObjectPrefab);
        playerObject1 = Instantiate(playerObjectPrefab);
        aIBrain0 = new AIBrain_Simulator(weights0);
        aIBrain1 = new AIBrain_Simulator(weights1);
    }

    void FixedUpdate()
    {
        if (newSetUp)
        {
            newSetUp = false;
            SetUp();
        }

        if (gameMaster.GameTimerOver())
        {
            int unitsOfPlayer0 = gameMaster.UnitsOfPlayer(0);
            int unitsOfPlayer1 = gameMaster.UnitsOfPlayer(1);

            string currentPath = path + "EvalResult" + gen + "vs" + ind + ".txt";
            StreamWriter writer = new StreamWriter(currentPath);
            writer.WriteLine(unitsOfPlayer0);
            writer.WriteLine(unitsOfPlayer1);
            writer.Close();
            CleanUp();

            ind++;
            if(ind > 15)
            {
                gen++;
                if(gen <= 10)
                {
                    ind = 0;
                }
                else
                {
                    Application.Quit();
                }
            }
            newSetUp = true;
        }
    }

    private void CleanUp()
    {
        gameMaster.CleanUp();
        Destroy(playerObject0);
        Destroy(playerObject1);
    }

    public AIBrain_Simulator GetAIBrain(int playerID)
    {
        if (playerID == 0)
        {
            return aIBrain0;
        }
        return aIBrain1;
    }
}