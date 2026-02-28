using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private AnimationCurve curve;

    [Header("StandardMovement")]
    [SerializeField] private Transform targetPosition;
    [SerializeField] private float movementTime = 1.5f;
    [SerializeField] private float waitTime = 2.0f;
    private Vector3 startPos;
    private Vector3 endPos;
    private float duration;
    private float wait;
    private Coroutine activeFatherCoroutine;
    private Coroutine activeDaughterCoroutine;

    public void MoveCamera(Transform targetPos, float d, float w)
    {
        StopCoroutines();
        wait = w;
        duration = d;
        startPos = transform.position;
        endPos = targetPos.position;
        activeFatherCoroutine = StartCoroutine(MoveCoroutine());
    }

    public void MoveCamera()
    {
        StopCoroutines();
        wait = movementTime;
        duration = waitTime;
        startPos = transform.position;
        endPos = targetPosition.position;
        activeFatherCoroutine = StartCoroutine(MoveCoroutine());
    }

    private IEnumerator MoveCoroutine()
    {
        yield return activeDaughterCoroutine = StartCoroutine(Move(startPos, endPos, duration));
        yield return new WaitForSeconds(wait);
        yield return activeDaughterCoroutine = StartCoroutine(Move(endPos, startPos, duration));
    }

    private IEnumerator Move(Vector3 start, Vector3 end, float time)
    {
        float timer = 0;
        while (timer < time)
        {
            // Lerp ti da una posizione in base al terzo coso(da 0 a 1), tra due punti A e B
            transform.position = Vector3.Lerp (start, end, curve.Evaluate(timer / time)); //Evaluate cambia la retta nella forma della curva da 0 a 1
            timer += Time.deltaTime;
            yield return null;
        }
    }

    private void StopCoroutines()
    {
        if (activeFatherCoroutine != null) StopCoroutine(activeFatherCoroutine);

        if (activeDaughterCoroutine != null) StopCoroutine(activeDaughterCoroutine);
    }
}
