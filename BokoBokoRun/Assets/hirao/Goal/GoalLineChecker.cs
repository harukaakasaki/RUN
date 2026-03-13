using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalLineChecker : MonoBehaviour
{
    //接続されているプレイヤーの数を数えるためのもの
    [SerializeField] private GameFlowManager m_GameFlowManager;
    [SerializeField] private InGameManager m_InGameManager;
    [SerializeField] private ResultManager m_resultManager;
    private int m_padNum;
   

    [SerializeField] int m_Rank = 0;//順位(どこかに渡す)
    bool isFinish = false;//このbool文がtrueになった時、カメラがScene遷移する

    //問題点
    //Playerが何人参加しているのかということを知りたい
    //どこに渡したらいいのかわからない
    //




    // Start is called before the first frame update
    void Start()
    {
        //最初に受け取る
        m_padNum = m_GameFlowManager.GetPadNum();
      
    }

    // Update is called once per frame
    void Update()
    {
        m_padNum = m_GameFlowManager.GetPadNum();
    }
    /// <summary>
    /// ゴールした人数
    /// </summary>
    public int GetGoalNum()
    {
       return m_Rank;
    }

  


    private void OnTriggerEnter(Collider other)
    {

        //Debug.Log("当たったよプレイヤーと%s",other.C);

        for(int i = 0; i < m_padNum; i++)
        {
         string playerTag = "Player" + (i + 1).ToString();
            if (other.CompareTag(playerTag))
            {
                m_Rank++;
                //このRankをPlayerに渡す
                Debug.Log("goalとplayerがあたったよ");
                //ゴールした分、生き残っている人数を減らす
                m_InGameManager.DecreaseAliveNum();
                if (m_Rank == 1)
                {
                    //1位のときの処理
                    //セット関数をセットして、そこに引数でplayerTagを渡す
                    Debug.Log("あなたの順位は" + m_Rank + "位です");

                    //m_resultManager
                    m_resultManager.SetBestPlayerTag(playerTag);

                }
                //ぶっ飛ばされた人の数
                 int verstNum = m_InGameManager.GetVerstNum();
                //Rankが最後の人までいったら、シーン遷移する
                if (m_Rank >= m_padNum - verstNum)//ぶっ飛ばされた人の数分を減らす
                {
                    isFinish = true;
                }
            }
        }

        //if (other.CompareTag("Player"))
        //{
        //    Rank++;
        //    //このRankをPlayerに渡す

        //    //Rankが最後の人までいったら、シーン遷移する
        //    if (Rank >= m_padNum)
        //    {
        //        isFinish = true;
        //    }

        //}
    }
}
