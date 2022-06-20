using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonManager : MonoBehaviour
{
    public static CannonManager instance;
    
    public GameObject cannonPrefab;
    public Transform playerTarget;
    public Transform[] transforms = new Transform[3];
    public bool isEnabled;

    //timers
    float timer;
    bool rotateCannons = false;
    Dictionary<int, CannonScript> cannons = new Dictionary<int, CannonScript>();

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }
    private void Start()
    {
        rotateCannons = true;
    }
    private void FixedUpdate()
    {
        if (isEnabled)
            CannonRotationBehaviour();
        else DisableAllCannons();
    }
    public void SpawnCannons()
    {
        for(int i = 0; i < transforms.Length; i++)
        {
            GameObject go = Instantiate(cannonPrefab, transforms[i].position, transforms[i].rotation);
            go.tag = "Cannon";  
            var cannon = go.GetComponent<CannonScript>();
            cannon.target = playerTarget;
            cannons.Add(i, cannon);
            go.name = $"Cannon{i+1}";
            print($"Cannon{i} spawned");
        }
    }

    void CannonRotationBehaviour()
    {
        if (rotateCannons)
        {
            rotateCannons = false;
            RandomizeCannons();
        }
        else timer += Time.fixedDeltaTime;
        if (timer >= 7)
        {
            timer = 0;
            rotateCannons = true;
        }
    }
    
    void RandomizeCannons()
    {
        int x = Random.Range(0, 3);
        switch (x)
        {
            case 0:
                cannons[1].isEnabled = true;
                cannons[2].isEnabled = true;
                cannons[x].isEnabled = false;
                break;
            case 1:
                cannons[0].isEnabled = true;
                cannons[2].isEnabled = true;
                cannons[x].isEnabled = false;
                break;
            case 2:
                cannons[0].isEnabled = true;
                cannons[1].isEnabled = true;
                cannons[x].isEnabled = false;
                break;

        }
    }
    void DisableAllCannons()
    {
        foreach(var cannon in cannons)
        {
            cannon.Value.isEnabled = false;
        }
    }
}
