using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;
using static UnityEditor.Experimental.GraphView.GraphView;

public class SelectManager : MonoBehaviour
{
    private bool m_isDecided = false;//決定ボタンを押したかどうか
    [SerializeField] private GameFlowManager m_gameFlowManager;

    //プレイヤーのスポーン位置
    [SerializeField] Transform[] m_spawnPositions;

    // 参加したプレイヤーのリスト
    private List<PlayerInput> m_joinedPlayers = new List<PlayerInput>();


    enum PlayerNum
    {
        Player1,
        Player2,
        Player3,
        Player4,
        Num
    }



    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        SoundManager.Instance.PlayBGM(SoundManager.Instance.SelectBGM);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
#if UNITY_EDITOR
        DebugProcessing();
#endif

        //XboxPadのaボタンが押されたら
        if (Gamepad.current.buttonSouth.wasPressedThisFrame)
        {
            //ゲームシーンに行くフラグをtrueにする
            OnGoInGame();
        }
    }

    public bool IsDecided()
    {
        return m_isDecided;
    }

    private void DebugProcessing()
    {
        if (Keyboard.current.zKey.wasPressedThisFrame)
        {
            m_isDecided = true;
        }
    }

    void OnPlayerJoined(PlayerInput input)
    {
        Debug.Log("OnPlayerJoined called: " + input.playerIndex);

        int index = input.playerIndex;//プレイヤーの通し番号を取得
        //スポーン位置へ移動
        input.transform.position = m_spawnPositions[index].position;
        input.transform.rotation = m_spawnPositions[index].rotation;

        Debug.Log("プレイヤーの位置をセレクトシーンへ移動");
        //保存する
        m_joinedPlayers.Add(input);
        Debug.Log("★★★ プレイヤー追加後のCount: " + m_joinedPlayers.Count + " ★★★");
    }

    public List<PlayerInput> GetJoinedPlayers()
    {

        Debug.Log("★★★ GetJoinedPlayersのCount: " + m_joinedPlayers.Count + " ★★★");
        return m_joinedPlayers;

    }


    private void OnGoInGame()
    {
        m_isDecided = true;
    }
}
