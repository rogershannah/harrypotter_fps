using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynanmicCrate : MonoBehaviour
{
    public float speed = 1;
    public float distance = 5;

    Vector3 startingPos;
    void Start()
    {
        startingPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = transform.position;
        newPos.x = startingPos.x + (Mathf.Sin(Time.time * speed) * distance);
        transform.position = newPos;
    }
}
