using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerTemp : MonoBehaviour
{

    [SerializeField] Transform[] waypoints;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Player.instance.transform.position = waypoints[0].position;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Player.instance.transform.position = waypoints[1].position;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Player.instance.transform.position = waypoints[2].position;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
