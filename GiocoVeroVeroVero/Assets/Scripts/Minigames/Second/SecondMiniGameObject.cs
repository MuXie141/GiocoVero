using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Collections;

public class SecondMiniGameObject : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector3 startPosition;
    private Image image;
    public bool isDropped = false;
    public int correctPosition;
    [HideInInspector] public int? oldIndex = null;

    void Awake() 
    {
        image = GetComponent<Image>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        SecondMiniGame.Instance.PlayPopAudio();
        startPosition = transform.position;
        image.raycastTarget = false;
        isDropped = false;
    }

    public void OnDrag(PointerEventData eventData) 
    {
        Vector3 mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = Mathf.Abs(Camera.main.transform.position.z);
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
        transform.position = mouseWorldPos;
    }

    public void OnEndDrag(PointerEventData eventData) 
    {
        image.raycastTarget = true;
        if (!isDropped) 
        {
            transform.position = startPosition;
        }
    }

    public void ShowError()
    {
        StartCoroutine(ShakeObject(0.5f, 2.5f)); 
    }

    IEnumerator ShakeObject(float duration, float magnitude)
    {
        Vector3 originalPos = transform.localPosition;
        float timer = 0.0f;

        while (timer < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(originalPos.x + x, originalPos.y + y, originalPos.z);
            timer += Time.deltaTime;

            yield return null; 
        }

        transform.localPosition = originalPos; 
    }

    public IEnumerator FadeIn(float duration)
    {
        Color startColor = image.color;
        startColor.a = 0f;
        image.color = startColor;

        float timer = 0f;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            float newAlpha = Mathf.Lerp(0f, 1f, timer / duration);
            
            Color newColor = image.color;
            newColor.a = newAlpha;
            image.color = newColor;

            yield return null; 
        }

        Color finalColor = image.color;
        finalColor.a = 1f;
        image.color = finalColor;
    }
}
