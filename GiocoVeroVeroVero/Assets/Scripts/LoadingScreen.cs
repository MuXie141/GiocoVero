using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private Animator logoAnimator;
    [SerializeField] private float Duration = 3f;

    void Start()
    {
        StartCoroutine(LoadAfterSplash());
    }

    IEnumerator LoadAfterSplash()
    {
        yield return new WaitForSeconds(Duration);
        SceneManager.LoadScene("SampleScene");
    }
}
