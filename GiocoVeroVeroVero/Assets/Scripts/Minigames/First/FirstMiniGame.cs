using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Collections;
public class FirstMiniGame : MonoBehaviour
{
    public TextAsset textAssetData;
    //[SerializeField] private Keycode[] keycodes;
    public enum WasteType{PLASTICA=0, CARTA=1, UMIDO=2, SECCO=3, VETRO=4, ALTRO=5}
    [Serializable]
    public struct Waste
    {
        public string name;
        public WasteType wrong;
        public WasteType correct;
        public string note;
    }
    
    public List<Waste> wastes = new List<Waste>();

    void ReadCSV()
    {
        string[] data = textAssetData.text.Split(new string[] {"\n"}, StringSplitOptions.None);

        for (int i = 1; i < data.Length; i++)
        {
            Waste newWaste = new Waste();
            string[] row = data[i].Split(new string[] {","}, StringSplitOptions.None);
            newWaste.name = row[0];
            switch(row[1])
            {   
                case "SECCO":
                    newWaste.wrong = WasteType.SECCO;
                    break;
                case "ALTRO":
                    newWaste.wrong = WasteType.ALTRO;
                    break;
                case "PLASTICA":
                    newWaste.wrong = WasteType.PLASTICA;
                    break;
                case "UMIDO":
                    newWaste.wrong = WasteType.UMIDO;
                    break;    
                case "CARTA":
                    newWaste.wrong = WasteType.CARTA;
                    break;
                case "VETRO":
                    newWaste.wrong = WasteType.VETRO;
                    break;
            }
            switch(row[2])
            {   
                case "SECCO":
                    newWaste.correct = WasteType.SECCO;
                    break;
                case "ALTRO":
                    newWaste.correct = WasteType.ALTRO;
                    break;
                case "PLASTICA":
                    newWaste.correct = WasteType.PLASTICA;
                    break;
                case "UMIDO":
                    newWaste.correct = WasteType.UMIDO;
                    break;    
                case "CARTA":
                    newWaste.correct = WasteType.CARTA;
                    break;
                case "VETRO":
                    newWaste.correct = WasteType.VETRO;
                    break;
            }
            newWaste.note = row[3];
            wastes.Add(newWaste);
        }
    }

    
    //UI-Animation
    [SerializeField] private TMPro.TextMeshProUGUI textMeshPro;
    [SerializeField] private TMPro.TextMeshProUGUI note;
    [SerializeField] private Image bin1;
    [SerializeField] private Image bin2;
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private Animator animator;
    //Game
    public static FirstMiniGame Instance;
    [SerializeField] private int wastesAmount;
    [SerializeField] private KeyCode[] keyCodes;
    [SerializeField] private WasteType bin1Type;
    [SerializeField] private WasteType bin2Type;
    [SerializeField] private Waste currentWaste;
    [SerializeField] private GameObject ResultsMenu;
    [SerializeField] private TMPro.TextMeshProUGUI PointsText;
    [SerializeField] private TMPro.TextMeshProUGUI MissText;
    [SerializeField] private TMPro.TextMeshProUGUI EarlyText;
    [SerializeField] private TMPro.TextMeshProUGUI AccuracyText;
    [SerializeField] private TMPro.TextMeshProUGUI WrongsText;
    

    private int currentWastesNumber;
    private int correct;
    private int misses;
    private int wrongs;
    private int early ;
    public bool pressed = false;
    private bool done = false;

    [Header("Messaggi Successo")]
    [SerializeField] private string[] positiveMessages;
    void OnEnable()
    {
        ResultsMenu.SetActive(false);
        ReadCSV();
        Instance = this;
        if(done)
        {
          
            currentWastesNumber= 0;
            correct= 0;
            misses = 0;
            wrongs = 0;
            early = 0;
            NewWaste();
        }
        else
        {
            done = true;
            StartCoroutine(WaitSeconds());
        }
        
    }

    IEnumerator WaitSeconds()
    {
        textMeshPro.text = "2 Prove Ascolto";
        animator.Play("PrimoMinigioco2", 0, 0f);
        yield return new WaitForSeconds(10f);
        textMeshPro.text = "Go!";
        yield return new WaitForSeconds(2f);
        
        currentWastesNumber= 0;
        correct= 0;
        misses = 0;
        wrongs = 0;
        early = 0;
        NewWaste();
    }



   
    
  

    void UpdateScene(string name,WasteType type1, WasteType type2)
    {
        textMeshPro.text = name;
        int randomInt = UnityEngine.Random.Range(0,2);
        switch(randomInt)
        {
            case 0:
                bin1.sprite = sprites[(int)type1];
                bin2.sprite = sprites[(int)type2];
                bin1Type = type1;
                bin2Type = type2;
                break;
            case 1:
                bin1.sprite = sprites[(int)type2];
                bin2.sprite = sprites[(int)type1];
                bin1Type = type2;
                bin2Type = type1;
                break;
        }

        
    }

