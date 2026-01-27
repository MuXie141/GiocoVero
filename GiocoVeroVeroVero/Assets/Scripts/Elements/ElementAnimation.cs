using UnityEngine;
using System.Collections;

public class ElementAnimation : MonoBehaviour
{
    private Animator animator;


    private void Start() 
    {
        animator = GetComponentInChildren<Animator>(true);
    }

    public void PlayAnimation(string animation)
    {
        GameManager.Instance.wait = true;
        animator.Play(animation);
        Debug.Log("Animazione eseguita!");
        //GameManager.Instance.currentAnimationSeconds = GetCurrentAnimatorStateInfo(0).length;
        StartCoroutine(WaitForAnim(animation));
    }

    IEnumerator WaitForAnim(string stateName) 
    {
 
        yield return null; // Perchè potrebbe catturare animazione precedente

        AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);

        while (info.IsName(stateName) && info.normalizedTime < 0.98f) {
            info = animator.GetCurrentAnimatorStateInfo(0);
            yield return null;
        }

        Debug.Log("Animazione completata con successo");
        GameManager.Instance.wait = false;


    }
    


    

/* 
public IEnumerator AnimationSequence(string animation)
    {
        GameManager.Instance.isAnimating = true;
        animator.Play(animation);
        yield return null;
        GameManager.Instance.currentAnimationSeconds=animator.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        GameManager.Instance.isAnimating = false;

    }



public void PlayAnimation(string animation, int stop)
    {
        StartCoroutine(AnimationSequence(animation, stop));
    }

   public IEnumerator AnimationSequence(string animation, int stop)
    {
    GameManager.Instance.isAnimating = true;
        animator.Play(animation);
        yield return null;
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        yield return new WaitForSeconds(stop);
        GameManager.Instance.isAnimating = false;

    }*/
}