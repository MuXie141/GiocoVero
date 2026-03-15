using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System.Collections;

public class SecondMiniGameDropZone : MonoBehaviour, IDropHandler
{
    [HideInInspector] public GameObject droppedObject;
    [HideInInspector] public int index;
    


    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null) 
        {
            droppedObject = eventData.pointerDrag;
            SecondMiniGameObject dragScript = droppedObject.GetComponent<SecondMiniGameObject>();
            dragScript.isDropped = true; 
            dragScript.transform.position = transform.position;
            droppedObject.transform.SetParent(transform); 
            SecondMiniGame.Instance.PlayTapAudio();
            SecondMiniGame.Instance.ChangeObject(droppedObject, index, dragScript.oldIndex);
            dragScript.oldIndex = index;
        }
    }

    public void ShowError()
    {
        StartCoroutine(ShakeObject(0.5f, 2.5f)); 
        GetComponent<Image>().color = Color.red;
        Invoke("ResetColor", 0.5f);
    }

    void ResetColor() 
    {
        GetComponent<Image>().color = Color.white;
    }

    IEnumerator ShakeObject(float duration, float magnitude)
    {
        Vector3 originalPos = transform.localPosition;
        float timer = 0.0f;

        while (timer < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;


            transform.localPosition = new Vector3(originalPos.x + x, originalPos.y, originalPos.z);
            timer += Time.deltaTime;

            yield return null; 
        }

        transform.localPosition = originalPos; 
    }
}