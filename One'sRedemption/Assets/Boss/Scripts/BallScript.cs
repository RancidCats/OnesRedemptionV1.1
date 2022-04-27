using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    Vector3 _gravity;
    Vector3 _speed;
    Vector3 _currentAngle;
    float _gravY;
    float _dTime;

    public void Initial(Vector3 speed, float gravity)
    {
        _speed = speed;
        _gravY = gravity;

        _gravity = Vector3.zero;
    }

    private void FixedUpdate()
    {
        _dTime += Time.fixedDeltaTime;
        _gravity.y = _gravY * _dTime;

        transform.position += (_speed + _gravity) * Time.fixedDeltaTime;
        _currentAngle.x = -Mathf.Atan((_speed.y + _gravity.y) / _speed.z) * Mathf.Rad2Deg;

        transform.eulerAngles = _currentAngle;
    }
}
