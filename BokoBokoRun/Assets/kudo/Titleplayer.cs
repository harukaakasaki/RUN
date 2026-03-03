using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Titleplayer : MonoBehaviour
{
    Vector3 m_pos;
    // Start is called before the first frame update
    void Start()
    {
        //ˆÊ’u‚ð•Û‘¶
        m_pos = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        //‘O•ûŒü‚ÉˆÚ“®
        m_pos.x += 1.0f;
    }
}
