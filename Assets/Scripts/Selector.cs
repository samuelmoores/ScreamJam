using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Selector : MonoBehaviour
{
    float angle;

    // Start is called before the first frame update
    void Start()
    {
        angle = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        angle += Time.deltaTime;
        transform.Rotate(0, Time.deltaTime * 50.0f, 0);
    }
}
