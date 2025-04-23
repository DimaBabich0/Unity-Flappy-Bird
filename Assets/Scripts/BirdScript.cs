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

    [SerializeField]
    private float health = 100f;

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

        health -= Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.CompareTag("Food"))
        {
            FoodScript food = obj.GetComponent<FoodScript>();
            GameObject.Destroy(obj.gameObject);
            health = Mathf.Clamp(health + food.giveHealth, 0f, 100f);
            Debug.Log($"Health: {this.health.ToString("F0")}");
        }
    }
}
