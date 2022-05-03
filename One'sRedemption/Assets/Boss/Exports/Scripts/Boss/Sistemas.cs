using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sistemas
{

    public static bool IsAnimationPlaying(Animator ani, string stateName)
    {
        if (ani.GetCurrentAnimatorStateInfo(0).IsName(stateName))//si el estado tiene este nombre, devolver verdadero, y viceversa.
        {
            return true;
        }
        else return false;
    }
    public static bool IsAnimationPlaying(Animator ani, string stateName, int time)
    {
        if (ani.GetCurrentAnimatorStateInfo(0).IsName(stateName) && ani.GetCurrentAnimatorStateInfo(0).normalizedTime > time) //si el estado tiene este nombre, y si paso este tiempo (normalizado) devolver verdadero, y viceversa
        {
            return true;
        }
        else return false;
    }

    //-----DISTANCIA----///
    public static float GetDistanceXZ(Vector3 v1, Vector3 v2) //Distancia sin tener en cuenta el eje Y, raiz cuadrada de suma de cuadrado de vectores
    {
        float xDiff = v1.x - v2.x;
        float zDiff = v1.z - v2.z;
        return Mathf.Sqrt((xDiff * xDiff) + (zDiff * zDiff));
    }
}
