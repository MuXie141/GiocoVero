using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OtherUI : MonoBehaviour
{
    [SerializeField] private GameObject[] tutorials;
    [SerializeField] private GameObject blackPanel;
    private int watchedIndex;
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ShowTutotial()
    {
        foreach (GameObject var in tutorials)
        {
            var.SetActive(false);
        }
        watchedIndex = 0;
        blackPanel.SetActive(true);
        ShowNext();
        
    }

    public void ShowNext()
    {
        if (watchedIndex < tutorials.Length)
        {
            
            if (watchedIndex > 0)
            {
                tutorials[watchedIndex - 1].SetActive(false);
            }
            tutorials[watchedIndex].SetActive(true);
            watchedIndex++;
        }
        else
        {
            CloseTutorial();
        }
    }

    public void CloseTutorial()
    {
        blackPanel.SetActive(false);
    }

    private void Update()
    {
        if (blackPanel.activeSelf)
        {
            if (Input.anyKeyDown)
            {
                ShowNext();
            }
        }
    }
}

