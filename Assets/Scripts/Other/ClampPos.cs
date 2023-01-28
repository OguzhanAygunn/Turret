using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClampPos : MonoBehaviour
{
    Transform ground;
    [SerializeField] LayerMask groundLayer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }

    void rayOperations(){
        RaycastHit hit;
        if(Physics.Raycast(transform.position,Vector3.down,out hit, Mathf.Infinity, groundLayer)){
            if(ground == null || ground != hit.collider.gameObject.transform){
                ground = hit.collider.gameObject.transform;
            }
        }
    }

}
