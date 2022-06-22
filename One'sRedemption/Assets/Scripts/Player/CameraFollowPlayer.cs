using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    public static CameraFollowPlayer instance;
    public Transform target;
    [Tooltip("Posicion relativa de la camara con el personaje.")]
    public Vector3 offset;
    public float smoothSpeed;
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }
    void FixedUpdate()
    {
        if (target)
        {
            Vector3 desiredPos = target.position + offset;
            Vector3 smoothedPos = Vector3.Lerp(transform.position, desiredPos, smoothSpeed * Time.deltaTime);

            transform.position = smoothedPos;
        }
    }

    public void CameraShake(float desiredDuration, float desiredMagnitude)
    {
        StartCoroutine(CameraShakeCoroutine(desiredDuration, desiredMagnitude));
    }

    IEnumerator CameraShakeCoroutine(float duration, float magnitude)
    {
        Vector3 originalPos = transform.localPosition;

        float elapsed = 0.0f;

        while(elapsed < duration)
        {
            float x = Random.Range(-1, 1f) * magnitude;
            float y = Random.Range(-1, 1f) * magnitude;

            transform.localPosition = new Vector3(originalPos.x + x, originalPos.y + y, originalPos.z);

            elapsed += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }

    }
}
