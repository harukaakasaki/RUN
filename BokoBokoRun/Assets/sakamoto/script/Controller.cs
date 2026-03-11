using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.PlayerSettings;

public class Controller : MonoBehaviour
{
    /// <summary>
    /// 外部からは読み取り専用で中身はこのスクリプトで書き換え可能な書き方(get;private set;)
    /// </summary>
    public Vector3 MoveInput { get; private set;  }

    //項目名を設定
    [Header("Input Actions (.inputactions asset)")]
    //[SerializeField] private InputActionAsset m_actionsAsset;

    //移動
    private InputAction m_moveAction;

    //PlayerInput
    private PlayerInput m_playerInput;

    private void Awake()
    {
        //コンポーネントを取得
        m_playerInput = GetComponent<PlayerInput>();

        //各プレイヤーに紐づくactionsからアクションを取得
        m_moveAction = m_playerInput.actions["Move"];

        m_moveAction.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        //移動(playerInputにペアリングされたデバイス入力のみ拾う)
        var moveValue = m_moveAction.ReadValue<Vector2>();
        //移動ベクトルの大きさを1に制限
        moveValue = Vector2.ClampMagnitude(moveValue, 1.0f);
       
        MoveInput = new Vector3(moveValue.x,0,moveValue.y);
    }
}