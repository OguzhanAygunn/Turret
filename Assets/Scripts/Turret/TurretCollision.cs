using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TurretCollision : MonoBehaviour
{
    CameraController cameraController;
    BoxCollider  myCollider;
    Rigidbody rigidbody;
    Vector3 colliderFirstScale,deathScale,targetScale;
    TurretMovement turretMovement;
    MeshRenderer myRender;
    public Transform targetPos;
    public List<Transform> posS = new List<Transform>();
    Vector3 defaultScale;
    Color defaultColor;
    public bool isColl;
    bool destroyActive,takeDamageState;
    [SerializeField] GameObject destroyEffect,enemyDestroyEffect;
    DeathEffectController deathEffectController;
    int health = 3;
    public int Health{
        get{
            return health;
        }
        set{
            health = value;
            if(Health <= 0 && !destroyActive){
                DestroyFunction();
            }
            else{
                if(!takeDamageState){
                    takeDamageState = true;
                    defaultScale = transform.localScale;
                    myRender.material.color = Color.Lerp(myRender.material.color,Color.red,30*Time.deltaTime);
                    Vector3 targetScale = defaultScale * 1.2f;
                    targetScale = new Vector3(Mathf.Clamp(targetScale.x,defaultScale.x,defaultScale.x*2),
                        Mathf.Clamp(targetScale.y, defaultScale.y, defaultScale.y * 2),
                        Mathf.Clamp(targetScale.z, defaultScale.z, defaultScale.z * 2));
                    transform.DOScale(targetScale,0.2f).OnComplete( () => {
                        transform.DOScale(defaultScale,0.2f).OnComplete( () => {
                            takeDamageState = false;
                        });
                    });
                }
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        myCollider            = GetComponent<BoxCollider>();
        rigidbody             = GetComponent<Rigidbody>();
        turretMovement        = GetComponent<TurretMovement>();
        myRender              = transform.GetChild(1).gameObject.GetComponent<MeshRenderer>();
        colliderFirstScale    = myCollider.size;
        myCollider.size       = new Vector3(colliderFirstScale.x * 1.4f,colliderFirstScale.y,colliderFirstScale.z * 1.4f);
        defaultScale          = transform.localScale;
        defaultColor          = myRender.material.color;
        cameraController      = Camera.main.gameObject.GetComponent<CameraController>();
        deathEffectController = Camera.main.gameObject.transform.GetChild(2).gameObject.GetComponent<DeathEffectController>();
    }

    private void FixedUpdate() {
        if(!isColl){
            transform.Rotate(0,260*Time.deltaTime,0);
        }

        transform.localScale = new Vector3(Mathf.Clamp(defaultScale.x,defaultScale.x,defaultScale.x*2),
            Mathf.Clamp(defaultScale.y, defaultScale.y, defaultScale.y * 2),
            Mathf.Clamp(defaultScale.z, defaultScale.z, defaultScale.z * 2));
    }

    public void DestroyFunction(){
        PlayerCollision.turrets.Remove(this.gameObject);
        destroyActive = true;
        Vector3 targetScale = defaultScale * 1.5f;
        targetScale = new Vector3(Mathf.Clamp(targetScale.x, defaultScale.x, defaultScale.x * 2),
        Mathf.Clamp(targetScale.y, defaultScale.y, defaultScale.y * 2),
        Mathf.Clamp(targetScale.z, defaultScale.z, defaultScale.z * 2));
        transform.DOScale(defaultScale * 1.5f,0.66f);
        myRender.material.DOColor(Color.red,0.68f).OnComplete( () => {
            Instantiate(destroyEffect,transform.position,Quaternion.identity);
            Destroy(this.gameObject);
            if(PlayerCollision.turrets.Count <= 0){
                Debug.Log("game lose");
            }
            cameraController.GameDeathController(false);
        });
    }

    private void OnTriggerEnter(Collider other) {
        if(((other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("TakeTurret")) && !isColl) && !destroyActive){
            GameManager.FirstColl = (GameManager.FirstColl == false) ? true : GameManager.FirstColl;
            if(transform.CompareTag("Turret")){
                transform.tag = "TakeTurret";
            }
            PlayerCollision.turrets.Add(this.gameObject);
            gameObject.layer = 7;
            isColl = true;
            //myCollider.isTrigger = false;
            //rigidbody.isKinematic = false;
            myCollider.size = colliderFirstScale;

            posS = new List<Transform>();

            int childCount = other.transform.childCount-1;
            int index = 0;
            float distanceValue;
            while(index <= 3){
                posS.Add(other.gameObject.transform.GetChild(childCount).gameObject.transform);
                index++;
                childCount--;
            }
            targetPos = posS[0];
            distanceValue = Vector3.Distance(transform.position,posS[0].position);

            foreach(Transform pos in posS){
                if(Vector3.Distance(transform.position, pos.position) < distanceValue){
                    distanceValue = Vector3.Distance(transform.position, pos.position);
                    targetPos = pos;
                }
            }
            transform.parent = targetPos.transform;
            turretMovement.active = true;
            defaultScale = new Vector3(0.009999998f,0.009999999f,0.01f);
            transform.DOLocalMove(Vector3.zero,0.25f);
            myRender.material.DOColor(Color.red,0.25f).OnComplete( () => {
                myRender.material.DOColor(defaultColor,0.25f);
            });

            transform.DOScale(defaultScale * 1.3f,0.25f).OnComplete( () => {
                transform.DOScale(defaultScale,0.25f);
            });

            transform.DOLocalRotate(new Vector3(90,0,0),0.25f);
        }

        if(other.gameObject.CompareTag("Enemy")){
            Health--;
            Vector3 pos = other.gameObject.transform.position;
            Instantiate(enemyDestroyEffect,pos,Quaternion.identity);
            Rigidbody otherRigid = other.gameObject.GetComponent<Rigidbody>();
            otherRigid.AddExplosionForce(10f,transform.position,20f,3f,ForceMode.Impulse);
        }
        Debug.Log(PlayerCollision.turrets.Count);
    }

}
