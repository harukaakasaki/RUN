using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalLineChecker : MonoBehaviour
{
    //接続されているプレイヤーの数を数えるためのもの
    [SerializeField] private GameFlowManager m_GameFlowManager;
    private int m_padNum;

    [SerializeField] int Rank = 0;//順位(どこかに渡す)
    bool isFinish = false;//このbool文がtrueになった時、カメラがScene遷移する

    //問題点
    //Playerが何人参加しているのかということを知りたい
    //どこに渡したらいいのかわからない
    //




    // Start is called before the first frame update
    void Start()
    {
        //最初に受け取る

    }

    // Update is called once per frame
    void Update()
    {
        m_padNum = m_GameFlowManager.GetPadNum();
    }

    private void OnTriggerEnter(Collider other)
    {

        for(int i = 0; i < m_padNum; i++)
        {
         string playerTag = "Player" + (i + 1).ToString();
            if (other.CompareTag(playerTag))
            {
                Rank++;
                //このRankをPlayerに渡す

                if(Rank == 1)
                {
                    //1位のときの処理
                    //セット関数をセットして、そこに引数でplayerTagを渡す


                }


                //Rankが最後の人までいったら、シーン遷移する
                if (Rank >= m_padNum)
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
