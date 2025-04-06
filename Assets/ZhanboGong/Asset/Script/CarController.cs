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
    private bool isBrakeSoundPlaying = false;
    // WorkShop
    private int count;
    public GameObject countTextObject;
    public TextMeshProUGUI countText;
    public AudioSource clickAudio;
    public AudioSource Score2;
    public AudioSource Score5;
    public AudioSource BackgroundMusic;
    //
    public AudioSource crashAudio;
    public GameObject popup;
    public GameObject PauseButton;
    public TextMeshProUGUI TimeText;
    public GameObject TimeTextObject;
    public GameObject FieldPanel;
    //Car Audio Feedback
    public AudioSource accelerateAudio;
    public AudioSource brakeAudio;
    //
    public TextMeshProUGUI currentScoreText;
    public TextMeshProUGUI timeUsedText;
    float clearTime;
    //
    public AudioSource WinAudio;
    public AudioSource LoseAudio;



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
        //
        if (accelerateAudio == null)
            accelerateAudio = gameObject.AddComponent<AudioSource>();
        if (brakeAudio == null)
            brakeAudio = gameObject.AddComponent<AudioSource>();
    }
    private void FixedUpdate()
    {
        clearTime += Time.fixedDeltaTime;
        GetInput();
        HandleMotor();
        HandleSteering();
        UpdateWheels();
        UpdateAudioStatus();
        if(TimeText.text == "00:00:00")
        {

            FieldPagePopsUp();
        }
        SetScoreText();
        SetTimeText();
        
    }

    private void GetInput()
    {
        // Steering Input
        horizontalInput = Input.GetAxis("Horizontal");

        // Acceleration Input
        verticalInput = Input.GetAxis("Vertical");

        // Breaking Input
        isBreaking = Input.GetKey(KeyCode.Space);

        //
        if (Input.GetKeyDown(KeyCode.W))
        {
            PlayAccelerateSound();
        }

        if (Input.GetKey(KeyCode.Space))
        {
            PlayBrakeSound();
        }
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
            Score2.Play();
        }
        else if (other.gameObject.CompareTag("pickup3"))
        {
            other.gameObject.SetActive(false);
            count = count + 5;
            SetCountText();
            Score5.Play();
        }
        else if(other.gameObject.CompareTag("trophy"))
        {
            TimeTextObject.SetActive(false);
            PauseButton.SetActive(false);
            other.gameObject.SetActive(false);
            Time.timeScale = 0f;
            countTextObject.SetActive(false);
            BackgroundMusic.Stop();
            SettlementPagePopsUp();
            WinAudio.Play();
        }
        else if (other.gameObject.CompareTag("barrier"))
        {
            crashAudio.Play();
        }
    }

    public void SetCountText()
    {
        countText.text = "Score  " + count.ToString();
    }

    public void SetScoreText()
    {
        currentScoreText.text = "Current Score: " + count.ToString();
    }

    public void SetTimeText()
    {
        timeUsedText.text = "Time Used: " + TimeSpan.FromSeconds(value:clearTime).ToString(format: @"mm\:ss\:ff");
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
            BackgroundMusic.Stop();
            LoseAudio.Play();
        }
    }

    private void PlayAccelerateSound()
    {
        if (accelerateAudio != null)
        {
            accelerateAudio.Play();
        }
    }

    private void PlayBrakeSound()
    {
        if (brakeAudio != null && !isBrakeSoundPlaying)
        {
            isBrakeSoundPlaying = true;
            brakeAudio.Play();
        }
    }

    private void UpdateAudioStatus()
    {
        if (isBrakeSoundPlaying && !brakeAudio.isPlaying)
        {
            isBrakeSoundPlaying = false;
        }
    }

}