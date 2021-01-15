using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using PlayFab.Json;
using UnityEngine.UI;

public class LeaderBoardManager : MonoBehaviour
{
    [SerializeField]
    Transform boardElement;
    [SerializeField]
    bool markPosition=false;
    [SerializeField]
    Text posText;
    [SerializeField]
    GameObject fw1;
    [SerializeField]
    GameObject fw2;
    // Start is called before the first frame update
    void Start()
    {
        GetLeaderboarder();
    }


    #region LeaderBoard

    public void GetLeaderboarder()
    {
        var requestLeaderBoard = new GetLeaderboardRequest { StartPosition = 0, StatisticName = "Score" };
        PlayFabClientAPI.GetLeaderboard(requestLeaderBoard, OnGetLeaderboard, OnErrorLeaderboard);
    }

    void OnGetLeaderboard(GetLeaderboardResult result)
    {
        int i = 0;

        if (markPosition)
        {
            Debug.Log("MARK POSITION");
            string myNick = PlayerPrefs.GetString("nick");
            int indexPosition = result.Leaderboard.FindIndex(x => x.DisplayName == myNick);
            if(indexPosition>=0 && indexPosition<=9 && result.Leaderboard[indexPosition].StatValue>0)
            {
                indexPosition += 1;
                Debug.Log("INDEX POSITION IS " + indexPosition + " in nickname " + myNick);
                posText.text = "Congratulations! You made it into the #" + indexPosition + " place of the leaderboard!";
                Color markColor = posText.color;
                markColor.a = 1;
                fw1.SetActive(true);
                fw2.SetActive(true);
                boardElement.GetChild(indexPosition).GetChild(0).transform.GetChild(0).GetComponent<Text>().color = markColor;
                boardElement.GetChild(indexPosition).GetChild(1).transform.GetChild(0).GetComponent<Text>().color = markColor;
                boardElement.GetChild(indexPosition).GetChild(2).transform.GetChild(0).GetComponent<Text>().color = markColor;
            }
            else
            {
                posText.gameObject.SetActive(false);
            }

        }


        foreach (PlayerLeaderboardEntry player in result.Leaderboard)
        {
            if (player.StatValue == 0)
                return;

            i += 1;
            if(boardElement.childCount>i)
            {
                boardElement.GetChild(i).GetChild(0).transform.GetChild(0).GetComponent<Text>().text = i.ToString();
                boardElement.GetChild(i).GetChild(1).transform.GetChild(0).GetComponent<Text>().text = player.DisplayName;
                string textValue = "000000";
                string recivedValue = player.StatValue.ToString();

                textValue= textValue.Remove(0, recivedValue.Length);
                //Debug.Log(recivedValue);

                boardElement.GetChild(i).GetChild(2).transform.GetChild(0).GetComponent<Text>().text = textValue+recivedValue;
            }

            //Debug.Log(player.DisplayName + ": " + player.StatValue);
        }



    }

    void OnErrorLeaderboard(PlayFabError error)
    {
        Debug.LogError(error.GenerateErrorReport());
    }

    #endregion LeaderBoard


}
