using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsText : MonoBehaviour
{
    [SerializeField] GameObject _controlsText;

    private void Start()
    {
        _controlsText.SetActive(false);
    }

    public void switchControls()
    {
        if (_controlsText.activeSelf)
        {
            _controlsText.SetActive(false);
        }
        else
            _controlsText.SetActive(true);
    }
}
