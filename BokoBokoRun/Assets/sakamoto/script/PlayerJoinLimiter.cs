using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJoinLimiter : MonoBehaviour
{
    [SerializeField] private GameFlowManager m_gameFlowManager;
    [SerializeField] private PlayerInputManager m_playerInputManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        Debug.Log("CurrentSceneState=" + m_gameFlowManager.GetNowScene());

        //現在のシーンがセレクトシーンの時だけJoinを許可する
        if (m_gameFlowManager.GetNowScene() == GameFlowManager.Scene.Select)
        {
            Debug.Log("Join Enabled");
            m_playerInputManager.EnableJoining();
        }
        else
        {
            Debug.Log("Join Disabled");
            m_playerInputManager.DisableJoining();
        }
    }
}
