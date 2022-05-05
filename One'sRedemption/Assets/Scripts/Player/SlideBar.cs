using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SlideBar : MonoBehaviour
{
    public Slider slider;


    private void Awake()
    {
        slider.GetComponent<Slider>();

    }
    public void RefreshBar(float _min, float _max)
    {
        slider.value = _min / _max;
    }
}
