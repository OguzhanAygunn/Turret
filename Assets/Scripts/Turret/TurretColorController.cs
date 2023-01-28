using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class TurretColorController : MonoBehaviour
{
    private MeshRenderer myRender;
    // Start is called before the first frame update
    void Start()
    {
        myRender = transform.GetChild(1).gameObject.GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
    }

    private void ColorOperations(){

    }
}
