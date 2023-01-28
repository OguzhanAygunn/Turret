using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    [SerializeField] float rotateSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RotateOperations();
    }

    void RotateOperations(){
        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
    }
}
