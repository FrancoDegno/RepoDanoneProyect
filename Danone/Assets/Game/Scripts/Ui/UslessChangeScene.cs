using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UslessChangeScene : MonoBehaviour
{
    public void btnChangeScene(string nameScene)
    {
        ChangeScene.changeScene.startToChange(nameScene);
    }


}
