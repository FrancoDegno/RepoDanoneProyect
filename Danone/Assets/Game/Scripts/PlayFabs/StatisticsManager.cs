using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using PlayFab.Json;

public class StatisticsManager : MonoBehaviour
{
    public int myScore;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            if(PlayFabLogin.login)
            {
                GetLeaderboarder();
                //StartCloudUpdatePlayerStats();
                //saveStatistics();
            }
        }
    }


    void saveStatistics()
    {
       /* PlayFabClientAPI.UpdatePlayerStatistics(new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
            {
                new StatisticUpdate{ StatisticName = "score",Value = myScore},
            }
        },
        result => { Debug.Log("User statistics update"); },
        error => { Debug.Log(error.GenerateErrorReport()); }
        );*/
        
    }

    /*public void CreatePlayerStatisticDefinition()
    {
        PlayFabAdminAPI.CreatePlayerStatisticDefinition(
            new CreatePlayerStatisticDefinitionRequest()
            {
                StatisticName = "Headshots",
                VersionChangeInterval = StatisticResetIntervalOption.Day
            },
            result => Debug.Log("Complete"),
            error => Debug.Log(error.GenerateErrorReport())
        );
    }*/

    private void StartCloudUpdatePlayerStats()
    {
        PlayFabClientAPI.ExecuteCloudScript(new ExecuteCloudScriptRequest()
        {
            FunctionName = "UpdatePlayerStats", // Arbitrary function name (must exist in your uploaded cloud.js file)
            FunctionParameter = new {Score = myScore}, // The parameter provided to your function
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
    }

    private void OnErrorShared(PlayFabError error)
    {
        Debug.Log(error.GenerateErrorReport());
    }


    void GetStatistics()
    {
        PlayFabClientAPI.GetPlayerStatistics(
            new GetPlayerStatisticsRequest(),
            OnGetStatistics,
            error => Debug.LogError(error.GenerateErrorReport())
        );
    }

    void OnGetStatistics(GetPlayerStatisticsResult result)
    {
        Debug.Log("Received the following Statistics:");
        foreach (var eachStat in result.Statistics)
            Debug.Log("Statistic (" + eachStat.StatisticName + "): " + eachStat.Value);
    }


    #region LeaderBoard

    public void GetLeaderboarder()
    {
        var requestLeaderBoard = new GetLeaderboardRequest { StartPosition = 0, StatisticName = "Score", MaxResultsCount = 20 };
        PlayFabClientAPI.GetLeaderboard(requestLeaderBoard, OnGetLeaderboard, OnErrorLeaderboard);
    }

    void OnGetLeaderboard(GetLeaderboardResult result)
    {
        Debug.Log("GET LEADERBOARD"+ result.Leaderboard.Count);
        foreach(PlayerLeaderboardEntry player in result.Leaderboard)
        {
            Debug.Log(player.DisplayName + ": " + player.StatValue);
        }
    }

    void OnErrorLeaderboard(PlayFabError error)
    {
        Debug.LogError(error.GenerateErrorReport());
    }

    #endregion LeaderBoard


}
