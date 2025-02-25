﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuDiverController : MonoBehaviour
{
    bool m_bGo = false;
    [SerializeField] Vector3 m_v3Start = new Vector3(0,0,0);
    [SerializeField] Vector3 m_v3End = new Vector3(0, 0, 0);
    float m_fT;
    [SerializeField] float m_fSpeed = 0.0f;
    private AudioSource m_Footsteps;

    // Start is called before the first frame update
    void Start()
    {
        m_Footsteps = GetComponent<AudioSource>();
        m_Footsteps.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_bGo)
        {
            if (!m_Footsteps.isPlaying) m_Footsteps.Play();
            m_fT += m_fSpeed * Time.deltaTime;

            if(m_fT < 1)
            {
                transform.position = Vector3.Lerp(m_v3Start, m_v3End, m_fT); 

            }
        }

        // If diver has reached the edge of the boat
        if (m_fT > 1)
        {
            m_Footsteps.Stop();
            Animator a = GetComponent<Animator>();

            if (a != null)
            {
                a.SetBool("Run", false);
            }
        }
    }

    public void Go()
    {
        m_bGo = true;
        Animator a = GetComponent<Animator>();

        if(a != null)
        {
            a.SetBool("Run", true);
        }
    }
}
