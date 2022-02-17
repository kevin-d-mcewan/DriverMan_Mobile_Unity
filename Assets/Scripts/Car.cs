using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField] private float carSpeed = 10.0f;
    [SerializeField] private float speedGainPerSecond = 0.2f;
    

    // Update is called once per frame
    void Update()
    {
        carSpeed += speedGainPerSecond * Time.deltaTime;
        // "Translate" function is how to move things forward in the "transform" component
        // "Time.deltaTime" is the completion time in seconds since the last frame
        transform.Translate(Vector3.forward * carSpeed * Time.deltaTime);
    }
}
