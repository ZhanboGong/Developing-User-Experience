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
    float timeLimit = 60;

    void FixedUpdate()
    {
        clearTime += Time.fixedDeltaTime;
        countText.text = System.TimeSpan.FromSeconds(value: timeLimit - clearTime).ToString(format: @"mm\:ss\:ff");
    }

}
