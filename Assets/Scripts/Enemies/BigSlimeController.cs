using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BigSlimeController : MonoBehaviour
{
    Collider[] hitColliders = new Collider[1];
    [SerializeField] float radius,speed;
    [SerializeField] private Transform[] spawnPosS;
    [SerializeField] GameObject MiniSlime,destroyEffect,Coin;
    int health = 45,enemyCount = 15,coinCount = 50;
    int Health{
        get{
            return health;
        }
        set{
            
            health = value;
            if(health == 0){
                Destroy(myColl);
                while(enemyCount > 0){
                    MiniSlimeController msc = Instantiate(MiniSlime,MiniEnemyPos(),Quaternion.identity).gameObject.GetComponent<MiniSlimeController>();
                    msc.targetPos = playerPos;
                    enemyCount--;
                }
                Instantiate(destroyEffect,transform.position,Quaternion.identity);
                StartCoroutine(coinSpawner());
                transform.DOScale(Vector3.zero,0.2f);
            }
        }
    }
    [SerializeField] LayerMask targetLayer;
    Transform targetPos,playerPos;
    bool takeDamageState,changeScaleActive,moveFreeze,collGround;
    Collider myColl;
    Vector3 targetScale;
    Material material;
    Rigidbody rigidbody;
    // Start is called before the first frame update

    private void Awake() {
        Vector3 pos = transform.position;
        pos.y = 80f;
        transform.position = pos;
        rigidbody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        myColl      = GetComponent<Collider>();
        playerPos   = GameObject.FindGameObjectWithTag("Player").gameObject.transform;
        targetScale = transform.localScale;
        material    = transform.GetChild(1).gameObject.GetComponent<SkinnedMeshRenderer>().material;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        movementOperations();
        lookAtOperations();
        Radar();
        scaleOperations();
    }

    void movementOperations(){
       if(targetPos && !GameManager.GameLose && !GameManager.GameWin && !moveFreeze){
           transform.position = Vector3.MoveTowards(transform.position,targetPos.position,speed*Time.fixedDeltaTime);
       } 
    }

    void lookAtOperations(){
        if(targetPos){
            transform.LookAt(targetPos.position,transform.up);
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

    void scaleOperations(){
        if(health > 0){
            if(transform.localScale != targetScale){
                transform.localScale = Vector3.MoveTowards(transform.localScale,targetScale,1.5f*Time.fixedDeltaTime);
            }
        }
    }

    public Vector3 MiniEnemyPos()
    {
        Vector3 pos = Random.insideUnitSphere*0.002f;
        Vector3 newPos = transform.position + pos;
        newPos.y = transform.position.y;
        return newPos;
    }

    IEnumerator coinSpawner(){
        while(coinCount > 0){
            Vector3 pos = transform.position;
            pos.y = 2f;
            Instantiate(Coin,pos,Quaternion.identity);
            coinCount--;
            yield return new WaitForSeconds(0.05f);
        }
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter(Collision other) {
        string tag = other.gameObject.tag;
        if(tag == "Bullet" && !takeDamageState){
            takeDamageFunction();
            targetScale = transform.localScale;
            targetScale *= 1.02f;
            Color color = material.color;
            color.r += 0.033f;
            material.color = color;
            
        }
        if(tag == "Player" || tag == "TakeTurret"){
            moveFreeze = true;
        }
        if(other.gameObject.layer == LayerMask.NameToLayer("Ground")){
            if(!collGround){
                rigidbody.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
                collGround = true;
            }
        }
        
    }

    private void OnCollisionExit(Collision other) {
        string tag = other.gameObject.tag;
        if(tag == "Player" || tag == "TakeTurret"){
            moveFreeze = false;
        }
    }

    private void OnTriggerEnter(Collider other) {
        string tag = other.gameObject.tag;
        if(tag == "Player" || tag == "TakeTurret"){
            moveFreeze = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        string tag = other.gameObject.tag;
        if(tag == "Player" || tag == "TakeTurret"){
            moveFreeze = false;
        }
    }

    void takeDamageFunction(){
        Health -= 1;
    }
}
