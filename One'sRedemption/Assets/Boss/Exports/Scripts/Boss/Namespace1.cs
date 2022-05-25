using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilidades
{
    public static class Aoe
    {
        public static void CreateRay(int rayType) //testing
        {
            switch (rayType)
            {
                case 0:
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast(ray, out RaycastHit hit))
                    {
                        CreateAreaOfEffect(hit.point, 2);
                    }
                    break;
                case 1:

                    break;
            }

        }

        public static Vector3 GetYPointFromTransform(Transform transform) // desde el transform, mando en Y un raycast para sacar el vector producto de la colision con la plataforma
        {
            Vector3 hitPoint = new Vector3(); // creo un vector3 vacio para guardar la posicion del hit.point
            Ray ray = new Ray(transform.position, -Vector3.up); //creo un ray nuevo, desde el parametro 1, en direccion hacia abajo
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, LayerMask.GetMask("Platform")))
            {
                if (hit.collider.gameObject.CompareTag("Floor"))
                {
                    hitPoint = hit.point; //guardo el vector de colision dentro la variable anterior
                }
                else
                {
                    throw new System.Exception("No hizo colision el ray contra objeto con tag 'Floor'"); //error de excepcion por si el tag del gameobject collisionado no es "Floor"
                }
            }
            return hitPoint;
        }

        public static GameObject CreateAreaOfEffect(Vector3 position, int type, Quaternion rotation)// crear una area de efecto en base a una posicion y un tipo.
        {
            string path = "";
            switch (type)
            {
                case 0:
                    path = "Prefab/AtaquesEnemigos/AreasDeEfecto/Aoe50";
                    break;
                case 1:
                    path = "Prefab/AtaquesEnemigos/AreasDeEfecto/Aoe270";
                    break;
                case 2:
                    path = "Prefab/AtaquesEnemigos/AreasDeEfecto/Aoe360";
                    break;
            }
            var asset2 = Resources.Load(path); //Resources es una carpeta dentro de Assets, load lo carga en memoria sin instanciarlo en la escena, para posterior uso.
            GameObject aoeObject2 = GameObject.Instantiate(asset2 as GameObject, position, rotation);
            //aoeObject2.transform.LookAt(-parent.position); //posibles errores
            return aoeObject2;
        }
        public static GameObject CreateAreaOfEffect(Vector3 position, int type) // crear una area de efecto en base a una posicion y un tipo.
        {
            string path = ""; //creo la ruta que tiene que tomar la funcion Resources.Load()
            switch (type)
            {
                case 0:
                    path = "Prefab/AtaquesEnemigos/AreasDeEfecto/Aoe50"; // area de efecto de 50 grados
                    break;
                case 1:
                    path = "Prefab/AtaquesEnemigos/AreasDeEfecto/Aoe270";// area de efecto de 270 grados
                    break;
                case 2:
                    path = "Prefab/AtaquesEnemigos/AreasDeEfecto/Aoe360";// area de efecto de 360 grados
                    break;
            }
            var asset = Resources.Load(path); //Resources es una carpeta dentro de Assets, load lo carga en memoria sin instanciarlo en la escena, para posterior uso.
            GameObject aoeObject = GameObject.Instantiate(asset as GameObject, position, Quaternion.identity); // as = forma de castear, convertir de un tipo al otro. Transformo Object en GameObject

            return aoeObject;
        }
        public static GameObject CreateAoeCollider(Vector3 position, int type, Quaternion rotation) // crear una area de efecto con collider en base a una posicion y un tipo.
        {
            string path = "";
            switch (type)
            {
                case 0:
                    path = "Prefab/AtaquesEnemigos/AoeColliders/AoeCollider50";
                    break;
                case 1:
                    path = "Prefab/AtaquesEnemigos/AoeColliders/AoeCollider270";
                    break;
                case 2:
                    path = "Prefab/AtaquesEnemigos/AoeColliders/AoeCollider360";
                    break;
            }
            var asset = Resources.Load(path); //Resources es una carpeta dentro de Assets, load lo carga en memoria sin instanciarlo en la escena, para posterior uso.
            GameObject aoeObject = GameObject.Instantiate(asset as GameObject, position, rotation); // rotacion relativa al parent
                                                                                                    // aoeObject.tag = "BossAttackCollider"; // para el ontriggerenter del player
            return aoeObject;
        }
        public static GameObject CreateAoeCollider(Vector3 position, int type) // crear una area de efecto con collider en base a una posicion y un tipo.
        {
            string path = "";
            switch (type)
            {
                case 0:
                    path = "Prefab/AtaquesEnemigos/AoeColliders/AoeCollider50";
                    break;
                case 1:
                    path = "Prefabs/AtaquesEnemigos/AoeColliders/AoeCollider270";
                    break;
                case 2:
                    path = "Prefab/AtaquesEnemigos/AoeColliders/AoeCollider360";
                    break;
            }
            var asset = Resources.Load(path); //Resources es una carpeta dentro de Assets, load lo carga en memoria sin instanciarlo en la escena, para posterior uso.
            GameObject aoeObject = GameObject.Instantiate(asset as GameObject, position, Quaternion.identity);
            // aoeObject.tag = "BossAttackCollider"; // para el ontriggerenter del player
            return aoeObject;
        }
        //crear el area de efecto, rotarla acorde
    }

    public static class Sistemas
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

}
