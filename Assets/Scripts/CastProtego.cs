using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastProtego : MonoBehaviour
{
    public GameObject protegoPrefab;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            GameObject protego = Instantiate(protegoPrefab) as GameObject;
            protego.transform.position = transform.position;
            protego.transform.position -= new Vector3(0, 4f, 0);
        }
    }
}
