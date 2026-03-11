using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class ResultManager : GameManagerBase
{
    private bool m_isBackTitle = false;//タイトルに戻るか

    //勝っているプレイヤーがいたときに使うもの
    private string m_bestPlayerTag;//一位になったプレイヤーのタグ
    [SerializeField] private GameObject[] m_winAnimPlayers;//勝った時のアニメーションを行っているプレイヤーの配列
    [SerializeField] private GameObject m_winnerPosObj;//1位のプレイヤーの位置にあるオブジェクト

    //全員が負けた時に使用するもの


    //どちらの場合でも使う
    private bool m_isAllLoser = false;//全員が負けたかどうか
    [SerializeField] private GameObject[] m_loseAnimPlayers;//負けた時のアニメーションを行っているプレイヤーの配列
    [SerializeField] private GameObject[] m_loserPosObj;//2位以降のプレイヤーの位置にあるオブジェクト

    private GameObject m_winnerPlayer;//1位になったプレイヤーのオブジェクト


    private void OnEnable()
    {
        //勝者がいた場合の処理
        if (!m_isAllLoser)
        {
            //すべての勝ちアニメーションを持つオブジェクトのタグと
            //勝ったプレイヤーのタグを照らし合わせる
            for (int i = 0; i < m_winAnimPlayers.Length; i++)
            {
                //照らし合わせる
                if (m_winAnimPlayers[i].tag == m_bestPlayerTag)
                {
                    //同じだったら配列に入っているオブジェクトの位置を
                    //事前に設定されてある勝者の位置に移動させる
                    m_winAnimPlayers[i].transform.position = m_winnerPosObj.transform.position;
                }
                else
                {
                    //それが負けたプレイヤーのタグだった場合
                    m_loseAnimPlayers[i].transform.position = m_loserPosObj[i].transform.position;
                }
            }
        }
        //すべてのプレイヤーが負けていた場合
        else
        {
            for (int i = 0; i < m_loseAnimPlayers.Length; i++)
            {
                m_loseAnimPlayers[i].transform.position = m_loserPosObj[i].transform.position;
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR 
        Debug();
#endif
    }

    public bool IsBackTitle()
    {
        return m_isBackTitle;
    }

    private void Debug()
    {
        if (Keyboard.current.cKey.wasPressedThisFrame)
        {
            m_isBackTitle = true;
        }
    }

    /// <summary>
    /// 勝ったプレイヤーのタグをセットする関数
    /// </summary>
    /// <param name="bestPlayer"></param>
    public void SetBestPlayerTag(string bestPlayer)
    {
        m_bestPlayerTag = bestPlayer;
    }

    /// <summary>
    /// すべてのプレイヤーが死んだ時
    /// </summary>
    public void OnAllLose()
    {
        m_isAllLoser = true;
    }
}
