using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CoinController : MonoBehaviour
{
    Rigidbody rigidbody;
    Vector3 torquePower,velocityPower;
    Transform TargetPos;
    [SerializeField] float time;
    Collider myColl;
    bool isColl;
    CoinPanelController coinPanelController;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        torquePower = new Vector3(Random.Range(150,360),Random.Range(150,360),Random.Range(150,360));
        velocityPower = new Vector3(Random.Range(-3,4),Random.Range(3,6),Random.Range(-3,4));
        rigidbody.AddTorque(torquePower);
        rigidbody.velocity = velocityPower;
        myColl = GetComponent<Collider>();
        TargetPos = GameObject.FindGameObjectWithTag("CoinTargetPos").gameObject.transform;
        coinPanelController = GameObject.FindObjectOfType<CoinPanelController>();
        //Invoke("ToTargetPos",1f);
    }

    private void FixedUpdate() {
        moveOperations();
    }

    void moveOperations(){
        if(isColl){
            transform.position = Vector3.MoveTowards(transform.position,TargetPos.position,20f*Time.deltaTime);
            if(transform.position == TargetPos.position){
                coinPanelController.Coin++;
                Destroy(this.gameObject);
            }
        }
    }

    public void ToTargetFunc(){
        transform.DORotate(Vector3.zero,1f);

        transform.DOMove(TargetPos.position,time).OnComplete( () => {
            Destroy(this.gameObject);
        });
    }

    void CollFunction(){
        Destroy(rigidbody);
        Destroy(myColl);
        isColl = true;
        transform.DORotate(Vector3.zero,1f);
    }

    private void OnCollisionEnter(Collision other) {
        string tag = other.gameObject.tag;
        if((tag == "Player" || tag == "TakeTurret") && !isColl){
            CollFunction();
        }
    }

    private void OnTriggerEnter(Collider other) {
        string tag = other.gameObject.tag;
        if((tag == "Player" || tag == "TakeTurret") && !isColl){
            CollFunction();
        }
    }
}
