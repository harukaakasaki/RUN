using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseUIMove : MonoBehaviour
{
    // UI‚ðŠg‘åk¬‚³‚¹‚é

    float m_angle = 0;
    float m_rad = 0;
    float m_rotZ = 0;
    

   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        m_angle++;

        if(m_angle >= 180)
        {
            m_angle = 0;
        }
        m_rad = m_angle * Mathf.Deg2Rad;

        m_rotZ = Mathf.Sin(m_angle);

        // ”ÍˆÍ‚ð-0.5‚©‚ç0.5‚É‚·‚é
        m_rotZ -= 0.5f;

        this.transform.rotation = Quaternion.Euler(0, 0, m_rotZ);

        
    }
}
