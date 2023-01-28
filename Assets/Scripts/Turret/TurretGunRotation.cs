using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TurretGunRotation : MonoBehaviour
{
    [SerializeField] Transform FirePos;
    public Transform enemyPos;
    [SerializeField] GameObject bullet;
    [SerializeField] float bulletSpawnTime,bulletPower,radius;
    [SerializeField] private LayerMask enemyLayer;
    Collider[] hitColliders = new Collider[1];
    private GameObject Cylinder;
    Vector3 DefaultScale;
    TurretCollision turretCollision;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(BulletSpawnerFunc());
        Cylinder = transform.GetChild(0).gameObject;
        DefaultScale = transform.localScale;
        turretCollision = transform.parent.gameObject.GetComponent<TurretCollision>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        LookatOperation();

        if(enemyPos == null){
            hitColliders = Physics.OverlapSphere(transform.position, radius * PlayerPrefs.GetFloat("Slot3Value"), enemyLayer);
               foreach(var obj in hitColliders){
                enemyPos = obj.transform;
            }
        }
    }

    IEnumerator BulletSpawnerFunc(){
        while(true){
            if(enemyPos != null && turretCollision.isColl){
                GameObject bullet_ = Instantiate(bullet,FirePos.position,transform.rotation);
                Rigidbody bulletRigid = bullet_.GetComponent<Rigidbody>();
                bulletRigid.velocity = transform.TransformDirection(Vector3.forward * bulletPower);
            }
            yield return new WaitForSeconds(bulletSpawnTime / PlayerPrefs.GetFloat("Slot2Value"));
        }
    }

    void LookatOperation(){
        if(enemyPos){
            transform.LookAt(enemyPos.position);
            Vector3 localeuler = transform.localEulerAngles;
            localeuler.x = 0;
            localeuler.z = 0;
            transform.localEulerAngles = localeuler;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(enemyPos == null && other.CompareTag("Enemy")){
            enemyPos = other.gameObject.transform;
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.CompareTag("Enemy") && other.gameObject.transform == enemyPos){
            enemyPos = null;
            //transform.DOLocalRotate(new Vector3(Random.Range(-360,360),Random.Range(-360,360),Random.Range(-360,360),0.3f));
        }
    }
}
