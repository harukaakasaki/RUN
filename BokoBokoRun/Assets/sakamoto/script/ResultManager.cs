using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class ResultManager : GameManagerBase
{
    private bool m_isBackTitle = false;//タイトルに戻るか

    private string m_bestPlayerTag;//一位になったプレイヤーのタグ
    

    private bool m_isAllLoser = false;//全員が負けたかどうか

    private GameObject[] m_players;//プレイヤーのオブジェクトを入れる配列

    [SerializeField] private GameObject m_winnerPos;//1位のプレイヤーの位置にあるオブジェクト
    [SerializeField] private GameObject[] m_loserPos;//2位以降のプレイヤーの位置にあるオブジェクト

    private GameObject m_winnerPlayer;//1位になったプレイヤーのオブジェクト
 

    private void OnEnable()
    {
        //全プレイヤーから1位になったプレイヤーのオブジェクトを取得
        for (int i = 0; i < m_players.Length; i++)
        {
            if (m_players[i].tag == "testWinner")
            {
                m_winnerPlayer = m_players[i];
            }
        }

        //勝ったプレイヤーをwinnerPosに移動させる
        m_winnerPlayer.transform.position = m_winnerPos.transform.position;
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
