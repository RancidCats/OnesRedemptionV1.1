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
        if(GameManager.instance.vamo_newells)
            AudioManager.instance.Play("Vamo_Newells");
        else
            AudioManager.instance.Play("Boss_Minotaur_Scream");

        BossController.instance.particles_FaseChange.SetActive(true);
        BossStageHandler();
        Debug.Log("Boss stage changed!");
    }

    public static void ResetEvents()
    {
        BossStageHandler = null;
    }
}
