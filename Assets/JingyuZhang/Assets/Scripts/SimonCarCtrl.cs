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
    private float turnSpeed = 1f;

    private int count;
    public TextMeshProUGUI countText;
    public AudioSource clickAudio;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = true;
        rb.mass = 1f;
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
        float turnDirection = moveX * Mathf.Sign(moveY);
        rb.angularVelocity = Vector3.up * turnDirection * turnSpeed;

        float forceMultiplier = 1f;
        Vector3 desiredForce = transform.forward * moveY * moveSpeed * forceMultiplier;
        rb.AddForce(desiredForce, ForceMode.Force);

        float maxSpeed = 18f;
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }

        if (Mathf.Abs(transform.eulerAngles.x) > 0.1f || 
            Mathf.Abs(transform.eulerAngles.z) > 0.1f)
        {
            transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y, 0f);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("pickup"))
        {
            CollectItem(other, 1);
        }
        else if (other.CompareTag("pickup2"))
        {
            CollectItem(other, 2);
        }
    }

    private void CollectItem(Collider item, int points)
    {
        item.gameObject.SetActive(false);
        count += points;
        SetCountText();
        clickAudio.Play();
    }

    public void SetCountText()
    {
        countText.text = "Score: " + count.ToString();
    }
}