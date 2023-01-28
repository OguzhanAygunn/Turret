using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] FloatingJoystick fj;
    [SerializeField] float MoveSpeed;
    Rigidbody rigidbody;
    Transform finishTransform;
    Vector3 finishPos;
    WinMenuController winMenuController;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        finishTransform = GameObject.FindGameObjectWithTag("FinishGround").gameObject.transform;
        finishPos = finishTransform.position;
        finishPos.y += finishTransform.localScale.y;
        winMenuController = GameObject.FindObjectOfType<WinMenuController>();

    }

    // Update is called once per frame
    void Update()
    {
        MovementOperations();
    }

    void MovementOperations(){
        if(!GameManager.GameWin){
            if((Input.touchCount > 0 || (fj.Vertical != 0 || fj.Horizontal != 0)) && !GameManager.GameLose){
                float exSpeed = PlayerPrefs.GetFloat("Slot1Value");
                transform.position += new Vector3(fj.Horizontal,0,fj.Vertical) * MoveSpeed * Time.deltaTime * exSpeed;
            }
        }
    }

    public void AddExplosionFunc(){
        Collider[] colliders = Physics.OverlapSphere(transform.position,20);
        foreach(var collider in colliders){
            Rigidbody rb = collider.gameObject.GetComponent<Rigidbody>();
            if(rb != null){
                Debug.Log("x");
                rb.AddExplosionForce(10f,transform.position,20f,3f,ForceMode.Impulse);
                GameManager.GameLose = true;
            }
        }
    }

    public void FinishMoveActive(){
        fj.gameObject.SetActive(false);
        rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        transform.DOMove(finishPos,1).OnComplete( () => {
            rigidbody.mass = 1;
            rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
            Vector3 scale = transform.localScale;
            transform.DOScale(scale*2.4f,0.5f).OnComplete( () => {
            }).OnComplete( () => {
                transform.DOScale(scale,0.5f);
            }).OnComplete( () => {
                winMenuController.ActiveFunction();
            });
        });
    }
}
