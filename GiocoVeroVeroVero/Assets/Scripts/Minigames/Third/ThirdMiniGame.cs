using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ThirdMiniGame: MonoBehaviour
{
    [Header("Grid Settings")]
    [SerializeField] private GameObject cellPrefab; 
    [SerializeField] private GridLayoutGroup grid;
    [SerializeField] private int width = 20;
    [SerializeField] private int height = 20;
    public GameObject[,] gridArray;
    public ThirdMiniGameTile[,] tileGrid;

    //Vector2Int è Vector2 con Int, no float
    private Vector2Int[] directions = new Vector2Int[]
    {
        new Vector2Int(0, -1), 
        new Vector2Int(0, 1),  
        new Vector2Int(1, 0),  
        new Vector2Int(-1, 0)  
    };

    void Start()
    {
        GenerateGrid();
        StartCoroutine(TickRoutine());
    }

    void GenerateGrid()
    {
        gridArray =  new GameObject[width, height];
        tileGrid = new ThirdMiniGameTile[width, height];
        RectTransform rectTransform = GetComponent<RectTransform>();

        float cellSize = rectTransform.rect.height / height;


        grid.cellSize = new Vector2(cellSize, cellSize);
        grid.constraintCount = width;

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                GameObject newCell = Instantiate(cellPrefab, transform);
                newCell.name = "Cell_"+x+"_"+y;
                tileGrid[x,y] = newCell.GetComponent<ThirdMiniGameTile>();
                tileGrid[x,y].x = x;
                tileGrid[x,y].y = y;
                gridArray[x, y] = newCell;
            }
        }
    }

    public Dictionary<ThirdMiniGameTile, int> GetNeighbors(int centerX, int centerY, int radius)
    {
        Dictionary<ThirdMiniGameTile, int> neighbors = new Dictionary<ThirdMiniGameTile, int>();
        //cerco quadrato
        for (int x = centerX - radius; x <= centerX + radius; x++)
        {
            for (int y = centerY - radius; y <= centerY + radius; y++)
            {
                //salto centro
                if (x == centerX && y == centerY) continue;
                //cerco e controllo non vado fuori
                if (x >= 0 && x < width && y >= 0 && y < height)
                {
                    //Cerchio
                    float dist = Vector2Int.Distance(new Vector2Int(centerX, centerY), new Vector2Int(x, y));
                    if (dist <= radius)
                    {
                        neighbors.Add(tileGrid[x, y], Mathf.CeilToInt(dist));
                    }
                }
            }
        }
        return neighbors;
    }
            



                
    //-____________________________________________________________________________________

    [Header("Tick Settings")]
    [SerializeField] private float tickRate = 0.5f;
    private bool isPaused = false;
    private Coroutine coroutine;

    IEnumerator TickRoutine()
    {
        while (true)
        {
            if (!isPaused)
            {
                Tick();
            }
            yield return new WaitForSeconds(tickRate);
        }
    }

    
    [Header("Per Tick")]
    [SerializeField] private float fuelValue_Lost = 10f;
    [SerializeField] private float heatToApply = 20f;
    [Header("Wind Settings")]
    [SerializeField] private Vector2Int windDirection = new Vector2Int(0, 1); 
    [SerializeField] private float windStrength = 15f;

    
    void Tick()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                ThirdMiniGameTile tile = tileGrid[x,y];

                if (tile.currentState == ThirdMiniGameTile.State.BURNING)
                {
                    tile.fuelValue -= fuelValue_Lost;

                    Dictionary<ThirdMiniGameTile, int> neighbors = GetNeighbors(x, y,3);
                    foreach (var n in neighbors)
                    {
                        ThirdMiniGameTile neighborTile = n.Key;
                        if (neighborTile.currentState == ThirdMiniGameTile.State.HEALTHY)
                        {
                            Vector2Int dirToN = new Vector2Int (n.Key.x,n.Key.y) - new Vector2Int (x, y) ;
                            int dir = ( dirToN ==  windDirection )? 1:0;
                            // In base alla distanza meno heat
                            neighborTile.currentHeat += (heatToApply/  n.Value) + (dir*windStrength);
                        }
                    }
                }
            }
        }

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                ThirdMiniGameTile tile = gridArray[x, y].GetComponent<ThirdMiniGameTile>();

                if (tile.currentState == ThirdMiniGameTile.State.HEALTHY && tile.currentHeat >= tile.heatStartBurning)
                {
                    tile.currentState = ThirdMiniGameTile.State.BURNING;
                }

                if (tile.currentState == ThirdMiniGameTile.State.BURNING && tile.fuelValue <= 0)
                {
                    tile.currentState = ThirdMiniGameTile.State.BURNT;
                }

                tile.UpdateVisuals();
            }
        }
    }
}
    

