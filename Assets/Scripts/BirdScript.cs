using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class BirdScript : MonoBehaviour
{
    [SerializeField]
    private float easyModeForce = 250f;
    [SerializeField]
    private float hardModeForce = 400f;
    [SerializeField]
    private bool isEasyMode = true;

    public static float health = 1.0f;
    [SerializeField]
    private float healthTimeout = 30.0f; // seconds before dying

    private float force;
    private Rigidbody2D rb;

    [SerializeField]
    private int tries = 3;
    public static bool isGetHit = false;
    [SerializeField]
    private TMPro.TextMeshProUGUI triesTmp;

    private Vector3 originalPosition;

    void Start()
    {
        originalPosition = this.transform.position;
        rb = this.GetComponent<Rigidbody2D>();
        triesTmp.text = tries.ToString();
    }

    void Update()
    {
        force = isEasyMode ? easyModeForce : hardModeForce;
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0))
        {
            rb.AddForce(force * Time.timeScale * Vector2.up);
        }
        this.transform.eulerAngles = new Vector3(0, 0, rb.linearVelocityY);

        health -= Time.deltaTime / healthTimeout;
        if (health <= 0)
        {
            health = 0.01f;
            Loose("Health", "You didn't eat food and because of this bird died");
        }
    }

    private void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.CompareTag("Food"))
        {
            FoodScript food = obj.GetComponent<FoodScript>();
            GameObject.Destroy(obj.gameObject);
            health = Mathf.Clamp01(health + food.giveHealth / healthTimeout);
        }
        else if (obj.CompareTag("Pipe"))
        {
            Loose("Collision", "You hit an obstacle and loose a life");
        }
    }
    
    private void Loose(string title, string message)
    {
        if(!isGetHit)
        {
            tries--;
            isGetHit = true;
        }
        triesTmp.text = tries.ToString();

        if (tries > 0)
        {
            AlertScript.Show(title, message,
                "Continue", DestroyerScript.ClearField);
        }
        else
        {
            AlertScript.Show("Game over", "You don't have another tries",
                "Restart", () => SceneManager.LoadScene(0));
        }
    }
}
