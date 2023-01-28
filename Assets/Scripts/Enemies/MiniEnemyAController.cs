using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MiniEnemyAController : MonoBehaviour
{
    Vector3 randomRot,defaultScale;
    [SerializeField] GameObject spawnEffect,Coin;
    CapsuleCollider MyColl;
    Transform playerPos,targetPos;
    private float speed = 2f,radius = 10f;
    Collider[] hitColliders = new Collider[1];
    public LayerMask targetLayer;
    // Start is called before the first frame update

    private void Awake() {
        Instantiate(spawnEffect,transform.position,Quaternion.identity);
        MyColl = GetComponent<CapsuleCollider>();
        //Instantiate(spawnEffect,transform.position,Quaternion.identity);
        defaultScale = transform.localScale;
        playerPos = GameObject.FindGameObjectWithTag("Player").gameObject.transform;
    }

    private void FixedUpdate() {
        MovementOperations();
        Radar();
    }

    void MovementOperations(){
        if(!GameManager.GameLose && !GameManager.GameWin){
            if(targetPos == null){
                transform.position = Vector3.MoveTowards(transform.position,playerPos.position,speed*Time.fixedDeltaTime);
            }
            else{
                transform.position = Vector3.MoveTowards(transform.position,targetPos.position,speed*Time.fixedDeltaTime);
            }
        }
    }

    void Radar(){
        hitColliders = Physics.OverlapSphere(transform.position, radius, targetLayer);
        if(targetPos == null && hitColliders != null){
            foreach(var obj in hitColliders){
                targetPos = obj.transform;
            }
        }
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.CompareTag("Bullet") || other.gameObject.CompareTag("Turret")){
            DestroyFunc();
        }
    }

    public void DestroyFunc(){
        Instantiate(spawnEffect,transform.position,Quaternion.identity);
        Destroy(this.gameObject);
        Instantiate(Coin,transform.position,Quaternion.identity);
    }
}
