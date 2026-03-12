using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class SecondMiniGameDropZone : MonoBehaviour, IDropHandler
{
    [HideInInspector] public GameObject droppedObject;
    [HideInInspector] public SecondMiniGame manager;
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
            manager.ChangeObject(droppedObject, index, dragScript.oldIndex);
            dragScript.oldIndex = index;
        }
    }
}