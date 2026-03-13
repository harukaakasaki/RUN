using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHit : MonoBehaviour
{


    void OnCollisionEnter(Collision other)
    {
        Debug.Log("当たった！！！！");
    }

}
