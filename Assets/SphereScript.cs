using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using eulerian;

public class SphereScript : MonoBehaviour
{

    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        EAEstimate prop = new EAEstimate("sphere/enter", "ref");
        prop.SetCurrency("GBP");

        Product product = new Product("Ref-nut");
        product.SetName("Nut");
        prop.AddProduct(product, 2, 4);

        Eulerian.Track(prop);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
