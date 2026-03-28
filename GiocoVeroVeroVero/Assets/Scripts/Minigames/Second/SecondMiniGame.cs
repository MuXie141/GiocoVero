using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System.Collections;
using System;

public class SecondMiniGame : MonoBehaviour
{
    public static SecondMiniGame Instance;
    [SerializeField] private GameObject dropZonePrefab;
    [SerializeField] private GameObject objectPrefab;
    [SerializeField] private GameObject objectsZone;
    [Serializable]
    public class RoundSprites
    {
        public Sprite[] roundSprites;
    }

    [SerializeField] private RoundSprites[] sprites;
    [SerializeField] private Sprite[] currentArray;
    private int round = 0;
    private int wrongs;
    private List<GameObject> listDropZone = new List<GameObject>();
    private List<GameObject> sequenceObject= new List<GameObject>(); 
    private GameObject[] playerSequence;

    [Header("Effects")]
    [SerializeField] private AudioClip pop;
    [SerializeField] private AudioClip tap;
    [SerializeField] private AudioClip clave;
    [SerializeField] private AudioClip wrong;
    [SerializeField] private AudioClip swoosh;
    [SerializeField] private AudioClip yay;
    [SerializeField] private ParticleSystem confetti1;
    [SerializeField] private ParticleSystem confetti2;

    [Header("Time")]
    [SerializeField] private float phase1;
    [SerializeField] private float phase2;
    [SerializeField] private Slider timerSlider;
    [SerializeField] private Image fillImage;
    [SerializeField] private Color red;
    [SerializeField] private Color green;
    private float timer;
    private bool ActiveTimer;
    private float timeMax;
    private Coroutine coroutine;

    
    private void OnEnable()
    {
        timerSlider.value = 1;
        ResultsMenu.SetActive(false);
        wrongs=0;
        currentArray = sprites[round].roundSprites;
        StartCoroutine(CreateObjects());
    }
    private void Awake() 
    {
        Instance = this;
    }

