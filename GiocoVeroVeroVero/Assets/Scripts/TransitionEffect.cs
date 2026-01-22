using UnityEngine;
using System.Collections;

public class TransitionEffect : MonoBehaviour
{
    private Material material;
    [SerializeField] private float time = 10f;
    [SerializeField] private AnimationCurve curve;
    [Header("If loop")]
    [SerializeField] private int loopCount;

    private float timer, value;
    private int times;





    void Start()
    {
        material = GetComponent<SpriteRenderer>().material;
    }

    //Sicuramente c'è un metodo migliore per fare,tipo usare la cura che va a metà a 1 e poi ritorna a 0, for loop ecc, ma va bene così(spero), se hai tempo

    public void Change()
    {
        times = loopCount + 1;
        timer = 0f;
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
        if(times <= loopCount)
        {
            times++;
            StartCoroutine(ChangeCoroutine());
        }
    }

    public void ChangeMoreTime()
    {
        times = 0;
        timer = 0f;
        StartCoroutine(ChangeCoroutine());
    }
}
