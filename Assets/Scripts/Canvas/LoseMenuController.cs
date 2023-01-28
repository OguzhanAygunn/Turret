using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LoseMenuController : MonoBehaviour
{
    [SerializeField] Image Background;
    // Start is called before the first frame update
    void Start()
    {
        Background.color = Color.black;
        Background.DOColor(Color.clear,0.5f).SetDelay(0.5f);
    }

    public void activeFunction(){
        Background.DOColor(Color.black,0.5f).OnComplete( () =>{
            SceneManager.LoadScene(0);
        });
    }
}
