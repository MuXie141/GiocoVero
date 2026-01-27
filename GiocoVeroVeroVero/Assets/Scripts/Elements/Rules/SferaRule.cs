using UnityEngine;

public class SferaRule : MonoBehaviour
{
    [SerializeField] private GameObject buttons;

    public void Primo()
    {
        buttons.SetActive(true);
    }
}
