using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class EnemyAController : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] int health,coinCount;
    [SerializeField] GameObject MiniEnemy,DestroyEffect,Coin;
    private int Health{
        get{
            return health;
        }
        set{
            health = value;
            tmpro.text = (health >= 0) ? health.ToString() : "0";
            if(health == 0){
                if(!ScaleDown){
                    ScaleDown = true;
                    transform.DOScale(Vector3.zero,0.33f).OnComplete( () => {
                        Instantiate(DestroyEffect,transform.position,Quaternion.identity);
                        StartCoroutine(DeathFunction());
                    });
                }
            }
        }
    }
    [SerializeField] Material fontMaterial;
    Transform target;
    MeshRenderer TMPRender;
    GameObject TextObj;
    TextMeshPro tmpro;
    bool ScaleUp,ScaleDown;
    Vector3 upScale,DefaultScale;
    // Start is called before the first frame update
    private void Awake() {
        Vector3 pos = transform.position;
        pos.y = 80f;
        transform.position = pos;
    }
    void Start()
    {
        TextObj = transform.GetChild(0).gameObject;
        TMPRender = TextObj.GetComponent<MeshRenderer>();
        TMPRender.material = fontMaterial;
        tmpro = TMPRender.GetComponent<TextMeshPro>();
        tmpro.text = health.ToString();
        DefaultScale = transform.localScale;
    }

    public Vector3 MiniEnemyPos()
    {
        Vector3 pos = Random.insideUnitSphere*0.5f;
        Vector3 newPos = transform.position + pos;
        newPos.y = transform.position.y + 1.5f;
        return newPos;
    }

    public Vector3 CoinSpawnPos()
    {
        Vector3 pos = Random.insideUnitSphere*0.1f;
        Vector3 newPos = transform.position + pos;
        newPos.y = transform.position.y + .5f;
        return newPos;
    }

    IEnumerator DeathFunction(){
        Destroy(this.gameObject);
        while(coinCount > 0){
            Instantiate(Coin,CoinSpawnPos(),Quaternion.identity);
            coinCount--;
            yield return new WaitForSeconds(0.05f);
        }
        yield return null;
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.CompareTag("Bullet") && health >= 0){
            if(ScaleUp == false){
                ScaleUp = true;
                upScale = transform.localScale * 1.1f;
                transform.DOScale(upScale,0.05f).OnComplete( () => {
                    transform.DOScale(DefaultScale,0.05f).OnComplete( () => {
                        ScaleUp = false;
                    });
                });
            }
            Health--;

            Instantiate(MiniEnemy,MiniEnemyPos(),Quaternion.identity);
        }
    }


}
