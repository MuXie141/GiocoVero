using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPos = transform.localPosition;
        float timer = 0.0f;

        while (timer < duration)
        {
            float y = Random.Range(-1f, 1f) * magnitude;
            Vector3 targetPos = new Vector3(originalPos.x , originalPos.y + y, originalPos.z);
            transform.localPosition = Vector3.Lerp(transform.localPosition, targetPos, 15f * Time.deltaTime);
            timer += Time.deltaTime;
            yield return null; 
        }

        timer = 0;
        while (timer < 0.1f)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, originalPos, timer / 0.1f);
            timer += Time.deltaTime;
            yield return null;
        }
        
        transform.localPosition = originalPos;
    }
}