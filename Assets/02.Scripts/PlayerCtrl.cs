using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    private Transform tr;
    private Animation anim;
    public float moveSpeed = 10.0f;
    public float turnSpeed = 80.0f;

    private readonly float initHp = 100.0f;
    public float currHp;
    public delegate void PlayerDieHandler();
    public static event PlayerDieHandler OnPlayerDie;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        currHp = initHp;
        tr = GetComponent<Transform>();
        anim = GetComponent<Animation>();

        anim.Play("Idle");

        turnSpeed = 0.0f;

        yield return new WaitForSeconds(0.3f);
        turnSpeed = 80.0f;
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        float r = Input.GetAxis("Mouse X");

        //Debug.Log("h="+h);
        //Debug.Log("v="+v);

        //transform.position += new Vector3(0,0,1);
        //tr.Translate(Vector3.forward * Time.deltaTime * v * moveSpeed);

        Vector3 moveDir = (Vector3.forward * v) + (Vector3.right*h);
        tr.Translate(moveDir.normalized*moveSpeed * Time.deltaTime);
        tr.Rotate(Vector3.up * turnSpeed * Time.deltaTime* r);

        playerAime(h,v);
    }

    void playerAime(float h, float v)
    {
        if(v>=0.1f)
        {
            anim.CrossFade("RunF",0.25f);
        }
        else if(v<= -0.1f)
        {
            anim.CrossFade("RunB", 0.25f);
        }
        else if(h>= 0.1f)
        {
            anim.CrossFade("RunR", 0.25f);
        }
        else if(h<= -0.1f)
        {
            anim.CrossFade("RunL", 0.25f);
        }
        else
        {
            anim.CrossFade("Idle",0.25f);
        }
    }

    void OnTriggerEnter(Collider coll) 
    {
        if(currHp >= 0.0f && coll.CompareTag("PUNCH"))
        {
            currHp -= 10.0f;
            Debug.Log($"Player hp = {currHp/initHp}");

            if(currHp <= 0.0f)
            {
                PlayerDie();
            }
        }
    }
    
    void PlayerDie()
    {
        Debug.Log("Player Die !");

        // GameObject[] monsters = GameObject.FindGameObjectsWithTag("MONSTER");

        // foreach(GameObject monster in monsters)
        // {
        //     monster.SendMessage("OnPlayerDie", SendMessageOptions.DontRequireReceiver);
        // }

        OnPlayerDie();
    }
}
