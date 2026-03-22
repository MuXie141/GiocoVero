using UnityEngine;

public class PlayChildeAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public void PlayAnimation(string name)
    {
        animator.Play(name);
    }
}
