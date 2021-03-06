﻿using UnityEngine;
using eulerian;

public class PlaneScript : MonoBehaviour
{
    private void Awake()
    {
        Eulerian.Init("et11.eulerian.net");
        Debug.Log("EUIDL: " + Eulerian.getEuidl());
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

        EAProperties prop = new EAProperties("plane/enter");
        prop.SetEmail("francois@eulerian.com");
        prop.SetNewCustomer(true);

        Action action = new Action();
        action.SetReference("my reference");
        action.SetIn("my in");
        action.AddOut(new string[] { "out 1" });
        action.AddOut(new string[] { "out 2", "out 3" });
        prop.SetAction(action);

        SiteCentric flag = new SiteCentric();
        flag.Set("key1", new string[] { "value1.1", "value1.2" });
        flag.Set("key2", new string[] { "value2.1" });
        prop.SetCFlag(flag);

        Eulerian.Track(prop);
    }

    private void OnTriggerStay(Collider other)
    {
        //Debug.Log("Object is within trigger");
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Object exit the trigger");

        EACart prop = new EACart("plane/exit");
        prop.SetNewCustomer(false);
        prop.SetUID("udid-test");
        prop.SetCartCumul(true);

        Product p1 = new Product("ref-p1");
        p1.SetName("p1");
        p1.SetGroup("alimentaire");
        prop.AddProduct(p1, 2, 2);

        Product p2 = new Product("ref-p2");
        p2.SetName("p2");
        p2.SetGroup("alimentaire");
        prop.AddProduct(p2, 0.4, 1);

        Eulerian.Track(prop);
    }

}