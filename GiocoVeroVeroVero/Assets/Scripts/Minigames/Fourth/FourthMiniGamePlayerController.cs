using UnityEngine;

public class FourthMiniGamePlayerController : MonoBehaviour
{
    [SerializeField] private float force = 10f; 
    [SerializeField] private float rotationSpeed = 200f;
    [SerializeField] private FourthMiniGameBackgroundParallax background; 
    
    [SerializeField] private Rigidbody2D rb;



    void Update()
    {
        float rotation = -Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;
        transform.Rotate(0, 0, rotation);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Push();
        }
    }

    void Push()
    {
        rb.AddForce(transform.up * force, ForceMode2D.Impulse);

        //quanto il giocatore è rivolto verso asse x positiovo
        float forward = Mathf.Max(0, transform.up.x); 
        
        background.AddForce(force * forward * 0.05f);
        
    }
}