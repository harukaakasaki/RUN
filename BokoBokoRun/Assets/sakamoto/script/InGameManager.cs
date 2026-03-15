using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Experimental.GraphView.GraphView;

public class InGameManager : GameManagerBase
{
    static class Constants
    {
        public const int kCountDownFrame = 150;
    }

    //接続されているプレイヤーの数を数えるためのもの
    [SerializeField] private GameFlowManager m_GameFlowManager;
    [SerializeField] private GoalLineChecker m_GoalLineChecker;
    [SerializeField] private targetMove m_TargetMove;
    //ゲームシーンのプレイヤーのスポーン位置
    [SerializeField] Transform[] m_spawnPositionsOfGame;
    //ゲームシーンのカメラ
    [SerializeField] CinemachineVirtualCamera m_Gamecam;
    //カメラの元々のFollowのゲームオブジェクト
    [SerializeField] private GameObject m_targetCamera;
    //新しいFollowのゲームオブジェクト
    [SerializeField] private GameObject m_newTargetCamera;
    //カメラのFollowが変わったときの旗
    bool m_isZoom = false;
    private int m_CameraFrame = 0;//カメラのFollowを変えたときのフレーム

    private int m_frame = 0;//最初のカウントダウン用のフレーム
    private bool m_isCanMove = false;//カウントダウン中動けないようにするためのフラグ

    // 参加したプレイヤーのリスト
    private List<PlayerInput> m_joinedPlayers = new List<PlayerInput>();

    private int m_padNum;
    private int m_aliveNum;     //誰が生き残っているか
    private int prevVerstNum;   //前フレームで死んだ人の数
    private int nowVerstNum;    //現在フレームで死んだ人の数

    private bool m_isEnd = false;   //ゲームが終了したか

    // Start is called before the first frame update
    void Start()
    {
        m_padNum = m_GameFlowManager.GetPadNum();//接続されているpadの数を取得
        m_aliveNum = m_padNum;//最初は全員生きている状態

        prevVerstNum = 0;
        nowVerstNum = 0;
        SoundManager.Instance.PlayBGM(SoundManager.Instance.InGameBGM);
    }

    private void OnEnable()
    {
        //カウントダウン用のフレームを代入
        m_frame = Constants.kCountDownFrame;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
#if UNITY_EDITOR
        UpdateDebug();
#endif
        //カメラがZoom中の時、時間経過で元に戻す処理
        if(m_isZoom)
        {
            m_CameraFrame++;
            if (m_CameraFrame > 100)
            {
                m_Gamecam.Follow = m_targetCamera.transform;
                Time.timeScale = 1f;
                m_isZoom = false;
            }
        }


        //ゲームシーンになったとき & カメラがゴールを見た後にフレームを減らす
        if (m_GameFlowManager.GetNowScene() == GameFlowManager.Scene.InGame &&
            m_GameFlowManager.IsBackCamera())
        {
            m_frame--;

            //kCountDownFrameたったら動けるようにする
            if (m_frame < 0)
            {
                m_isCanMove = true;
            }
        }


        //TODO:
        //ゴールした時、ぶっ飛ばされた時に呼ぶ関数を作る//完了
        //吹っ飛ばしandゴールした人が接続されている数とそろえばゲーム終了にする
        //このゲームに切り替わってから、カメラを動かし始める//完了

        //m_GoalLineChecker.GetGoalNum();

        //カメラを動かす
        m_TargetMove.MoveCamera(0.07f);

        prevVerstNum = nowVerstNum;

        nowVerstNum = CheckPlayersAlive();//死んだ人の数
        //前のフレームよりもバーストされた人が増えたら生きている数の人を減らす
        if (prevVerstNum != nowVerstNum)
        {
            DecreaseAliveNum();//人が減る処理
        }
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


    /// <summary>
    /// GameSceneに切り替わったとき、プレイヤーの位置をゲームシーンのスポーン位置へ移動させる
    /// </summary>
    public void SetInGamePlayers()
    {
        Debug.Log("プレイヤーをInGameに移動中です");
        for (int i = 0; i < m_joinedPlayers.Count; i++)
        {
            var input = m_joinedPlayers[i];
            int index = input.playerIndex;//プレイヤーの通し番号を取得

            //スポーン位置へ移動
            input.transform.position = m_spawnPositionsOfGame[index].position;
            input.transform.rotation = m_spawnPositionsOfGame[index].rotation;
            Debug.Log("プレイヤーの位置をゲームシーンへ移動");
        }
    }

    public void SetPlayerInput(List<PlayerInput> list)
    {
        m_joinedPlayers = list;
        Debug.Log("プレイヤーのリストを受け取ったよ");
    }
    /// <summary>
    /// 現在死んでいる人の数
    /// </summary>
    /// <returns></returns>
    public int GetVerstNum()
    {
        return nowVerstNum;
    }
    /// <summary>
    /// 現在プレイ中の人の数
    /// </summary>
    /// <returns></returns>
    public int GetAliveNum()
    {
        return m_aliveNum;

    }

    private int CheckPlayersAlive()
    {
        int VerstNum = 0;

        for (int i = 0; i < m_joinedPlayers.Count; i++)
        {
            var input = m_joinedPlayers[i];
            int index = input.playerIndex;//プレイヤーの通し番号を取得

            //ぶっ飛ばされた数を把握
            var m_playerScr = input.GetComponent<Player>();

           if(m_playerScr.GetNoActive())
            {
                VerstNum++;
            }
        }
        return VerstNum;
    }

    public bool IsCanMove()
    {
        return m_isCanMove;
    }
    public void SetCameraZoomTrigger(Vector3 pos)
    {
        //調整
        pos = new Vector3(pos.x, pos.y + 3, pos.z);

        // m_Gamecam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset = new Vector3(0, -3, -8);
        //カメラのFollowを変える//その前にFollowを保存する//最初に
        //すうびょうしたら元に戻す//そのための旗がいる
        //もらった座標に新しいターゲットを置く
        m_newTargetCamera.transform.position = pos;
        m_Gamecam.Follow = m_newTargetCamera.transform;
        //時間を遅くする
        Time.timeScale = 0.1f;
        m_isZoom = true;


    }
}
