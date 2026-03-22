using UnityEngine;
using UnityEngine.UI;

public class ThirdMiniGameTile : MonoBehaviour
{
    public enum State{BURNT,BURNING,HEALTHY}
    public enum PlantType { GRASS, BUSH, TREE }
    public float fuelValue = 100f;
    public float currentHeat = 0f;
    public State currentState = State.HEALTHY;
    public PlantType plantType;
    public float heatStartBurning = 50f;
    public float heatOutput;
    public int x;
    public int y;
    public bool isPlayerFire = false;
    public bool nearBigFire = false;
    private float maxFuel;

    private Image img;

    void Awake()
    {
        img = GetComponent<Image>();
        img.color = Color.green;    
    } 

    public void UpdateVisuals()
    {
        if (currentState == State.BURNING)
        {
            Color baseBurnColor = Color.white; 

            switch (plantType)
            {
                case PlantType.GRASS:
                    baseBurnColor = new Color(1f, 0.6f, 0.0f); 
                    break;
                case PlantType.BUSH:
                    baseBurnColor = new Color(1f, 0.3f, 0.0f);
                    break;
                case PlantType.TREE:
                    baseBurnColor = Color.red; 
                    break;
            }

            float fuelPercent = fuelValue / maxFuel; 

            img.color = Color.Lerp(Color.black, baseBurnColor, fuelPercent);
        }
        else if (currentState == State.BURNT)
        {
            img.color = Color.black;
        }
        else // HEALTHY
        {
            switch (plantType)
            {
                case PlantType.GRASS:
                    img.color = new Color(0.0f, 0.902f, 0.302f); 
                    break;
                case PlantType.BUSH:
                    img.color = new Color(0.0f, 0.702f, 0.235f);
                    break;
                case PlantType.TREE:
                    img.color = new Color(0.0f, 0.501f, 0.168f); 
                    break;
            }
        }
    }

    public void SetUp(float noiseValue)
    {
        currentState = State.HEALTHY;
        if (noiseValue < 0.30f)
        {
            plantType = PlantType.GRASS;
            fuelValue = 15f;     
            maxFuel = fuelValue;  
            heatOutput = 20f;         
            heatStartBurning = 15f;   
        }
        else if (noiseValue < 0.80f) 
        {
            plantType = PlantType.TREE;
            fuelValue = 90f;         
            maxFuel = fuelValue;  
            heatOutput = 15f;        
            heatStartBurning = 150f;  
        }
        else 
        {
            plantType = PlantType.BUSH;
            fuelValue = 60f; 
            maxFuel = fuelValue;  
            heatOutput = 10f;  
            heatStartBurning = 50f; 
        }
    }
    
}
