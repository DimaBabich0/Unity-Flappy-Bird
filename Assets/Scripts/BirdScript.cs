using System;
using UnityEngine;

public class BirdScript : MonoBehaviour
{
    [SerializeField]
    private float easyModeForce = 250f;
    [SerializeField]
    private float hardModeForce = 400f;
    [SerializeField]
    private bool isEasyMode = true;

    public static float health = 1f;
    [SerializeField]
    private float healthTimeout = 100.0f; // seconds before dying

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
        this.transform.eulerAngles = new Vector3(0, 0, rb.linearVelocityY);

        health -= Time.deltaTime / healthTimeout;
    }

    private void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.CompareTag("Food"))
        {
            FoodScript food = obj.GetComponent<FoodScript>();
            GameObject.Destroy(obj.gameObject);
            health = Mathf.Clamp01(health + food.giveHealth / healthTimeout);
        }
        if (obj.CompareTag("Pipe"))
        {
            AlertScript.Show("Collision", "You hit an obstacle and loose a life");
        }    
    }
}
