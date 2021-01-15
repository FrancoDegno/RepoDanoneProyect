using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class PlayFabLogin : MonoBehaviour
{


    string nameAcc="";
    string surname="";
    public GameObject popUpError;
    public Text errorText;
    public GameObject popUpRegister;
    public GameObject popUpWelcome;
    public static bool login = false;
    bool register = false;
    string nickuser;


    private void Start()
    {
       // PlayerPrefs.DeleteAll();
    }


    public void setName(string n)
    {
        nameAcc = n;
    }

    public void setSurname(string sn)
    {
        surname = sn;
    }


    public void createAndLogin()
    {
        if(nameAcc=="" || surname=="")
        {
            errorText.text = "Error:The name and last name need to have at least 3 letters.";
             popUpError.SetActive(true);
        }
        else
        {
            Debug.Log("TRY ENTER");

            nickuser = nameAcc.ToLower() + " " + surname.ToLower();
            var request = new LoginWithCustomIDRequest { CustomId = nameAcc.ToLower() + " " + surname.ToLower(), CreateAccount = true };
            PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginFailure);
        }
        
    }


    private void OnLoginSuccess(LoginResult result)
    {
        login = true;
        PlayFabClientAPI.UpdateUserTitleDisplayName(new UpdateUserTitleDisplayNameRequest { DisplayName = nameAcc.ToLower()+" "+surname.ToLower() }, OnDisplayName, OnLoginFailure);

        Debug.Log("Login Success");

        if(PlayerPrefs.HasKey("nick"))
        {
            if(PlayerPrefs.GetString("nick")==nickuser)
            {
                popUpWelcome.SetActive(true);
            }
            else
            {
                popUpRegister.SetActive(true);
                PlayerPrefs.SetString("nick", nickuser);         
            }
            
        }
        else
        {
            popUpRegister.SetActive(true);
            PlayerPrefs.SetString("nick", nickuser);
        }
       // PauseController.pause.pauseGame();
        //startToChange();

    }

    private void OnLoginFailure(PlayFabError error)
    {
        Debug.Log("FAILURE");
        errorText.text = error.GenerateErrorReport();
        popUpError.SetActive(true);
    }




    #region RegisterName
    void OnRegisterSuccess(RegisterPlayFabUserRequest result)
    {
        PlayFabClientAPI.UpdateUserTitleDisplayName(new UpdateUserTitleDisplayNameRequest { DisplayName = nameAcc + " " + surname }, OnDisplayName,OnLoginFailure);
        register = true;
        popUpRegister.SetActive(true);
        //PauseController.pause.pauseGame();
    }

    void OnDisplayName(UpdateUserTitleDisplayNameResult result)
    {
        Debug.Log(result.DisplayName + " is  your new display name");
    }
    #endregion RegisterName



    public void startToChange()
    {
        Debug.Log("START TO CHANGE");
        CallToFade.callToFade.fadeIn();
        FadeMusic.fadeMusic.fadeIn();
        StartCoroutine(ChangeScene());
        
    }

    IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(2);
        Debug.Log("ChangeScene");
        SceneManager.LoadScene(1);
    }


    

}