    void NewWaste()
    {
        
        if(currentWastesNumber>=wastesAmount)
        {
           ShowResults();
           return;  
        }
        doNotMiss = false;
        currentWastesNumber++;
        pressed = false;
        int randomIndex = UnityEngine.Random.Range(0,wastes.Count);
        currentWaste = wastes[randomIndex];
        if(currentWaste.wrong!=WasteType.ALTRO && currentWaste.correct!= WasteType.ALTRO)
        {
            UpdateScene(currentWaste.name,currentWaste.wrong , currentWaste.correct);
        }
        else
        {
            
            WasteType wasteType;
            do 
            {
                wasteType = (WasteType)UnityEngine.Random.Range(0, 5);
            } while(wasteType == currentWaste.correct || wasteType == currentWaste.wrong);

            if(currentWaste.correct == WasteType.ALTRO)
            {
                UpdateScene(currentWaste.name,currentWaste.wrong , wasteType);
            }
            else
            {
                UpdateScene(currentWaste.name , currentWaste.correct,wasteType);
            }
        }
        
        animator.Play("PrimoMiniGioco1", 0, 0f);
        
    }

    IEnumerator NextWaste()
    {
        yield return null;
        while(FirstMiniGame2.Instance.isAnimating)yield return null;
        NewWaste();
    }

    

    void Update()
    {

        if (FirstMiniGame2.Instance.canInput)
        {
            if(FirstMiniGame2.Instance.early)
            {
                if(Input.GetKeyDown(keyCodes[0])||Input.GetKeyDown(keyCodes[1])||Input.GetKeyDown(keyCodes[2]))
                {
                    FirstMiniGame2.Instance.canInput = false;
                    Early();
                }
                return;
            }
            
            if (Input.GetKeyDown(keyCodes[0])) CheckInput(bin1Type);
            else if (Input.GetKeyDown(keyCodes[1])) CheckInput(bin2Type);
            else if (Input.GetKeyDown(keyCodes[2])) CheckInput(WasteType.ALTRO);
        }
    }

    void CheckInput(WasteType wasteType)
    {
        doNotMiss = true;
        pressed = true;
        FirstMiniGame2.Instance.canInput = false;
        
        if(wasteType== currentWaste.correct)
        {
            FirstMiniGame2.Instance.PlayCorrectAudio();
            correct++;
            StartCoroutine(ShowNote(positiveMessages[UnityEngine.Random.Range(0,positiveMessages.Length)], Color.green));
            
        }
        else
        {
            FirstMiniGame2.Instance.PlayWrongAudio();
            wrongs++;
            StartCoroutine(ShowNote(currentWaste.note, Color.red));
        }

        StartCoroutine(NextWaste());
    }

    IEnumerator ShowNote(string text, Color color)
    {
        note.gameObject.SetActive(true);
        note.text = text;
        note.color =  color;
        float timer = 0;
        note.transform.localScale = Vector3.one * 1.5f; 
        while (timer < 1.5f)
        {
            timer += Time.deltaTime;
            note.color = new Color(        note.color.r, note.color.g, note.color.b,        Mathf.Lerp(1f, 0f, timer / 1.5f)    );
            note.transform.localScale =  Vector3.one * Mathf.Lerp(1.5f, 1.0f, timer / 1.5f);
            yield return null; 
        }
        note.gameObject.SetActive(false);
    }
    bool doNotMiss;
    public void Miss()
    {
        if(doNotMiss) return;
        misses++;
        FirstMiniGame2.Instance.PlayMissAudio();
        StartCoroutine(ShowNote("Miss", Color.grey));
        StartCoroutine(NextWaste());
    }

    void Early()
    {
        doNotMiss = true;
        FirstMiniGame2.Instance.canInput = false;
        early++;
        FirstMiniGame2.Instance.PlayMissAudio();
        StartCoroutine(ShowNote("Troppo presto", Color.grey));
        StartCoroutine(NextWaste());
    }

    void ShowResults()
    {
        ResultsMenu.SetActive(true);
        PointsText.text = "Punteggio totale:\t"+correct;
        WrongsText.text = "Sbagliati:\t"+wrongs;
        MissText.text = "Saltati:\t"+misses;
        EarlyText.text = "In anticipo:\t"+early;
        AccuracyText.text = "Precisione:\t"+(float)correct/wastesAmount*100+"%";
        
    }

    public void SetActiveFalse()
    {
        gameObject.SetActive(false);
    }
    
}


