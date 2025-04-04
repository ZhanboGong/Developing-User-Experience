using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CarController : MonoBehaviour
{
    private float horizontalInput, verticalInput;
    private float currentSteerAngle, currentbreakForce;
    private bool isBreaking;
    // WorkShop
    private int count;
    public GameObject countTextObject;
    public TextMeshProUGUI countText;
    public AudioSource clickAudio;
    public GameObject popup;
    public GameObject PauseButton;
    public TextMeshProUGUI TimeText;
    public GameObject TimeTextObject;
    public GameObject FieldPanel;


    // Settings
    [SerializeField] private float motorForce, breakForce, maxSteerAngle;

    // Wheel Colliders
    [SerializeField] private WheelCollider frontLeftWheelCollider, frontRightWheelCollider;
    [SerializeField] private WheelCollider rearLeftWheelCollider, rearRightWheelCollider;

    // Wheels
    [SerializeField] private Transform frontLeftWheelTransform, frontRightWheelTransform;
    [SerializeField] private Transform rearLeftWheelTransform, rearRightWheelTransform;

    void Start()
    {
        Time.timeScale = 1f;
    }
    private void FixedUpdate()
    {
        GetInput();
        HandleMotor();
        HandleSteering();
        UpdateWheels();
        if(TimeText.text == "00:00:00")
        {

            FieldPagePopsUp();
        }
    }

    private void GetInput()
    {
        // Steering Input
        horizontalInput = Input.GetAxis("Horizontal");

        // Acceleration Input
        verticalInput = Input.GetAxis("Vertical");

        // Breaking Input
        isBreaking = Input.GetKey(KeyCode.Space);
    }

    private void HandleMotor()
    {
        frontLeftWheelCollider.motorTorque = verticalInput * motorForce;
        frontRightWheelCollider.motorTorque = verticalInput * motorForce;
        currentbreakForce = isBreaking ? breakForce : 0f;
        ApplyBreaking();
    }

    private void ApplyBreaking()
    {
        frontRightWheelCollider.brakeTorque = currentbreakForce;
        frontLeftWheelCollider.brakeTorque = currentbreakForce;
        rearLeftWheelCollider.brakeTorque = currentbreakForce;
        rearRightWheelCollider.brakeTorque = currentbreakForce;
    }

    private void HandleSteering()
    {
        currentSteerAngle = maxSteerAngle * horizontalInput;
        frontLeftWheelCollider.steerAngle = currentSteerAngle;
        frontRightWheelCollider.steerAngle = currentSteerAngle;
    }

    private void UpdateWheels()
    {
        UpdateSingleWheel(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateSingleWheel(frontRightWheelCollider, frontRightWheelTransform);
        UpdateSingleWheel(rearRightWheelCollider, rearRightWheelTransform);
        UpdateSingleWheel(rearLeftWheelCollider, rearLeftWheelTransform);
    }

    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.rotation = rot;
        wheelTransform.position = pos;
    }

    //Workshop
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("pickup"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
            clickAudio.Play();
        }
        else if (other.gameObject.CompareTag("pickup2"))
        {
            other.gameObject.SetActive(false);
            count = count + 2;
            SetCountText();
            clickAudio.Play();
        }
        else if (other.gameObject.CompareTag("pickup3"))
        {
            other.gameObject.SetActive(false);
            count = count + 5;
            SetCountText();
            clickAudio.Play();
        }
        else if(other.gameObject.CompareTag("trophy"))
        {
            TimeTextObject.SetActive(false);
            PauseButton.SetActive(false);
            other.gameObject.SetActive(false);
            Time.timeScale = 0f;
            countTextObject.SetActive(false);
            SettlementPagePopsUp();
        }
    }

    public void SetCountText()
    {
        countText.text = "Score  " + count.ToString();
    }

    public void SettlementPagePopsUp()
    {
        if (popup != null)
        {
            popup.SetActive(true);
        }
    }

    public void FieldPagePopsUp()
    {
        if(FieldPanel != null)
        {
            TimeTextObject.SetActive(false);
            PauseButton.SetActive(false);
            Time.timeScale = 0f;
            FieldPanel.SetActive(true);
            countTextObject.SetActive(false);
        }
    }

}