    private void Update() 
    {
        if (ActiveTimer)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
                timerSlider.value = timer;
                Color coloreTarget = (timer < timeMax * 0.3f) ? red : green;
                fillImage.color = Color.Lerp(fillImage.color, coloreTarget, Time.deltaTime * 5f);
            }
            else
            {
                timer = 0;
                ActiveTimer = false;
                coroutine = StartCoroutine(StartFill());
                
            }
        }
    }

    IEnumerator StartFill()
    {
        float elapsed = 0f;

        while (elapsed < 1f)
        {
            elapsed += Time.deltaTime;
            
            timerSlider.value = Mathf.Lerp(0, timerSlider.maxValue, elapsed / 1f);

            Color coloreTarget = (elapsed < 1 * 0.3f) ? red : green;
            fillImage.color = Color.Lerp(fillImage.color, coloreTarget, elapsed / 1f);
            
            yield return null;
        }

        timerSlider.value = timerSlider.maxValue;
    }


    void StartTimer(float time)
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null;
        }
        fillImage.color = GetColor(Color.green,0.7f);        
        timeMax = time;
        timer = time;
        timerSlider.maxValue = time;
        timerSlider.value = time;
        ActiveTimer = true;
    }
    
    Color GetColor(Color color, float alpha)
    {
        color.a = alpha;
        return color;
    }

    IEnumerator DestroyWithFade(GameObject obj)
    {
        Image img = obj.GetComponent<Image>();
        float alpha = 1f;

        while (alpha > 0)
        {
            alpha -= Time.deltaTime * 3f; 
            img.color = new Color(img.color.r, img.color.g, img.color.b, alpha);
            yield return null;
        }

        Destroy(obj);
    }


    IEnumerator CreateObjects()
    {
        foreach (GameObject obj in sequenceObject)
        {
            yield return StartCoroutine(DestroyWithFade(obj)); 
        }
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(CreateDropZone());
        yield return new WaitForSeconds(1f);
        sequenceObject = new List<GameObject>();
        //Mischio ogni volta i sprite per una sequenza diversa
        for (int i = 0; i < currentArray.Length; i++)
        {
            int rnd = UnityEngine.Random.Range(i, currentArray.Length);
            Sprite tempSprite = currentArray[i];
            currentArray[i] = currentArray[rnd];
            currentArray[rnd] = tempSprite;
        }
        //Metto gli oggetti nella zona giusta, giocatore visualizza
        for (int i = 0; i < listDropZone.Count; i++)
        {
            GameObject newObject = Instantiate(objectPrefab, listDropZone[i].transform);
            Image image = newObject.GetComponent<Image>();
            image.sprite = currentArray[i]; 
            image.SetNativeSize();

            SecondMiniGameObject script = newObject.GetComponent<SecondMiniGameObject>();
            script.correctPosition = i; 
            script.enabled = false; 
            StartCoroutine(script.FadeIn(0.5f));
            PlayClaveAudio();
            yield return new WaitForSeconds(0.2f);

            sequenceObject.Add(newObject);
        }

        StartTimer(phase1);
        yield return new WaitForSeconds(phase1);

        //Sposto gli oggetti nella zona degli oggetti
        for(int i = 0 ; i< sequenceObject.Count ; i++)
        {
            int random = UnityEngine.Random.Range(i,sequenceObject.Count);
            GameObject temp = sequenceObject[random];
            sequenceObject[random] = sequenceObject[i];
            sequenceObject[i] = temp;
        }
        //Il giocatore inizia a riordinare
        PlaySwooshAudio();
        for(int i = 0 ; i< sequenceObject.Count ; i++)
        {
            SecondMiniGameObject script = sequenceObject[i].GetComponent<SecondMiniGameObject>();
            script.enabled = true;
            StartCoroutine(script.FadeIn(0.5f));
            sequenceObject[i].transform.SetParent(objectsZone.transform);
        }

        StartTimer(phase2);
        yield return new WaitForSeconds(phase2);

        Check();
    }
    void Check()
    {
        int correct = 0;
        for(int i= 0 ; i< listDropZone.Count; i++)
        {
            
            SecondMiniGameDropZone zone = listDropZone[i].GetComponentInParent<SecondMiniGameDropZone>();
            SecondMiniGameObject script = zone.GetComponentInChildren<SecondMiniGameObject>();

            if (script != null && zone.index == script.correctPosition)
            {
                correct++;
            }
            else
            {
                listDropZone[i].GetComponent<SecondMiniGameDropZone>().ShowError();
                if(script != null )script.ShowError();
            }
        }

        if (correct == sequenceObject.Count)
        {
            PlayYayAudioAndConfettiEffect();
            Debug.Log("Hai indovinato tutto!");
            if(round<(sprites.Length-1))
            {
                currentArray = sprites[++round].roundSprites;
                Invoke("Restart",2f);
            }
            else
            {
                ShowResults();
            }
            
            
        }
        else
        {
            wrongs++;
            SoundFXManager.Instance.PlayAudio(wrong);
            Debug.Log("Hai indovinato " + correct + " pezzi.");
            Invoke("Restart",2f);
        }
    }   
    void Restart()
    {
        StartCoroutine(CreateObjects());
    }
    IEnumerator CreateDropZone()
    {
        if(listDropZone.Count != null)
        {
            foreach ( GameObject obj in listDropZone)
            {
                StartCoroutine(DestroyWithFade(obj));
            }
            listDropZone.Clear();
        }
        yield return new WaitForSeconds(3f);
        
        for (int i = 0; i < currentArray.Length; i++)
        {
            GameObject newObject = Instantiate(dropZonePrefab, transform);
            listDropZone.Add(newObject);
            SecondMiniGameDropZone script = newObject.GetComponent<SecondMiniGameDropZone>();
            script.index = i;
            
        }
        playerSequence = new GameObject[currentArray.Length];
    }

  
    public void ChangeObject(GameObject gameObject, int index, int? oldIndex)
    {
        GameObject oldObject = playerSequence[index];
        playerSequence[index] = gameObject;
        if (oldIndex!= null)
        {
            playerSequence[(int)oldIndex] = oldObject;
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

    [SerializeField] private GameObject ResultsMenu;
    [SerializeField] private TMPro.TextMeshProUGUI text;
    void ShowResults()
    {
        ResultsMenu.SetActive(true);
        if(wrongs<= 0)
        {
            text.text = "Cervello Galattico!";
        }
        else
        {
            text.text = "Hai sbagliato:\t" +wrongs+ " volte";
        }
    }

    public void PlayTapAudio()
    {
        SoundFXManager.Instance.PlayAudio(tap);
    }
    public void PlayPopAudio()
    {
        SoundFXManager.Instance.PlayAudio(pop);
    }
    public void PlayClaveAudio()
    {
        SoundFXManager.Instance.PlayAudio(clave);
    }
    public void PlaySwooshAudio()
    {
        SoundFXManager.Instance.PlayAudio(swoosh);
    }
    public void PlayYayAudioAndConfettiEffect()
    {
        SoundFXManager.Instance.PlayAudio(yay);
        confetti1.Play();
        confetti2.Play();
    }


}
