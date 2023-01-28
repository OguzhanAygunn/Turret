using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallController : MonoBehaviour
{
    [SerializeField] float minY;
    CameraController cameraController;
    Rigidbody rigidbody;
    bool active;
    // Start is called before the first frame update
    void Start()
    {
        cameraController = Camera.main.gameObject.GetComponent<CameraController>();
        rigidbody        = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        mainOperations();
    }

    void mainOperations(){
        if(transform.position.y < minY && !GameManager.GameLose && !active){
            active = true;
            cameraController.GameDeathController(true);
            rigidbody.constraints = RigidbodyConstraints.FreezeAll;
            GameObject[] turrets = GameObject.FindGameObjectsWithTag("TakeTurret");
            foreach(GameObject turret in turrets){
                TurretCollision tc = turret.gameObject.GetComponent<TurretCollision>();
                tc.DestroyFunction();
            }
        }
    }
}
