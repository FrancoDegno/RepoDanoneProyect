using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public static ChangeScene changeScene;

    private void Start()
    {

        

        if (changeScene != null)
            Destroy(this.gameObject);

        changeScene = this;
        DontDestroyOnLoad(this.gameObject);

    }

    public void startToChange(string sceneName)
    {
        CallToFade.callToFade.fadeIn();
        FadeMusic.fadeMusic.fadeIn();
        StartCoroutine(changeToScene(sceneName));
    }

    IEnumerator changeToScene(string sceneName)
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(sceneName);
    }


}
