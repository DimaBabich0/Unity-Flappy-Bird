using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControlScript : MonoBehaviour
{
    private Rigidbody2D rb;
    private InputAction moveAction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        moveAction = InputSystem.actions.FindAction("Move");
    }

    // Update is called once per frame
    void Update()
    {
        //// 1. Input - прямой доступ до устройств (клавиатура, мышка, геймпад и т.д.)
        //if (Input.GetKeyDown(KeyCode.W))
        //{
        //    rb.AddForce(Vector2.up * 200f);
        //}
        //if (Input.GetKey(KeyCode.A))
        //{
        //    rb.AddForce(Vector2.left * 1f);
        //}
        //if (Input.GetKey(KeyCode.D))
        //{
        //    rb.AddForce(Vector2.right * 1f);
        //}

        //// 2. Input Manager - соединение разных способов управления по "осям"
        //float y = Input.GetAxis("Vertical");
        //rb.AddForce(5f * y * Vector2.up);
        //float x = Input.GetAxis("Horizontal");
        //rb.AddForce(5f * -x * Vector2.left);

        // 3. Input System - соединение осей в векторе
        rb.AddForce(1f * moveAction.ReadValue<Vector2>());
    }
}
