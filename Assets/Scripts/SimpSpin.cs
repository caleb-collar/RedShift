using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpSpin : MonoBehaviour
{
    private Transform t;
    [Range(0,100)]
    [SerializeField] private float rotationSpeed = 0f;

    void Start()
    {
        t = GetComponent<Transform>();
    }

    void Update()
    {
        t.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }
}
