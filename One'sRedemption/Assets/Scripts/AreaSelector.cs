using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaSelector : MonoBehaviour
{
    
    [SerializeField] GameObject[] _areaOfActivity;
    [SerializeField] int _freeZoneIndex;
    [SerializeField] float restartTime;
    [SerializeField] float runningForSec;
  
    public bool running;

    public void Start()
    {
        running = false;
        for (int i = 0; i < _areaOfActivity.Length; i++)
        {
            _areaOfActivity[i].GetComponent<AreaOfActivity>()._myIndex = i;
            
        }
    }
    
    public void Running()
    {
        StartCoroutine(Selector());
    }
    IEnumerator Selector()
    {
        while (BossController.instance.Health > 0)
        {
            StartAreaActivity();
            yield return new WaitForSeconds(runningForSec);
            RestartValuesOfActivity();
            yield return new WaitForSeconds(restartTime);
        }
        
    }
    void RestartValuesOfActivity()
    {
        for (int i = 0; i < _areaOfActivity.Length; i++)
        {
            _areaOfActivity[i].SetActive(true);
            _areaOfActivity[i].GetComponent<AreaOfActivity>().StopSpawn();
        }
    }
    public void SelectFreeZone()
    {
        int count = 0;
        for (int i = 0; i < _areaOfActivity.Length; i++)
        {
            if (_areaOfActivity[i].GetComponent<AreaOfActivity>().playerInArea)
            {               
                FreeZone(i);
                while (!_areaOfActivity[_freeZoneIndex].GetComponent<AreaOfActivity>()._toggle)
                {
                    FreeZone(i);
                }
                _areaOfActivity[_freeZoneIndex].SetActive(false);
                count = 0;
            }
            else
            {
                count++;
                if (count == _areaOfActivity.Length - 1)
                {
                    _freeZoneIndex = Random.Range(0, _areaOfActivity.Length);
                    while (!_areaOfActivity[_freeZoneIndex].GetComponent<AreaOfActivity>()._toggle)                 
                    {
                        _freeZoneIndex = Random.Range(0, _areaOfActivity.Length);

                    }

                    _areaOfActivity[Random.Range(0,_areaOfActivity.Length)].SetActive(false);
                } 
            }
        }
    int FreeZone(int i)
    {
        _freeZoneIndex = Random.Range(0, _areaOfActivity.Length);     
        while (_freeZoneIndex == i)
        {
            _freeZoneIndex = Random.Range(0, _areaOfActivity.Length);
        }
        return _freeZoneIndex;
    }
    }
    public void StartAreaActivity()
    {
        SelectFreeZone();
        for (int j = 0; j < _areaOfActivity.Length; j++)
        {
            if (_areaOfActivity[j].activeSelf)
            {
                _areaOfActivity[j].GetComponent<AreaOfActivity>().StartSpawn();
                
            }
        }                                           
    }
}
