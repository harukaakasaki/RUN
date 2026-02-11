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
    [SerializeField] private InputActionAsset m_actionsAsset;

    //移動速度
    private float m_speed = Constants.kSpeed;
    //移動
    private InputAction m_moveAction;
    //Rigidbody
    private Rigidbody m_rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        //InputActionのジャンプと移動を取得して有効化
        m_moveAction = m_actionsAsset.FindAction("Move");

        m_moveAction.Enable();

        //Rigidbodyの取得
        m_rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //移動
        var moveValue = m_moveAction.ReadValue<Vector2>();
        //移動ベクトルの大きさを1に制限
        moveValue = Vector2.ClampMagnitude(moveValue, 1.0f);
        //移動ベクトルの計算
        var move = new Vector3(moveValue.x, 0.0f, moveValue.y) * m_speed * Time.deltaTime;

        //移動
        transform.Translate(move, Space.World);

        if (m_moveAction.WasPressedThisFrame())
        {
            Debug.Log("Move");
        }

    }
}