using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerColorController : MonoBehaviour
{
    Material material;
    [SerializeField] Color DeathColor;
    [SerializeField] Ease ease;
    // Start is called before the first frame update
    void Start()
    {
        material = transform.GetChild(1).gameObject.GetComponent<MeshRenderer>().material;
    }

    public void deathFunction(){
        material.DOColor(DeathColor,1f).SetEase(ease).SetDelay(1f);
    }
}
