using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyFactory : MonoBehaviour
{
    //敵がスポーンする座標
    Vector3 point0 = new Vector3(21, -1, 33);




    // Inspectorでアサインする敵のPrefab
    public GameObject enemyPrefab;

    // 指定位置に敵を生成するメソッド
    public GameObject SpawnEnemy(Vector3 position)
    {
        if (enemyPrefab != null)
        {
            return Instantiate(enemyPrefab, position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("Enemy Prefabがアサインされていません");
            return null;
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current != null && Keyboard.current.enterKey.wasPressedThisFrame)
        {
            SpawnEnemy(this.transform.position);
        }

    }

    void OnTriggerEnter(Collider other)//子オブジェクトが当たり判定と当たったら、これが呼び出される
    {
        SpawnID point = GetComponent<SpawnID>();

        if (point != null)
        {
           switch(point.spawnID)
            {
                case 0:
                    SpawnEnemy(point0);
                    Debug.Log("敵が生成されました。");
                    break;
            }
        }
    }
}
