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
    public float scale = 0.1f;
    public float seedX;
    public float seedY;
    public ThirdMiniGameTile[,] tileGrid;
    bool wait = false;

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
        TickRate();
        seedX = Random.Range(0f, 99999f);
        seedY = Random.Range(0f, 99999f);
        Wind();
        StartCoroutine(GenerateGrid());
        
        
    }
    
    private float GetNoiseValue(int x, int y) 
    {

        float xCoord = (float)x / (float)width * scale + seedX;
        float yCoord = (float)y / (float)height * scale + seedY;

        
        return Mathf.PerlinNoise(xCoord, yCoord);
    }

    private float GetMixedNoise(int x, int y) 
    {
        float perlin = GetNoiseValue(x, y);
        float random = Random.Range(-0.2f, 0.2f); 

        return Mathf.Clamp01(perlin + random);
    }
    void RandomBurn()
    {
        int startX = Random.Range(0, width);
        int startY = Random.Range(0, height);
        
        wildfireOrigin = new Vector2Int(startX, startY);
        
        tileGrid[startX, startY].currentState = ThirdMiniGameTile.State.BURNING;
        tileGrid[startX, startY].isPlayerFire = false; 
        crackle.PlayOneShot(thunder);
    }

    IEnumerator GenerateGrid()
    {
        wait = true;
        gridArray =  new GameObject[width, height];
        tileGrid = new ThirdMiniGameTile[width, height];
        RectTransform rectTransform = GetComponent<RectTransform>();

        float cellSize = rectTransform.rect.height / height;


        grid.cellSize = new Vector2(cellSize, cellSize);
        grid.constraintCount = width;
        yield return null;
        int num = 0;
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
                tileGrid[x,y].SetUp(GetMixedNoise(x,y));
                tileGrid[x,y].UpdateVisuals();
                num++;
                if(num>= 320)
                {
                    num=0;
                    yield return new WaitForSeconds(0.0000001f);
                }
                
            }
        }
        StartCoroutine(WaitAndCalculate());
        RandomBurn();
        crackle.Play();
        coroutine = StartCoroutine(TickRoutine());
        wait = false;
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
    private float tickRate;
    [SerializeField] private float maxTickRate = 0.23f;
    [SerializeField] private float minTickRate = 0.4f;
    private Coroutine coroutine;
    private bool isPaused = false;

    void Wind()
    {
        Vector2Int[] directions = 
        {
            new Vector2Int(0, 1),
            new Vector2Int(0, -1),
            new Vector2Int(1, 0),
            new Vector2Int(-1, 0)
        };

        windDirection = directions[Random.Range(0, directions.Length)];
    }

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
    [Header("Player Abilities")]
    [SerializeField] private float fireCooldown = 1.5f; 
    private float nextFireTime = 0f;



    Vector3[] gridCorners = new Vector3[4];
    float unit;
    private Vector2Int wildfireOrigin;
  


    
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

                    Dictionary<ThirdMiniGameTile, int> neighbors = GetNeighbors(x, y,4);
                    foreach (var n in neighbors)
                    {
                        ThirdMiniGameTile neighborTile = n.Key;
                        if (neighborTile.currentState == ThirdMiniGameTile.State.HEALTHY)
                        {
                            
                            
                            if (tile.isPlayerFire)
                            {

                                float currentDist = Vector2.Distance(new Vector2(x, y), wildfireOrigin);
                                float neighborDist = Vector2.Distance(new Vector2(neighborTile.x, neighborTile.y), wildfireOrigin);

                                if (neighborDist < currentDist-0.7f)
                                {
                                    
                                    neighborTile.currentHeat += (tile.heatOutput/  n.Value) * 1.3f; 
                                    if(neighborTile.currentState!=ThirdMiniGameTile.State.BURNING)neighborTile.isPlayerFire = true;
                                }

                                
                            }
                            else 
                            {
                                Vector2Int dirToN = new Vector2Int (n.Key.x,n.Key.y) - new Vector2Int (x, y) ;
                                int dir = ( dirToN ==  windDirection )? 1:0;
                                neighborTile.currentHeat += (tile.heatOutput/  n.Value) + (dir*windStrength);
                                neighborTile.isPlayerFire = false;
                            }
                        }
                    }
                }
            }
        }

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                ThirdMiniGameTile tile = tileGrid[x, y];

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
        CheckGameOver();
    }

    
    void StartPlayerFire(ThirdMiniGameTile tile)
    {
        tile.isPlayerFire = true;
        tile.currentState = ThirdMiniGameTile.State.BURNING;
        tile.currentHeat = tile.heatStartBurning; 
        
        PlayIgniteSound();
    }



    Vector3[] GetGridWorldCorners()
    {
        RectTransform firstRect = gridArray[0, 0].GetComponent<RectTransform>();
        Vector3[] corners = new Vector3[4];
        firstRect.GetWorldCorners(corners);
        Vector3 topLeft = corners[1];

        RectTransform lastRect = gridArray[width - 1, height - 1].GetComponent<RectTransform>();
        corners = new Vector3[4];
        lastRect.GetWorldCorners(corners);
        Vector3 bottomRight = corners[3]; 

        Vector3 topRight = new Vector3(bottomRight.x, topLeft.y, topLeft.z);
        Vector3 bottomLeft = new Vector3(topLeft.x, bottomRight.y, bottomRight.z);
        corners = new Vector3[]{bottomLeft,topLeft,topRight,bottomRight};
        return corners;
    }

    IEnumerator WaitAndCalculate()
    {
        yield return new WaitForEndOfFrame();
        
        CalculateBasicUnit(); 
    }
    void CalculateBasicUnit()
    {
        gridCorners = GetGridWorldCorners();

        unit = (gridCorners[3].x - gridCorners[0].x) / width;
    }



    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            if (Time.time >= nextFireTime&&!wait)
            {
                Vector3 mousePos = Input.mousePosition;
                mousePos.z = Mathf.Abs(Camera.main.transform.position.z); 
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePos);

                Vector3 position = worldPosition - gridCorners[0];

                int x = (int)(position.x / unit);
                int y = (int)(position.y / unit);

                y = (height - 1) - y;

                if(x >= 0 && x < width && y >= 0 && y < height)
                {
                    StartPlayerFire(tileGrid[x, y]);
                    tileGrid[x, y].UpdateVisuals();
                    nextFireTime = Time.time + fireCooldown;
                }
            }
            
            
        }
    }

    //__________________________________________________________________________________________-
    [Header("Results Menu")]
    [SerializeField] private GameObject resultsPanel;
    [SerializeField] private TMPro.TextMeshProUGUI statsText;

    void CheckGameOver()
    {
        int burningCount = 0;
        int healthyCount = 0;
        int burntCount = 0;
        bool onlyPlayerFire = true;

        foreach (var tile in tileGrid)
        {
            if (tile.currentState == ThirdMiniGameTile.State.BURNING)
            {
                burningCount++;
                if (!tile.isPlayerFire) onlyPlayerFire = false;
            } 
            else if (tile.currentState == ThirdMiniGameTile.State.HEALTHY) healthyCount++;
            else if (tile.currentState == ThirdMiniGameTile.State.BURNT) burntCount++;
        }
        UpdateFireAudio(burningCount);

        if (burningCount == 0 && !resultsPanel.activeSelf|| onlyPlayerFire)
        {
            ShowResults(healthyCount, burntCount);
            StartCoroutine(FadeAndStop(crackle,0.7f));
        }
    }

    void ShowResults(int healthy, int burnt)
    {
        resultsPanel.SetActive(true);
        
        float total = width * height;
        float percentSaved = (healthy / total) * 100f;

        statsText.text ="Foresta salvata:"+(int)percentSaved +"%" ;
    }
    void ClearExistingGrid()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        gridArray = null;
        tileGrid = null;
    }

    void TickRate()
    {
        float t = Mathf.Pow(Random.value, 0.5f);
        tickRate = Mathf.Lerp(minTickRate, maxTickRate, t);
    }
    public void Restart()
    {
        Wind();
        TickRate();
        resultsPanel.SetActive(false);
        seedX = Random.Range(0f, 99999f);
        seedY = Random.Range(0f, 99999f);
        StopCoroutine(coroutine);
        ClearExistingGrid();
        StartCoroutine(GenerateGrid());
    }

    //____________________________________________________________________________________________
    [SerializeField] private AudioSource crackle;
    [SerializeField] private float maxVolume = 1.0f;
    [SerializeField] private int tilesForMaxVolume = 500; 
    [SerializeField] private AudioClip ignite;
    [SerializeField] private AudioClip thunder;


    void UpdateFireAudio(int burningCount)
    {

        float targetVolume = Mathf.Clamp01((float)burningCount / tilesForMaxVolume);
        
  
        crackle.volume = Mathf.Lerp(crackle.volume, targetVolume * maxVolume, Time.deltaTime * 2f);
        

        crackle.pitch = Mathf.Lerp(0.8f, 1.2f, targetVolume);
    }

    public void PlayIgniteSound()
    {
        if(Random.Range(0,10) > 4) return;



        crackle.PlayOneShot(ignite);
    }

    IEnumerator FadeAndStop(AudioSource source, float duration)
    {
        float startVolume = source.volume;

        while (source.volume > 0)
        {
            source.volume -= startVolume * Time.deltaTime / duration;
            yield return null;
        }

        source.Stop();
        source.volume = startVolume;
    }
    



    
}
    

