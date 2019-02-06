using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Object entered the trigger");
    }

    private void OnTriggerStay(Collider other)
    {
        //Debug.Log("Object is within trigger");
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Object exit the trigger");
    }
}
