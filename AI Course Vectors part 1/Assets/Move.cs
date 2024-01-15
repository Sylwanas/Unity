using System.Collections.Generic;
using System.Numerics;
using UnityEditor;
using UnityEditor.U2D;
using UnityEngine;

public class Move : MonoBehaviour {

    public GameObject goal;
    UnityEngine.Vector3 direction;
    public float speed = 3f;

    void Start() 
    {
        //direction = goal.transform.position - transform.position;
        //transform.Translate(direction);
    }

    private void LateUpdate() 
    {
        direction = goal.transform.position - transform.position;
        transform.LookAt(goal.transform.position);

        if (direction.magnitude > 1)
        {
            UnityEngine.Vector3 velocity = direction.normalized * speed * Time.deltaTime;
            transform.position = transform.position + velocity;
        }
    }
}
