using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventHandler : MonoBehaviour
{
    public delegate void BossStageChanged();
    public static event BossStageChanged BossStageHandler;
    public static void BossStageChange()
    {
        BossStageHandler();
        Debug.Log("Boss stage changed!");
    }

    public static void ResetEvents()
    {
        BossStageHandler = null;
    }
}
