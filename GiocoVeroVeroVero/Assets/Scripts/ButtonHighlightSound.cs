using UnityEngine;
using UnityEngine.EventSystems; 
public class ButtonHighlightSound : MonoBehaviour, IPointerEnterHandler
{
    
    [SerializeField] private AudioClip selectedClip;

    public void OnPointerEnter(PointerEventData eventData)
    {

        SoundFXManager.Instance.PlayAudio(selectedClip);
    }
    
}
