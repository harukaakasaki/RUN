using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.InputSystem;

static class Constants
{
       public const float kEnemyeSpawnZ = 31.0f;
}
public class EnemyFactory : MonoBehaviour
{

    //敵がスポーンする座標//追加していく//inspectorでいじれる機能があったはず、//Excelみたいに
    Vector3 point0 = new Vector3(53.5f, 0.6f, Constants.kEnemyeSpawnZ);
    Vector3 point01 = new Vector3(53.5f, 0.6f, 42);
    Vector3 point1 = new Vector3(104.5f, 0.6f, Constants.kEnemyeSpawnZ);
    Vector3 point11 = new Vector3(104.5f, 0.6f, 42);
    Vector3 point2 = new Vector3(153.5f, 0.6f, Constants.kEnemyeSpawnZ);
    Vector3 point21 = new Vector3(153.5f, 0.6f, 42);
    Vector3 point3 = new Vector3(204.5f, 0.6f, Constants.kEnemyeSpawnZ);
    Vector3 point31 = new Vector3(204.5f, 0.6f, 42);
    Vector3 point4 = new Vector3(264.5f, 0.6f, Constants.kEnemyeSpawnZ);
    Vector3 point41 = new Vector3(261.5f, 0.6f, 42);
    Vector3 point5 = new Vector3(306.5f, 0.6f, Constants.kEnemyeSpawnZ);
    Vector3 point51 = new Vector3(306.5f, 0.6f, 42);
    Vector3 point6 = new Vector3(357.5f, 0.6f, Constants.kEnemyeSpawnZ);
    Vector3 point61 = new Vector3(357.5f, 0.6f, 42);
    Vector3 point7 = new Vector3(408.5f, 0.6f, Constants.kEnemyeSpawnZ);
    Vector3 point71 = new Vector3(408.5f, 0.6f, 42);

    
    // Inspectorでアサインする敵のPrefab
    public GameObject enemyPrefab;

    [SerializeField] private onEffectManager m_efManager;
    //スポーンSEのクールタイムFixedUpdateでカウントして、これ以上ならSEを鳴らす、みたいな感じで
    [SerializeField] private int summonSeCooldownFrames = 100;
    //エフェクトを出す位置
    private Vector3 m_effectPos;

    private float m_soundCoolTime;

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

    private void FixedUpdate()
    {
        //クールタイム
        m_soundCoolTime++;
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
        //if (Keyboard.current != null && Keyboard.current.jKey.wasPressedThisFrame)
        //{
        //    string name = "Spawn";
        //    //エフェクトを出す
        //    m_efManager.PlayEffect(transform.position, name);
        //    Debug.Log("エフェクトを押したよ");
        //}

        // Player1〜4 のどれか
        bool isAnyPlayer =
            other.CompareTag("Player1") ||
            other.CompareTag("Player2") ||
            other.CompareTag("Player3") ||
            other.CompareTag("Player4");




        // Player 以外は無視
        if (!isAnyPlayer) return;

        // クールタイム中は無視
        if (m_soundCoolTime < summonSeCooldownFrames) return;
        // SE
        if (SoundManager.Instance != null && SoundManager.Instance.SummonSE != null)
        {
            SoundManager.Instance.PlaySE(SoundManager.Instance.SummonSE);
        }

        // エフェクト
        if (m_efManager != null)
        {
            Vector3 pos = other.ClosestPoint(transform.position);
            m_efManager.PlayEffect(pos, "Spawn");
        }

        Debug.Log("スポーン演出（SE/エフェクト）を再生しました。");

        // クールタイムをリセット
        m_soundCoolTime = 0;
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
