using UnityEngine;
using UnityEngine.UI;

public class FourthMiniGameBackgroundParallax : MonoBehaviour
{
    [System.Serializable]
    public class Layer {
        public RawImage image;
        public float speedMultiplier;
    }

    [SerializeField] private Layer[] layers;
    [SerializeField] private float friction = 0.98f; 
    [SerializeField] private float maxSpeed = 2f;  
    private float currentSpeed = 0f;

    void Update()
    {

        foreach (Layer layer in layers)
        {

            //muovo lo sfondo invece del giocatore
            float xOffset = layer.image.uvRect.position.x + (currentSpeed * layer.speedMultiplier * Time.deltaTime);
            layer.image.uvRect = new Rect(xOffset, layer.image.uvRect.position.y, layer.image.uvRect.size.x, layer.image.uvRect.size.y);
            
            
        }

        currentSpeed = Mathf.Lerp(currentSpeed, 0, (1 - friction) * Time.deltaTime * 50f);
    }

    public void AddForce(float amount)
    {
        currentSpeed += amount;
        currentSpeed = Mathf.Clamp(currentSpeed, 0, maxSpeed);
    }
    public float GetCurrentSpeed() 
    { 
        return currentSpeed; 
    }
}