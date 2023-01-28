using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class GroundFirstOperationsController : MonoBehaviour
{
    Vector3 firstPos,firstScale,firstRotate;
    GameObject parent;
    // Start is called before the first frame update



    void Awake()
    {
        parent = transform.parent.gameObject;
        transform.parent = null; 


        firstPos = transform.position;
        firstScale = transform.localScale;
        firstRotate = transform.localEulerAngles;


        Debug.Log(firstRotate);

        transform.eulerAngles = new Vector3(Random.Range(-360,+361),Random.Range(-360,+361),Random.Range(-360,+361));
        transform.position += firstPos + new Vector3(Random.Range(-15,+16),Random.Range(-5,+6),0);

        transform.DOMove(firstPos,2f).SetDelay(1f);
        transform.DORotate(firstRotate,2f).SetDelay(1f).OnComplete( () => {
            transform.parent = parent.transform;
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
