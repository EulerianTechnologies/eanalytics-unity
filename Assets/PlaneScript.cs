using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using eulerian;

public class PlaneScript : MonoBehaviour
{
    private void Awake()
    {
        Eulerian.Init("et11.eulerian.net");
    }

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
        Eulerian.Track("enter");
    }

    private void OnTriggerStay(Collider other)
    {
        //Debug.Log("Object is within trigger");
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Object exit the trigger");
        Eulerian.Track("exit");
    }

}