using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walking
{
   public void Walk(Vector3 inputVector, Transform _transform,float _smoothTime,float _smoothVelocity, float _moveSpeed, Rigidbody _rb)
    {
        Vector3 dir = new Vector3(inputVector.x, 0, inputVector.z).normalized;
        float targetAngle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
        float angle = Mathf.SmoothDampAngle(_transform.eulerAngles.y, targetAngle, ref _smoothVelocity, _smoothTime);
        _transform.rotation = Quaternion.Euler(0, angle, 0);
        _rb.MovePosition(_rb.position + dir * _moveSpeed * Time.deltaTime);
    }
}
