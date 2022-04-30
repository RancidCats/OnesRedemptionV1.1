using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void GoToScene(int sceneIndex)
    {
        //print("Hola mundo");
        SceneManager.LoadScene(sceneIndex);
    }
}
