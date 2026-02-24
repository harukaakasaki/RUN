using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.UI;

public class TitleManager : GameManagerBase
{
    
    private System.IDisposable m_onAnyButton;//何かのボタンが押されたときに使う変数
    private bool m_isStated;//ゲームを開始したかどうか

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnEnable()
    {
        m_fadeManager.OnFadeIn();

        //何かのボタンが押された瞬間に呼ばれる
        m_onAnyButton = InputSystem.onAnyButtonPress.CallOnce(control =>
        {
            //既に開始していたら処理を飛ばす
            if (m_isStated) return;
            //シーンをインゲームにチェンジする
            m_gameFlowManager.ChangeScene(GameFlowManager.Scene.InGame);
            m_isStated = true;
            m_fadeManager.OnFadeOut();
        });
    }

    private void OnDisable()
    {
        //OnEnableで登録したイベントを解除する
        m_onAnyButton?.Dispose();
        m_onAnyButton = null;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
