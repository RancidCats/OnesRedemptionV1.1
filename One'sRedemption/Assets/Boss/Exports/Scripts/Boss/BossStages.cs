using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventHandler : MonoBehaviour
{
    public delegate void BossStageChanged();
    public static event BossStageChanged OnBossStageChanged;

    public static void BossStageChange()
    {
        OnBossStageChanged();
        Debug.Log("Boss stage changed!");
    }

}
