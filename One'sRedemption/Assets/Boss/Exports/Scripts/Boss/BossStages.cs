using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventHandler : MonoBehaviour
{


    public static Action<BossController.BossStages> OnBossStageChanged;

    public static void BossStageChanged(BossController.BossStages stage)
    {
        OnBossStageChanged(stage);
    }


    




}
