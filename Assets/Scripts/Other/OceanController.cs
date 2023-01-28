using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OceanController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        string tag = other.tag;
        if(tag == "Enemy" || tag == "Coin"){
            Destroy(other.gameObject);
        }
    }
}
