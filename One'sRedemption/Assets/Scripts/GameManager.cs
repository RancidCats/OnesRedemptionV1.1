using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private bool debug;

    [SerializeField] Transform _start;
    [SerializeField] Transform _checkpoint_1;
    [SerializeField] Transform _checkpoint_2;

    //public GameObject protagonist;
    //
    //public GameObject VictoryScreen, DefeatScreen;

    //public float timer, startTime;

    //public bool victoryflag = true;

    //public List<GameObject> enemies = new List<GameObject>();

    public static Vector3 _spawnPos;
    public static bool _pastCheckpoint_1 = false;
    public static bool _pastCheckpoint_2 = false;

    private void Awake()
    {
        instance = this;
        if (!_pastCheckpoint_1 && !_pastCheckpoint_2)
        {
            _spawnPos = _start.position;

        }
        else
        {
            if (_pastCheckpoint_1 && !_pastCheckpoint_2)
            {
                _spawnPos = _checkpoint_1.position;
            }
            else
            {
                _spawnPos = _checkpoint_2.position;
            }
        }
        
    }
    public void Start()
    {
        if (Player.instance!=null)
            Player.instance.transform.position = GameManager._spawnPos;

        print(_spawnPos);

    }

    // Update is called once per frame
    void Update()
    {
        //Quick reload of the scene
        if (Input.GetKeyDown(KeyCode.R) || !Player.instance.gameObject.activeSelf)
        {
            if (_pastCheckpoint_1)
            {
                _spawnPos = _checkpoint_1.position;
                Debug.Log("Entro 1");
            }

            if (_pastCheckpoint_2)
            {
                _spawnPos = _checkpoint_2.position;
                Debug.Log("Entro 2");
            }

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _pastCheckpoint_1 = false;
            _pastCheckpoint_2 = false;
            SceneManager.LoadScene(0);
        }

        if (Input.GetKeyDown(KeyCode.U) && debug) // bossDebug
        {
            Player.instance.transform.position = _checkpoint_1.position;
            Debug.Log("Entro 1");
        }
        if (Input.GetKeyDown(KeyCode.I) && debug)
        {
            Player.instance.transform.position = _checkpoint_2.position;
            Debug.Log("Entro 2");
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
