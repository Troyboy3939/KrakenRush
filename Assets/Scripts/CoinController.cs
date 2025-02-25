﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CoinController : MonoBehaviour
{
    BoxCollider col;
    [SerializeField] private float m_fGravityMultiplier = 0;
    [HideInInspector] public bool m_bHeld = false;

    public void SetHeld(bool bHeld)
    {
        m_bHeld = bHeld;
    }

    private void Start()
    {
        col = GetComponent<BoxCollider>();
        Rigidbody rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        GetComponent<Rigidbody>().AddForce(m_fGravityMultiplier * Physics.gravity);
    }

    private void Update()
    {
        if (transform.position.y < 2.35f) transform.position = new Vector3(transform.position.x, 2.35f, transform.position.z);
    }

    private void OnCollisionEnter(Collision other)
    {
        
        // If touching a player that is not stunned
        if (other.transform.tag == "Player")
        {
            PlayerController pc = other.gameObject.GetComponent<PlayerController>();

            if (!m_bHeld && !pc.m_bHasCoin && !pc.GetStunned() && pc.m_bCanPickUpCoin)
            {
                pc.m_bCanPickUpCoin = false;
                pc.SetHasCoin(true);
                transform.SetParent(other.transform);
                transform.Translate(new Vector3(0, 1, 0));



                MeshRenderer[] m = GetComponentsInChildren<MeshRenderer>();


                for(int i = 0; i < m.Length; i++)
                {
                    m[i].enabled = false;
                }
                


                Rigidbody rb = GetComponent<Rigidbody>();
                rb.constraints = RigidbodyConstraints.FreezePositionY;

                
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;

                BoxCollider bc = GetComponent<BoxCollider>();
                bc.enabled = false;
                m_bHeld = true;
            }
        }
    }
}
