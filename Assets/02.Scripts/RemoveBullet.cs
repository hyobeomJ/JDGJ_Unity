using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveBullet : MonoBehaviour
{
    public GameObject sparkEffect;
    void OnCollisionEnter(Collision coll) 
    {
        if(coll.collider.CompareTag("BULLET"))
        {
            ContactPoint cp = coll.GetContact(0);
            Quaternion rot = Quaternion.LookRotation(-cp.normal);
            GameObject spark = Instantiate(sparkEffect,cp.point,rot);
            //Instantiate(sparkEffect,coll.transform.position,Quaternion.identity);
            Destroy(spark,0.5f);
            Destroy(coll.gameObject);
        }
        // if(coll.collider.tag == "BULLET")
        // {
        //     Destroy(coll.gameObject);
        // }
    }
}
