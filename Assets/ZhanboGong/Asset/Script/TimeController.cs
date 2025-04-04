using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeController : MonoBehaviour
{
    [SerializeField] Text timeText;
    public TextMeshProUGUI countText;
    float clearTime;

    void FixedUpdate()
    {
        clearTime += Time.fixedDeltaTime;
        countText.text = System.TimeSpan.FromSeconds(value: clearTime).ToString(format: @"mm\:ss\:ff");
    }

}
