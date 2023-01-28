using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretMovement : MonoBehaviour
{
    public Transform TargetPos;
    public bool active;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        MovementOperations();
    }

    private void MovementOperations(){
        if(active){
            transform.localPosition = Vector3.MoveTowards(transform.localPosition,Vector3.zero,speed*Time.deltaTime);
            if(transform.position == Vector3.zero){
                Destroy(this);
            }
        }
    }
}
