using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.PlayerSettings;

public class Controller : MonoBehaviour
{
    //定数クラス
    static class Constants
    {
        public const float kSpeed = 10.0f;//移動速度
        public const float kJumpPower = 300.0f;//ジャンプ力
    }

    //項目名を設定
    [Header("Input Actions (.inputactions asset)")]
    //[SerializeField] private InputActionAsset m_actionsAsset;

    //移動速度
    private float m_speed = Constants.kSpeed;
    //移動
    private InputAction m_moveAction;
    //Rigidbody
    private Rigidbody m_rigidbody;
    //フェードマネージャー
    private FadeManager m_fadeManager;
    //PlayerInput
    private PlayerInput m_playerInput;

    private void Awake()
    {
        //コンポーネントを取得
        m_playerInput = GetComponent<PlayerInput>();
        m_rigidbody = GetComponent<Rigidbody>();

        //各プレイヤーに紐づくactionsからアクションを取得
        m_moveAction = m_playerInput.actions["Move"];

        m_moveAction.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        ////移動(playerInputにペアリングされたデバイス入力のみ拾う)
        //var moveValue = m_moveAction.ReadValue<Vector2>();
        ////移動ベクトルの大きさを1に制限
        //moveValue = Vector2.ClampMagnitude(moveValue, 1.0f);
        ////移動ベクトルの計算
        //var move = new Vector3(moveValue.x, 0.0f, moveValue.y) * m_speed * Time.deltaTime;

        //移動
        //transform.Translate(move, Space.World);

        if (m_moveAction.WasPressedThisFrame())
        {
            Debug.Log("Move");
        }
    }
}