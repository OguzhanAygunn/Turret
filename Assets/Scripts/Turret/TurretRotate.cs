using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretRotate : MonoBehaviour
{
    float x = 0;
    Vector3 rotate;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //RotateOperations();   
    }

    void RotateOperations(){
        if(transform.parent){
            transform.localRotation = Quaternion.Euler(90,0,0);
        }
    }
}
