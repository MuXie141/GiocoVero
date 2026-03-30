using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MinigamesManager : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private GameObject currentMiniGame;
    [SerializeField] private GameObject miniGameMenu;
    [SerializeField] private Image image;
    [SerializeField] private Sprite[] countDown;
    [SerializeField] private GameObject num;
    [SerializeField] private GameObject backGround;
    public bool active= true;
    [SerializeField] private AudioClip countDownClip;
    [SerializeField] private AudioClip startClip;
    [SerializeField] private GameObject buttonConferma;
    [SerializeField] private GameObject buttonX;
    private bool doNotOpen;
    public void OpenMinigame(GameObject minigame)
    {
        if(!active) return;
        doNotOpen=false;
        GameManager.Instance.wait = true;
        animator.Play("StartMiniGame");
        backGround.SetActive(true);
        miniGameMenu.SetActive(true);
        buttonX.SetActive(true);
        buttonConferma.SetActive(false);
        currentMiniGame = minigame;
        image.sprite = minigame.GetComponent<Image>().sprite; 
        image.SetNativeSize();
    }

    public void ActivateButtonConferm()
    {
        buttonConferma.SetActive(true);
    }

    public void StartMiniGameCoroutine()
    {
        StartCoroutine(StartMiniGame());
    }

    IEnumerator StartMiniGame()
    {
        miniGameMenu.SetActive(false);
        num.SetActive(true);
        for (int i = 0; i < countDown.Length; i++) 
        {
            SoundFXManager.Instance.PlayAudio(countDownClip);
            num.GetComponent<Image>().sprite = countDown[i]; 
            yield return new WaitForSeconds(1f);
        }
        SoundFXManager.Instance.PlayAudio(startClip);
        num.SetActive(false);
        if(doNotOpen) yield break;
        currentMiniGame.SetActive(true);
    }

    public void CloseMinigame()
    {
        doNotOpen = true;
        animator.Play("CloseMiniGame");
        currentMiniGame.SetActive(false);

    }

    public void CloseBackGround()
    {
        backGround.SetActive(false);
        GameManager.Instance.wait = false;
    }

    /*public void MakeSureItsClosed()
    {
        currentMiniGame.SetActive(false);
    }*/
    public void RestartMiniGame()
    {
        currentMiniGame.SetActive(false);
        currentMiniGame.SetActive(true);
    }

    [Header("Bottone")]
    [SerializeField] private Image buttonImage;
    [SerializeField] private Sprite[] sprites;

    public void Button()
    {
        active = !active;
        int index = (active) ? 0 : 1;
        buttonImage.sprite = sprites[index];
    }

    private void Awake() 
    {
        active = true;
    }
}
