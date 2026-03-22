using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DayUI : MonoBehaviour
{
    public static DayUI Instance;

    [SerializeField] private Image[] nodes;     
    [SerializeField] private Image overlay;

    void Awake()
    {
        Instance = this;
    }

    public IEnumerator Advance(int day)
    {
        yield return Fade(overlay, 0f, 1f, 1f);

        if (day >= 0 && day <= nodes.Length)
            nodes[day].color = Color.white;
            if(day!=nodes.Length-1)
            {
                nodes[day+1].color = Color.blue;
            }
            

        // Hold
        yield return new WaitForSeconds(1f);

        // Fade back out
        yield return Fade(overlay, 1f, 0f, 1f);
    }

    private IEnumerator Fade(Image img, float from, float to, float dur)
    {
        float t = 0f;
        Color c = img.color;
        while (t < dur)
        {
            t += Time.deltaTime;
            c.a = Mathf.Lerp(from, to, t / dur);
            img.color = c;
            yield return null;
        }
        c.a = to;
        img.color = c;
    }
}
