using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public float moveSpeed;
    public Vector3 offset;
    public float followDistance;

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = Vector3.Lerp
            (transform.position, target.position + offset - transform.forward * followDistance, moveSpeed * Time.deltaTime);

        transform.position = pos;
    }
}
