using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyFactory : MonoBehaviour
{
    //敵がスポーンする座標//追加していく//inspectorでいじれる機能があったはず、//Excelみたいに
    Vector3 point0 = new Vector3(53.5f, 0.6f, 28);
    Vector3 point01 = new Vector3(53.5f, 0.6f, 42);
    Vector3 point1 = new Vector3(104.5f, 0.6f, 28);
    Vector3 point11 = new Vector3(104.5f, 0.6f, 42);
    Vector3 point2 = new Vector3(153.5f, 0.6f, 28);
    Vector3 point21 = new Vector3(153.5f, 0.6f, 42);
    Vector3 point3 = new Vector3(204.5f, 0.6f, 28);
    Vector3 point31 = new Vector3(204.5f, 0.6f, 42);
    Vector3 point4 = new Vector3(251.5f, 0.6f, 28);
    Vector3 point41 = new Vector3(251.5f, 0.6f, 42);
    Vector3 point5 = new Vector3(306.5f, 0.6f, 28);
    Vector3 point51 = new Vector3(306.5f, 0.6f, 42);
    Vector3 point6 = new Vector3(357.5f, 0.6f, 28);
    Vector3 point61 = new Vector3(357.5f, 0.6f, 42);
    Vector3 point7 = new Vector3(408.5f, 0.6f, 28);
    Vector3 point71 = new Vector3(408.5f, 0.6f, 42);

    
    // Inspectorでアサインする敵のPrefab
    public GameObject enemyPrefab;

    //エフェクト呼び出す
    private onEffectManager m_efManager;
    //エフェクトを出す位置
    private Vector3 m_effectPos;

    // 指定位置に敵を生成するメソッド
    public GameObject SpawnEnemy(Vector3 position,bool anc)//boolはy軸反転するかどうか
    {
        if (enemyPrefab != null)
        {
            GameObject enemy = Instantiate(enemyPrefab, position, anc ? Quaternion.identity : Quaternion.Euler(0, 180, 0));
            enemy.GetComponent<enemyMove>().SetStateEnemy();
            return enemy;

           

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
        //GameFlowManagerコンポーネントを取得
        m_efManager = FindObjectOfType<onEffectManager>();
        if (m_efManager == null)
        {
            Debug.LogError("onEffectManager が見つかりません.");
        }
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

        //敵と当たったらエフェクトを出す
        string name = "Spawn";
        //エフェクトを出す
        m_efManager.PlayEffect(transform.position, name);
        Debug.Log("エフェクトを出しました");

    }
    public void OnChildernTrigger(int id)
    {
        switch (id)
        {
            case 0:
                SpawnEnemy(point0,true);
                SpawnEnemy(point01, false);
                break;
            case 1:
                SpawnEnemy(point1,true);
                SpawnEnemy(point11, false);
                break;
            case 2:
                SpawnEnemy(point2,true);
                SpawnEnemy(point21, false);
                break;
            case 3:
                SpawnEnemy(point3,true);
                SpawnEnemy(point31, false);
                break;
            case 4:
                SpawnEnemy(point4,true);
                SpawnEnemy(point41, false);
                break;
            case 5:
                SpawnEnemy(point5,true);
                SpawnEnemy(point51, false);
                break;
            case 6:
                SpawnEnemy(point6,true);
                SpawnEnemy(point61, false);
                break;
            case 7:
                SpawnEnemy(point7,true);
                SpawnEnemy(point71, false);
                break;
            default:
                break;
        }
    }
}
