using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GamePauseOpt : MonoBehaviour
{
    public void pauseGame()
    {
        FadeMusic.fadeMusic.pauseMusic();
        PauseController.pause.pauseGame();
    }

    public void unPause()
    {
        FadeMusic.fadeMusic.unPauseMusic();
        PauseController.pause.unPaused();
    }

    public void goHome()
    {
        PauseController.pause.unPaused();
        Debug.Log("GO HOME");
        ChangeScene.changeScene.startToChange("LoginScene");
    }

    public void reloadGame()
    {
        PauseController.pause.unPaused();
        ChangeScene.changeScene.startToChange(SceneManager.GetActiveScene().name);
    }

    public void changeScene(string newScene)
    {
        PauseController.pause.unPaused();
        Debug.Log("GO HOME");
        ChangeScene.changeScene.startToChange(newScene);
    }



}
