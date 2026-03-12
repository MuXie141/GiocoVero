using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.UI;

public class SecondMiniGameObject : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector3 startPosition;
    private Image image;
    public bool isDropped = false;
    [HideInInspector] public int? oldIndex = null;

    void Start() 
    {
        image = GetComponent<Image>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
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
}
