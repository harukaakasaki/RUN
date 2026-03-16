using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultManager : GameManagerBase
{
    [SerializeField] Image winUI;
    [SerializeField] Image loseUI;

    private bool m_isBackTitle = false;//タイトルに戻るか

    [SerializeField] private GameFlowManager m_gameFlowManager;
    [SerializeField] private SelectManager m_selectManager;

    private GameObject[] m_players;//ゲームに参加中のプレイヤーの配列

    //勝っているプレイヤーがいたときに使うもの
    private string m_bestPlayerTag;//一位になったプレイヤーのタグ
    [SerializeField] private GameObject[] m_winAnimPlayers;//勝った時のアニメーションを行っているプレイヤーの配列
    [SerializeField] private GameObject m_winnerPosObj;//1位のプレイヤーの位置にあるオブジェクト

    //全員が負けた時に使用するもの


    //どちらの場合でも使う
    private bool m_isAllLoser = false;//全員が負けたかどうか
    [SerializeField] private GameObject[] m_loseAnimPlayers;//負けた時のアニメーションを行っているプレイヤーの配列
    [SerializeField] private GameObject[] m_loserPosObj;//2位以降のプレイヤーの位置にあるオブジェクト
    //カメラ
    [SerializeField] private CinemachineVirtualCamera m_vcam;

    private void OnEnable()
    {
        //現在ゲーム内にいるプレイヤーをタグで検索して
        //それをプレイヤーの配列に突っ込む
        int playerNum = m_selectManager.GetJoinedPlayers().Count;//現在接続中のパッドのタグを取得

        //プレイヤーの配列のメモリをパッドの数分確保する
        m_players = new GameObject[playerNum];

        //接続されているパッドの数分ループを回す
        for (int i = 0; i < playerNum; i++)
        {
            //プレイヤーのタグを取得
            string playerTag = "Player" + (i + 1).ToString();
            //そのタグを持っているオブジェクト（プレイヤー）をm_playersに突っ込む
            m_players[i] = GameObject.FindWithTag(playerTag);
        }
        

        //勝者がいた場合の処理
        if (!m_isAllLoser)
        {
            //アニメーションプレイヤーを回転させるためのQuaternionを作成
            for (int i = 0; i < m_players.Length; i++)
            {
                //すべてのプレイヤーと勝ったプレイヤーのタグを照らし合わせる
                if (m_players[i].tag == m_bestPlayerTag)
                {
                    //同じだったら勝ちアニメーションプレイヤーの配列に入っているオブジェクトの位置を
                    //事前に設定されてある勝者の位置に移動させる
                    Instantiate(m_winAnimPlayers[i],m_winnerPosObj.transform.position,Quaternion.Euler(0.0f,90.0f,0.0f));
                    // WinUIを表示する
                    winUI.gameObject.SetActive(true);

                }
                else
                {
                    //それが負けたプレイヤーのタグだった場合
                    //負けアニメーションのみを行うプレイヤーオブジェクトを生成する
                    Instantiate(m_loseAnimPlayers[i], m_loserPosObj[i].transform.position, Quaternion.Euler(0.0f, 90.0f, 0.0f)); 
                }
            }
        }
        //すべてのプレイヤーが負けていた場合
        else
        {
            // LoseUIを表示する
            loseUI.gameObject.SetActive(true);

            for (int i = 0; i < m_players.Length; i++)
            {
                //負けアニメーションのみを行うプレイヤーオブジェクトを生成する
                Instantiate(m_loseAnimPlayers[i], m_loserPosObj[i].transform.position, Quaternion.Euler(0.0f, 90.0f, 0.0f));
            }
        }
    }
    private void Start()
    {
        SoundManager.Instance.PlayBGM(SoundManager.Instance.ResultBGM);
        m_vcam.m_Lens.FieldOfView = 0.1f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
#if UNITY_EDITOR 
        DebugSakamoto();
#endif 
       
        //カメラを動かす処理//FollowOffsetを少しずつ変化させて、カメラを近づける
        m_vcam.m_Lens.FieldOfView = Mathf.Lerp(m_vcam.m_Lens.FieldOfView, 60.0f, 0.01f);

        
    }

    void Update()
    {
        //Aボタンが押されたら一瞬だけ別のシーンに遷移する(急遽シーンをリセットするため)
        //そのあとタイトルシーンに戻る
        if (m_gameFlowManager.GetNowScene() == GameFlowManager.Scene.Result)
        {
            //XboxPadのAボタンが押されたら
            if (Gamepad.current.buttonSouth.wasPressedThisFrame)
            {
                //リザルトシーンに遷移する
                SceneManager.LoadScene("ResultScene");
            }
        }
    }

    public bool IsBackTitle()
    {
        return m_isBackTitle;
    }

    private void DebugSakamoto()
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

    /// <summary>
    /// Aボタンが押されたときにInputManagerが勝手に呼んでくれる関数
    /// </summary>
    /// <param name="ctx">InputActionクラスのCallbackContextという構造体</param>
    public void OnNext(InputAction.CallbackContext ctx)
    {
        Debug.Log("isBackTitleがtrueになった！");
        if (ctx.performed)
        {
            m_isBackTitle = true;
        }
    }
}
