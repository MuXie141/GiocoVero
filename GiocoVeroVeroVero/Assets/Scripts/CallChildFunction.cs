using UnityEngine;

public class CallChildFunction : MonoBehaviour
{
    [SerializeField] private TransitionEffect child; 

    
    public void CallChildTransition1_Function()
    {
        child.Change();
    }

    public void CallChildTransition2_Function()
    {
        child.ChangeMoreTime();
    }

}
