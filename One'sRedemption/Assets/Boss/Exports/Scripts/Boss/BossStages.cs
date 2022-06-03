using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventHandler : MonoBehaviour
{
    public delegate void BossStageChanged(ref BossController.BossStages stage);
    public static event BossStageChanged OnBossStageChanged;

    public static void BossStageChange(ref BossController.BossStages stage)
    {
        OnBossStageChanged(ref stage);
    }
}
