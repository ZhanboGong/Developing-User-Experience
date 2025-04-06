using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] float x;
    [SerializeField] float y;
    [SerializeField] float z;
    void Start()
    {

    }
    void Update()
    {
        transform.Rotate(new Vector3(x, y, z) * Time.deltaTime);
    }
}
