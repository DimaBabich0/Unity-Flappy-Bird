using UnityEngine;

public class BirdScript : MonoBehaviour
{
    [SerializeField]
    private float easyModeForce = 250f;
    [SerializeField]
    private float hardModeForce = 400f;
    [SerializeField]
    private bool isEasyMode = true;

    private float force;
    private Rigidbody2D rb;

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        force = isEasyMode ? easyModeForce : hardModeForce;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(force * Vector2.up);
        }
    }
}
