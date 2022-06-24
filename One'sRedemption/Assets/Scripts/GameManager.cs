using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private bool debug;

    [SerializeField] public Transform _start;              //Posición del Spawn
    [SerializeField] Transform _checkpoint_1;
    [SerializeField] Transform _checkpoint_2;
    [SerializeField] Transform _checkpoint_3;       //Posición previa al boss utilizada para debugear la fight

    [SerializeField] Transform _startTesting;              //Posición del Spawn

    public static Vector3 spawnPos;                 //Posición en la que va a spawnear el player

    private static bool aux = false;

    public bool vamo_newells = false;

    private void Awake()
    {
        instance = this;

        if (SceneManager.GetActiveScene().buildIndex == 2 && !aux)
        {
            spawnPos = _start.position;
            aux = true;
        }

        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            spawnPos = _startTesting.position;
        }

    }

    public void Start()
    {
        if (Player.instance != null)
            Player.instance.transform.position = GameManager.spawnPos;


        print(spawnPos);
    }

    // Update is called once per frame
    void Update()
    {
        RestartLevel();
        BacktoLastCheckpoint();
        BacktoMenu();
        Debugeo();
        Newells();
    }

    void Newells()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            if (vamo_newells)
                vamo_newells = false;
            else
                vamo_newells = true;
        }
            
    }

    public void RestartLevel()      //Resetea el level desde el principio
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (SceneManager.GetActiveScene().buildIndex == 2)
            {
                spawnPos = _start.position;
            }

            if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                spawnPos = _startTesting.position;
            }
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            EventHandler.ResetEvents();
        }
    }

    public void BacktoLastCheckpoint()      //Resetea el level hasta el último checkpoint
    {
        if (Input.GetKeyDown(KeyCode.L) || !Player.instance.gameObject.activeSelf)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            EventHandler.ResetEvents();
        }
    }

    public void BacktoMenu()        //Volver al Menu Inicial
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (SceneManager.GetActiveScene().buildIndex == 2)
            {
                spawnPos = _start.position;
            }

            if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                spawnPos = _startTesting.position;
            }
            SceneManager.LoadScene(0);
            EventHandler.ResetEvents();
        }
    }

    public void Debugeo()       //Funcion para mover al player por el nivel para debugear
    {
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
        if (Input.GetKeyDown(KeyCode.O) && debug)
        {
            Player.instance.transform.position = _checkpoint_3.position;
            Debug.Log("Entro 3");
        }
    }

}