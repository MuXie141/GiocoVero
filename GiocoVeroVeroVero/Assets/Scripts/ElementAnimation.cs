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
        animator.Play(animation);
        GameManager.Instance.currentAnimationSeconds=animator.GetCurrentAnimatorStateInfo(0).length;
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