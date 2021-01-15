using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using PlayFab.Json;
using UnityEngine.UI;

public class SaveScore : MonoBehaviour
{

    [SerializeField]
    GameObject popUpEnd;

    public Text textScore;
    public static int myScore;
    public int myLastScore = -1;
    int lastPositionScore;
    int lastScore = 0;
    string scoreToString = "";


    private void Start()
    {
        myScore = 0;
        GetLeaderboarder();
    }

    public void saveData()
    {
        Debug.Log("SAVE DATA");
        Debug.Log(myScore + "last score " + myLastScore);
        if (myScore < myLastScore)
        {
            popUpEnd.SetActive(true);
            FadeMusic.fadeMusic.gameOverMusic();
            StartCoroutine(rankingScene());
            PauseController.pause.pauseGame();
            return;
        }
        else
            StartCloudUpdatePlayerStats();

    }

    private void Update()
    {
        if (lastScore != myScore)
        {
            string formatPoints = "000000";
            lastScore = myScore;
            scoreToString = lastScore.ToString();
            formatPoints = formatPoints.Remove(0, scoreToString.Length);
            textScore.text = formatPoints + scoreToString;
        }

    }


    private void StartCloudUpdatePlayerStats()
    {

        PlayFabClientAPI.ExecuteCloudScript(new ExecuteCloudScriptRequest()
        {
            FunctionName = "UpdatePlayerStats", // Arbitrary function name (must exist in your uploaded cloud.js file)
            FunctionParameter = new { score = myScore }, // The parameter provided to your function
            GeneratePlayStreamEvent = true, // Optional - Shows this event in PlayStream
        }, OnCloudUpdatePlayerStats, OnErrorShared);
    }

    private void OnCloudUpdatePlayerStats(ExecuteCloudScriptResult result)
    {
        // CloudScript returns arbitrary results, so you have to evaluate them one step and one parameter at a time
        Debug.Log(PlayFab.PluginManager.GetPlugin<ISerializerPlugin>(PluginContract.PlayFab_Serializer));
        JsonObject jsonResult = (JsonObject)result.FunctionResult;
        object messageValue;
        jsonResult.TryGetValue("messageValue", out messageValue); // note how "messageValue" directly corresponds to the JSON values set in CloudScript
        Debug.Log((string)messageValue);

        popUpEnd.SetActive(true);
        FadeMusic.fadeMusic.gameOverMusic();
        StartCoroutine(rankingScene());
        PauseController.pause.pauseGame();
    }

    IEnumerator rankingScene()
    {
        yield return new WaitForSecondsRealtime(4);
        PauseController.pause.unPaused();
        ChangeScene.changeScene.startToChange("Congrats");
    }


    private void OnErrorShared(PlayFabError error)
    {
        Debug.Log(error.GenerateErrorReport());
    }




    public void GetLeaderboarder()
    {
        var requestLeaderBoard = new GetLeaderboardRequest { StartPosition = 0, StatisticName = "Score" };
        PlayFabClientAPI.GetLeaderboard(requestLeaderBoard, OnGetLeaderboard, OnErrorLeaderboard);
    }

    void OnGetLeaderboard(GetLeaderboardResult result)
    {
        if (result.Leaderboard.Count == 10)
            lastPositionScore = result.Leaderboard[9].StatValue;
        else
        {
            lastPositionScore = 0;
        }

        foreach (PlayerLeaderboardEntry player in result.Leaderboard)
        {
            if (player.DisplayName == PlayerPrefs.GetString("nick"))
            {
                myLastScore = player.StatValue;
            }
        }
    }

    void OnErrorLeaderboard(PlayFabError error)
    {
        Debug.LogError(error.GenerateErrorReport());
    }


}
