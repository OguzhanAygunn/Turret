using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraController : MonoBehaviour
{
    private Transform playerPos;
    [SerializeField] float FollowSpeed;
    [SerializeField] Vector3 posOffset,lookOffset,LoseOffset,targetOffset;
    Vector3 myVector;
    PlayerMovement playerMovement;
    PlayerScaleController playerScaleController;
    PlayerColorController playerColorController;
    DeathEffectController deathEffectController;
    LoseMenuController loseMenuController;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Start();
        DOTween.To(()=> targetOffset, x=> targetOffset = x, posOffset, 1);
        playerPos = GameObject.FindGameObjectWithTag("Player").gameObject.transform;
        playerMovement = playerPos.gameObject.GetComponent<PlayerMovement>();
        playerScaleController = playerPos.gameObject.GetComponent<PlayerScaleController>();
        playerColorController = playerPos.gameObject.GetComponent<PlayerColorController>();
        deathEffectController = transform.GetChild(2).gameObject.GetComponent<DeathEffectController>();
        loseMenuController    = FindObjectOfType<LoseMenuController>();
    }   

    // Update is called once per frame
    private void LateUpdate()
    {
        FollowOperations();
        LookatOperations();
    }

    void FollowOperations(){
        transform.position = Vector3.MoveTowards(transform.position,playerPos.position + targetOffset, FollowSpeed*Time.deltaTime);
    }
    
    void LookatOperations(){
        transform.LookAt(playerPos.position + lookOffset);
    }

    public void GameDeathController(bool x){
        bool y = false;

        foreach(GameObject obj in PlayerCollision.turrets){
            if(obj){y = true;}
        }

        if((y == false && !GameManager.GameLose) || x){
            deathEffectController.EffectActive(true);
            DOTween.To(()=> targetOffset, x=> targetOffset = x, LoseOffset, 2);
            playerColorController.deathFunction();
            GameManager.GameLose = true;
            Camera.main.DOShakePosition(1,1);
            playerMovement.AddExplosionFunc();
            StartCoroutine(loseMenuActive());
        }
    }

    IEnumerator loseMenuActive(){
        yield return new WaitForSeconds(2f);
        loseMenuController.activeFunction();
    }
}
