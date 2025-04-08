using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class SimonCarCtrl : MonoBehaviour
{
    private Rigidbody rb;
    private float moveX;
    private float moveY;
    private float moveSpeed = 15f;
    private float turnSpeed = 50f; // 转向速度

    private int count;
    public TextMeshProUGUI countText;

    public AudioSource clickAudio;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
    }

    public void OnMove(InputValue moveValue)
    {
        Vector2 moveVector = moveValue.Get<Vector2>();
        moveX = moveVector.x;
        moveY = moveVector.y;
    }

    public void FixedUpdate()
    {
        // 根据水平输入（moveX）调整车辆的旋转
        if (moveX != 0)
        {
            transform.Rotate(Vector3.up, moveX * turnSpeed * Time.fixedDeltaTime);
        }

        // 如果还有垂直输入（moveY），则根据输入向前或向后移动
        if (moveY != 0)
        {
            Vector3 forwardMovement = transform.forward * moveY * moveSpeed * Time.fixedDeltaTime;
            rb.MovePosition(transform.position + forwardMovement);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("pickup"))
        {
            other.gameObject.SetActive(false);
            count += 1;
            SetCountText();
            clickAudio.Play();
        }
        else if(other.gameObject.CompareTag("pickup2"))
        {
            other.gameObject.SetActive(false);
            count += 2;
            SetCountText();
            clickAudio.Play();
        }
    }

    public void SetCountText()
    {
        countText.text = "Score: " + count.ToString();
    }
}