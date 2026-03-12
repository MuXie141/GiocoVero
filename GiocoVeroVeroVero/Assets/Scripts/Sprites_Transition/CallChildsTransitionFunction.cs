using UnityEngine;

public class CallChildsTransitionFunction : MonoBehaviour
{
    [SerializeField] private TransitionEffect[] childs; 

    
    public void CallChildTransition1_Function()
    {
        for (int i = 0; i < childs.Length; i++)
        {
            childs[i].Change();
        }
    }

    public void CallChildTransition2_Function()
    {
        for (int i = 0; i < childs.Length; i++)
        {
            childs[i].ChangeMoreTime();
        }
    }
}
