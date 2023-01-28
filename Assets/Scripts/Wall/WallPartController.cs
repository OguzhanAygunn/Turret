using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WallPartController : MonoBehaviour
{
    Material myMaterial;
    Vector3 DefaultScale;
    [SerializeField] GameObject destroyEffect;
    Rigidbody rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody    = GetComponent<Rigidbody>();
        myMaterial   = GetComponent<MeshRenderer>().material;
        DefaultScale = transform.localScale;
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.CompareTag("Bullet") || other.gameObject.CompareTag("Turret")){
            rigidbody.mass = 0.01f;
            Color randomColor = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
            myMaterial.DOColor(randomColor,0.66f);
            transform.DOScale(DefaultScale*2f,0.66f).OnComplete( () => {
                Instantiate(destroyEffect,transform.position,Quaternion.identity);
                Destroy(this.gameObject);
            });
        }
    }
}
