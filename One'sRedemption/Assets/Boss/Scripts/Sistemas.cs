using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sistemas
{

    public static bool IsAnimationPlaying(Animator ani, string stateName)
    {
        if (ani.GetCurrentAnimatorStateInfo(0).IsName(stateName))
        {
            return true;
        }
        else return false;
    }
    public static bool IsAnimationPlaying(Animator ani, string stateName, int time)
    {
        if (ani.GetCurrentAnimatorStateInfo(0).IsName(stateName) && ani.GetCurrentAnimatorStateInfo(0).normalizedTime > time)
        {
            return true;
        }
        else return false;
    }

    //-----DISTANCIA----///
    public static float GetDistanceXZ(Vector3 v1, Vector3 v2)
    {
        float xDiff = v1.x - v2.x;
        float zDiff = v1.z - v2.z;
        return Mathf.Sqrt((xDiff * xDiff) + (zDiff * zDiff));
    }
}
