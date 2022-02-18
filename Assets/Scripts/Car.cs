using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Car : MonoBehaviour
{
    [SerializeField] private float carSpeed = 10.0f;
    [SerializeField] private float speedGainPerSecond = 0.2f;
    // Developer access to turn car in testing
    [SerializeField] private float turnSpeed = 200f;
    [SerializeField] Rigidbody rb;

    // Turning of the car -1 = left, 0 = straight, 1 = right
    private int steerValue;
    

    // Update is called once per frame
    void Update()
    {
        // Increase car speed over every second
        carSpeed += speedGainPerSecond * Time.deltaTime;

        // Enable car turning
        transform.Rotate(0f, steerValue * turnSpeed, 0f);


        // "Translate" function is how to move things forward in the "transform" component
        // "Time.deltaTime" is the completion time in seconds since the last frame
        transform.Translate(Vector3.forward * carSpeed * Time.deltaTime);
    }

    // Will be called when ScriptOwner [car] collides with another object
    private void OnTriggerEnter(Collider other)
    {
        // If "other" being passed collides with Car (in this case) and has Tag of "Obstacles" go to Scene 0
        if (other.CompareTag("Obstacles"))
        {
            SceneManager.LoadScene(0);
        }
    }

    // Gives us a way outside of the script to call and change the steerValue
    public void Steer (int value)
    {
        steerValue = value;
    }

}
