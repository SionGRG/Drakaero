using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Borders : MonoBehaviour
{
    public float Left;
    public float Right;
    public float Top;
    public float Bottom;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, Left, Right),
            Mathf.Clamp(transform.position.y, Bottom, Top), transform.position.z);
    }
}

