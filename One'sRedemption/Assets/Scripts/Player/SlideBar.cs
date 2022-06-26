using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlideBar : MonoBehaviour
{

    public Slider slider;
    public Image[] bossLifebarImages;

    public void Awake()
    {
        if (slider != null)
        {
            slider.GetComponent<Slider>();

        }
    }

    public void RefreshBar(float _min, float _max)
    {
        slider.value = _min / _max;
    }
    public void RefreshImage(float _min, float _max)
    {
        if (bossLifebarImages != null)
        {
            for (int i = 0; i < bossLifebarImages.Length; i++)
            {

                bossLifebarImages[i].fillAmount = _min / _max;
            }

        }
    }
}
