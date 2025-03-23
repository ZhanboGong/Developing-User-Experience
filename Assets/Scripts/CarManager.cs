using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarManager : MonoBehaviour
{
    public GameObject car;
    public float value;
    public Vector3 sizeChange;
    public float moveSize;

    public void MoveRight()
    {
        value = value - moveSize;
        car.transform.position = new Vector3(-4, 1, value);
    }

    public void MoveLeft()
    {
        value = value + moveSize;
        car.transform.position = new Vector3(-4, 1, value);
    }

    public void RotateRight()
    {
        car.transform.Rotate(0f, 20f, 0f);
    }

    public void RotateLeft()
    {
        car.transform.Rotate(0f, -20f, 0f);
    }

    public void Grow()
    {
        car.transform.localScale = car.transform.localScale + sizeChange;
    }

    public void Shrink()
    {
        car.transform.localScale = car.transform.localScale - sizeChange;
    }

    public void ResetCar()
    {
        car.transform.position = new Vector3(-4, 1, 1);
        car.transform.rotation = Quaternion.Euler(new Vector3(0, -69, 0));
        car.transform.localScale = new Vector3(1,1,1);
        value = 1;
    }
}
