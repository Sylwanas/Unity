using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondsUpdate : MonoBehaviour
{
    float timeStartOffset = 0;
    bool gotStartTimer = false;
    public float speed = 1;

    void Update()
    {
        if (!gotStartTimer)
        {
            timeStartOffset = Time.realtimeSinceStartup;
            gotStartTimer = true;
        }
        transform.position = new Vector3(transform.position.x, transform.position.y, (Time.realtimeSinceStartup - timeStartOffset) * speed);
    }
}
