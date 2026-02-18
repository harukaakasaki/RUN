using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyFactory : MonoBehaviour
{
    //敵がスポーンする座標//追加していく//inspectorでいじれる機能があったはず、//Excelみたいに
    Vector3 point0 = new Vector3(21, 0.6f, 30);
    Vector3 point01 = new Vector3(21, 0.6f, 45);




    // Inspectorでアサインする敵のPrefab
    public GameObject enemyPrefab;

    // 指定位置に敵を生成するメソッド
    public GameObject SpawnEnemy(Vector3 position,bool anc)//boolはy軸反転するかどうか
    {
        if (enemyPrefab != null)
        {
            return Instantiate(enemyPrefab, position, anc ?Quaternion.identity : Quaternion.Euler(0, 180, 0));
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
        //if (Keyboard.current != null && Keyboard.current.enterKey.wasPressedThisFrame)
        //{
        //    SpawnEnemy(this.transform.position);
        //}

    }

    void OnTriggerEnter(Collider other)//子オブジェクトが当たり判定と当たったら、これが呼び出される
    {
        //SpawnID point = GetComponent<SpawnID>();
        //Debug.Log("壁に当たったよ。");
        //if (point != null)
        //{
        //   switch(point.spawnID)
        //    {
        //        case 0:
        //            SpawnEnemy(point0);
        //            Debug.Log("敵が生成されました。");
        //            break;
        //    }
        //}
    }
    public void OnChildernTrigger(int id)
    {
        switch (id)
        {
            case 0:
                SpawnEnemy(point0,true);
                SpawnEnemy(point01, false);
                Debug.Log("敵が生成されました。");
                break;
            default:
                break;
        }
    }
}
