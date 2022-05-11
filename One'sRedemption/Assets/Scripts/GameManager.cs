using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] Transform _start;
    [SerializeField] Transform _checkpoint;

    //public GameObject protagonist;
    //
    //public GameObject VictoryScreen, DefeatScreen;

    //public float timer, startTime;

    //public bool victoryflag = true;

    //public List<GameObject> enemies = new List<GameObject>();

    public static Vector3 _spawnPos;
    public static bool _pastCheckpoint = false;

    private void Awake()
    {
        instance = this;
        if (!_pastCheckpoint)
            _spawnPos = _start.position;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Quick reload of the scene
        if (Input.GetKeyDown(KeyCode.R) || !Player.instance.gameObject.activeSelf)
        {
            if (_pastCheckpoint)
                _spawnPos = _checkpoint.position;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _pastCheckpoint = false;
            SceneManager.LoadScene(0);
        }


        //if (!Player.instance.gameObject.activeSelf)
        //{
        //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //}

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
