using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    //public GameObject protagonist;
    //
    //public GameObject VictoryScreen, DefeatScreen;

    //public float timer, startTime;

    //public bool victoryflag = true;

    //public List<GameObject> enemies = new List<GameObject>();

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Quick reload of the scene
        if (Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        if (!Player.instance)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        //If protagonist is dead timer stops and shows defeat screen
        
        //if (Player.instance)
        //    timer -= Time.deltaTime;
        //else
        //    DefeatScreen.SetActive(true);

        //This is just to delay the win function until all the enemies are added to the list
        
        //if (startTime >= 2)
        //    Win();
        //else
        //    startTime += Time.deltaTime;
    }

    //Win function
    //public void Win()
    //{
    //    if (enemies.Count == 0 && victoryflag)
    //    {
    //        VictoryScreen.SetActive(true);
    //        print("YOU DEFEATED ALL THE ENEMIES, CONGRATULATIONS");
    //        victoryflag = false;
    //    }
    //}
}
