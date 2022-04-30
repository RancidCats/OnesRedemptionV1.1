using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public static UIManager instance;

    public Image[] helmets;

    private void Awake()
    {
        instance = this;
    }

    public void HelmetsUpdate(int maxHp, int currentHp)
    {
        for (int i = 0; i < helmets.Length; i++)
        {
            if (currentHp - 1 < i) helmets[i].enabled = false;
            else helmets[i].enabled = true;
        }
    }
}
