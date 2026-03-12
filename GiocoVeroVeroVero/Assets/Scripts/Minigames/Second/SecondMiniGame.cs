using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class SecondMiniGame : MonoBehaviour
{
    [SerializeField] private GameObject dropZonePrefab;
    [SerializeField] private Sprite[] sprites;
    private List<GameObject> listDropZone = new List<GameObject>();
    private GameObject[] gameObjectArray;
    void Start()
    {
        CreateDropZone();
    }
    void CreateDropZone()
    {
        for (int i = 0; i < sprites.Length; i++)
        {
            GameObject newObject = Instantiate(dropZonePrefab, transform);
            listDropZone.Add(newObject);
            SecondMiniGameDropZone script = newObject.GetComponent<SecondMiniGameDropZone>();
            script.manager = this;
            script.index = i;
        }
        gameObjectArray = new GameObject[sprites.Length];
    }

  
    public void ChangeObject(GameObject gameObject, int index, int? oldIndex)
    {
        GameObject oldObject = gameObjectArray[index];
        gameObjectArray[index] = gameObject;
        if (oldIndex!= null)
        {
            gameObjectArray[(int)oldIndex] = oldObject;
            SecondMiniGameDropZone oldZone = listDropZone[(int)oldIndex].GetComponent<SecondMiniGameDropZone>();
            // Se la zona di destinazione era vuota, ora la vecchia zona è vuota
            if (oldObject != null) 
            {
                //metto l'oggetto cacciato nella zona liberata
                oldObject.transform.position =oldZone.transform.position;
                oldObject.transform.SetParent(oldZone.transform);

                //Quando aggiorni qualcosa devi aggiornare anche le cose a esso related
                SecondMiniGameObject tempScript = oldObject.GetComponent<SecondMiniGameObject>();
                tempScript.oldIndex = oldIndex;
                oldZone.droppedObject =oldObject;
            }
            else 
            {
                oldZone.droppedObject = null;
            }
        }  
    }
}
