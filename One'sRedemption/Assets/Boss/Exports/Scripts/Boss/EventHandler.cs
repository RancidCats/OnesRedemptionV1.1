using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventHandler : MonoBehaviour
{
    /// <summary>
    /// Se crea el delegate tipo BossStageChanged()
    /// Se crea un handler del mismo tipo, para manejar las fases y los metodos suscritos.
    /// </summary>
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
