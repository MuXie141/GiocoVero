using UnityEngine;
using System.Collections;

public class TransitionEffect : MonoBehaviour
{
    [Header("Normal")]
    [SerializeField] private float duration;
    [SerializeField] private AnimationCurve curve;
    [Header("Flashing")]
    [SerializeField] private float flashDuration;
    [SerializeField] private int flashCount;
    [SerializeField] private AnimationCurve flashCurve;

    private bool flash;
    private float timer, value, time, times;
    private Material material;
    private AnimationCurve animationCurve;





    void Start()
    {
        material = GetComponent<SpriteRenderer>().material;
    }

    //Sicuramente c'è un metodo migliore per fare,tipo usare la cura che va a metà a 1 e poi ritorna a 0, for loop ecc, ma va bene così(spero), se hai tempo

    public void Change()
    {
        flash = false;
        timer = 0f;
        animationCurve = curve;
        time = duration;
        StartCoroutine(ChangeCoroutine());
    }
    public void ChangeMoreTime()
    {
        flash = true;
        timer = 0f;
        animationCurve = flashCurve;
        time = flashDuration;
        times = 1;
        StartCoroutine(ChangeCoroutine());
    }
    private IEnumerator ChangeCoroutine()
    {
        if(timer == 0)
        {
            while(timer<=time)
            {   
                timer += Time.deltaTime;
                value = curve.Evaluate(timer/time);
                material.SetFloat("_Value", value);
                yield return null;
            }
            material.SetFloat("_Value", curve.Evaluate(1f));
        }
        else
        {
            while(timer >= 0)
            {   
                timer -= Time.deltaTime;
                value = curve.Evaluate(timer/time);
                material.SetFloat("_Value", value);
                yield return null;
            }
            material.SetFloat("_Value", curve.Evaluate(0f));
            timer = 0;
        } 

        if(!flash) yield break;
        Flash();
    }

    private void Flash()
    {
            times = times + 0.5f;
            if(times==flashCount) flash = false;
            StartCoroutine(ChangeCoroutine());
    }

    
}
