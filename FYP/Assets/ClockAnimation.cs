using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockAnimation : MonoBehaviour
{
    public float totalTime = 10f; //total time
    public Transform secondHand; // second hand
    private float startAngle = -90f;
    private float startTime;
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        float elapsedTime = Time.time - startTime; 
        if (elapsedTime <= totalTime) 
        {
            //calculate the angle of rotation
            float rotationAngle = startAngle - (elapsedTime / totalTime) * 360f;
            secondHand.rotation = Quaternion.Euler(0f, 0f, rotationAngle);
        }
        else
        {
            secondHand.rotation = Quaternion.Euler(0f, 0f, startAngle);
            elapsedTime = 0;
        }
        
    }
}
