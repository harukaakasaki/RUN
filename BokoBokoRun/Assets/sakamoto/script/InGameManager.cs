using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InGameManager : GameManagerBase
{
    //接続されているプレイヤーの数を数えるためのもの
    [SerializeField] private GameFlowManager m_GameFlowManager;
    [SerializeField] private GoalLineChecker m_GoalLineChecker;
    [SerializeField] private targetMove m_TargetMove;
    private int m_padNum;
    private int m_aliveNum;//誰が生き残っているか


    private bool m_isEnd = false;   //ゲームが終了したか

    // Start is called before the first frame update
    void Start()
    {
        m_padNum = m_GameFlowManager.GetPadNum();//接続されているpadの数を取得
        m_aliveNum = m_padNum;//最初は全員生きている状態
    }

    // Update is called once per frame
    void FixedUpdate()
    {
#if UNITY_EDITOR
        UpdateDebug();
#endif

        //TODO:
        //ゴールした時、ぶっ飛ばされた時に呼ぶ関数を作る//完了
        //吹っ飛ばしandゴールした人が接続されている数とそろえばゲーム終了にする
        //このゲームに切り替わってから、カメラを動かし始める//完了

        //m_GoalLineChecker.GetGoalNum();

        //カメラを動かす
        m_TargetMove.MoveCamera(0.03f);

    }

    public bool IsEnd()
    {
        return m_isEnd;
    }

    public void DecreaseAliveNum()
    {
        m_aliveNum--;
        if (m_aliveNum <= 0)
        {
            //全員死んだときの処理
            //シーン遷移する
            OnEnd();
        }
        else
        {
            //まだ生きている人がいるときの処理
        }
    }

    private void UpdateDebug()
    {
        if (Keyboard.current.xKey.wasPressedThisFrame)
        {
            m_isEnd = true;
        }
    }

    public void OnEnd()
    {
        m_isEnd = true;
    }
}
