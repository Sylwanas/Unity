using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedUpdateMove : MonoBehaviour
{
    public float speed = 1;
    void FixedUpdate()
    {
        transform.Translate(0, 0, Time.deltaTime * speed);
    }
}
