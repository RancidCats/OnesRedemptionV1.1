using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    //[SerializeField] GameObject _player; //Referencio al player para poder emparentarlo en caso que desee

    [SerializeField] GameObject[] _waypoints; //Array de Waypoints

    int _currentPoint = 0; //Waypoint actual

    [SerializeField] float _speed = 1f; //Velocidad de la plataforma

    private void Update()
    {
        Movement(); //Llamo a la función Movement
    }

    //Función que controla el movimiento de la plataforma. En el if checkeo que la distancia entre la plataforma y el waypoint sea menor a 0.1 así avanza al siguiente.
    //El movimiento lo realizo utilizando MoveTowards con la posición actual y la objetivo.
    void Movement()
    {
        if (Vector3.Distance(transform.position, _waypoints[_currentPoint].transform.position) < 0.1f)
        {
            _currentPoint++;
            if (_currentPoint >= _waypoints.Length)
                _currentPoint = 0;
        }

        transform.position = Vector3.MoveTowards(transform.position, _waypoints[_currentPoint].transform.position, _speed * Time.deltaTime);
    }


    //Acá intenté emparentar el player a la plataforma para que se mueva con ella pero no pude lograr que se pueda mover arriba así que lo dejo comentado

    //public void OnTriggerEnter(Collider other)
    //{
    //    if(other.gameObject.name=="feets")
    //        _player.transform.parent = transform;
    //    //if(other.gameObject.name=="Player")
    //    //    other.gameObject.transform.SetParent(transform);
    //}
    //
    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject.name == "feets")
    //        _player.transform.parent = null;
    //    //if (other.gameObject.name == "Player")
    //    //    other.gameObject.transform.SetParent(null);
    //}
}
