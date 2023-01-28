using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public static List<GameObject> turrets = new List<GameObject>();
    PlayerMovement playerMovement;
    WinMenuController winMenuController;
    CoinPanelController cpc;
    CameraController cameraController;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.PlayerDestroyable = false;
        playerMovement = GetComponent<PlayerMovement>();
        winMenuController = GameObject.FindObjectOfType<WinMenuController>();
        cpc = GameObject.FindObjectOfType<CoinPanelController>();
        cameraController = Camera.main.gameObject.GetComponent<CameraController>();
    }

    private void FixedUpdate() {
        if(GameManager.FirstColl && !GameManager.GameLose){
            if(GameObject.FindGameObjectsWithTag("TakeTurret").Length == 0){
                cameraController.GameDeathController(true);
            }
        }
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.CompareTag("FinishGround") && !GameManager.GameWin){
            GameManager.GameWin = true;
            playerMovement.FinishMoveActive();
            cpc.DeActiveFunction();
        }
    }
}