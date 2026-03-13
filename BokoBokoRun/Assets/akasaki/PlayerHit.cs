using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    // プレイヤーがヒットして天井のコライダーに当たったら位置を移動する

    void OnCollisionEnter(Collision other)
    {
        Debug.Log("あたった！！！！");
    }
    private void OnTriggerEnter(Collider other)
    {
        
    }
}
