using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BulletController : MonoBehaviour
{
    [SerializeField] GameObject EnemyCollEffect,WallCollEffect;
    Vector3 TargetScale;
    // Start is called before the first frame update
    void Start()
    {
        TargetScale = transform.localScale * 2f;
        transform.DOScale(TargetScale,1f).OnComplete( () => {
            Instantiate(EnemyCollEffect,transform.position,Quaternion.identity);
            Destroy(this.gameObject);
        });
    }


    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.CompareTag("Enemy")){
            Instantiate(EnemyCollEffect,transform.position,Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
