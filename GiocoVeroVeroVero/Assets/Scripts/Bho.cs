using UnityEngine;

public class Bho : MonoBehaviour
{
    [SerializeField] private GameObject lombrichi;
    public void Hide()
    {
        lombrichi.SetActive(false);
    }
}
