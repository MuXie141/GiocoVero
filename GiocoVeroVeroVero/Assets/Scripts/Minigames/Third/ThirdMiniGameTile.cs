using UnityEngine;
using UnityEngine.UI;

public class ThirdMiniGameTile : MonoBehaviour
{
    public enum State{BURNT,BURNING,HEALTHY}
    public float fuelValue = 100f;
    public float currentHeat = 0f;
    public State currentState = State.HEALTHY;
    public float heatStartBurning = 50f;
    public int x;
    public int y;

    private Image img;

    void Awake()
    {
        img = GetComponent<Image>();
        img.color = Color.green;    
    } 

    public void UpdateVisuals()
    {
        if (currentState == State.BURNING) img.color = Color.red;
        else if (currentState == State.BURNT) img.color = Color.black;
        else img.color = Color.green;
    }
    
}
