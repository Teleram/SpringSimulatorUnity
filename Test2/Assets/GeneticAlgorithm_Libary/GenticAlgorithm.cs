using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NeuralNetwork;

public class GenticAlgorithm : MonoBehaviour
{
    private PopulationManager popMan;

    private AIBrain_Simulator aIBrain0;
    private AIBrain_Simulator aIBrain1;

    public GameObject gameMasterPrefab;
    private GameMasterScript_Singleplayer gameMaster;

    public GameObject playerObjectPrefab;
    private GameObject playerObject0;
    private GameObject playerObject1;

    private bool newSetUp;

    // Use this for initialization
    void Start()
    {
        popMan = new PopulationManager(16, 32, 140, 0.05);
        newSetUp = false;
        SetUp();
    }

    private void SetUp()
    {
        GameObject tmp = Instantiate(gameMasterPrefab);
        gameMaster = tmp.GetComponent<GameMasterScript_Singleplayer>();

        playerObject0 = Instantiate(playerObjectPrefab);
        playerObject1 = Instantiate(playerObjectPrefab);
        aIBrain0 = new AIBrain_Simulator(popMan.GetWeightsForInd(0));
        aIBrain1 = new AIBrain_Simulator(popMan.GetWeightsForInd(1));
    }

    void Update()
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

            popMan.NextMatch(unitsOfPlayer0, unitsOfPlayer1);
            CleanUp();
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
