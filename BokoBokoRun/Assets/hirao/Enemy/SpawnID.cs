using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class SpawnID : MonoBehaviour
{
    // Start is called before the first frame update

    int coolTimer = 0;
    [SerializeField] int coolTime = 1000;

    //InGameManager
    [SerializeField] InGameManager m_ingameManager;

    //前のフレームで通った人数
   // private int prevThrouthCountNum = 0;

    //誰が通ったかのbool
    /// <summary>
    /// プレイヤーが通ったかどうか[0～3]
    /// </summary>
    private bool[] m_throuth;

    public int spawnID;//当たったら敵を場所によって、出現させるので、そのID
    void Start()
    {
        //GameFlowManagerコンポーネントを取得
        //m_ingameManager = FindObjectOfType<InGameManager>();
        //if (m_ingameManager == null)
        //{
        //    Debug.LogError("flowManager が見つかりません.");
        //}
        m_throuth = new bool[4];
        for (int i = 0; i < 4; i++)
        {
            m_throuth[i] = false;
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        coolTimer--;

        //TODO:
        //padの取得数をゲット
        //padの数、ぶっ飛ばされた数をもとに最後の人とあたったら敵を出す
        //
    }

    void OnTriggerEnter(Collider other)//子オブジェクトが当たり判定と当たったら、これが呼び出される
    {
        if (coolTimer > 0) return;
        const string name = "Player1";
        const string name2 = "Player2";
        const string name3 = "Player3";
        const string name4 = "Player4";

        if(!(other.CompareTag(name) || other.CompareTag(name2) || other.CompareTag(name3) || other.CompareTag(name4)))
        {
            return;//プレイヤー以外が当たったら、何もしない
        }

        //TODO
        //最後に当たったプレイヤーだからぶっ飛ばされて死んだプレイヤー、接続されているプレイヤーの数をもとに、敵を出すかどうかを決める
        //InGameManagerのm_aliveNumから取得

        int aliveNum = m_ingameManager.GetAliveNum();

        for(int i = 0; i < 4; i++)
        { 
            //Tagがあっていたら
             string playerTag = "Player" + (i + 1).ToString();
            if (other.CompareTag(playerTag))
            {
                m_throuth[i] = true;
                //そのtagの人が通ったとtrueにする→生き残っている人の数と同じになったら最後の人なので、敵を出す

            }
        }

        //m_throuthの中でtrueの数を数える→生き残っている人の数と同じになったら最後の人なので、敵を出す
        int trueCount = 0;
        for(int i = 0; i < 4; i++)
        {
           if (m_throuth[i]) trueCount++;
        }

        if (trueCount == aliveNum)
        {
        // 親のEnemyFactoryを取得して通知
        EnemyFactory factory = GetComponentInParent<EnemyFactory>();
        if (factory != null)
        {
            //生成のためのidを渡す
            factory.OnChildernTrigger(spawnID);
                //クールタイムをセットする
                coolTimer = coolTime;
        }
           // prevThrouthCountNum = trueCount;

        }
    }
}
