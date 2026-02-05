using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CharacterController))]
public abstract class C : MonoBehaviour
{

    // 継承先でも読み書きするからprotected
    // 見出し
    // private 変数をインスペクターに表示して編集できるようにするためのタグ

    //キャラクター関連
    [Header("ステータス")]
    [SerializeField] protected Vector3 m_CharacterPos;   // 位置
    [SerializeField] protected float m_CharacterSpeed = 1.0f;
    [SerializeField] protected bool m_IsGrounded = true;
    [SerializeField] protected bool m_IsDead = false;

    //アニメーション関連
    [Header("アニメーション")]
    [SerializeField] protected Animator m_Animator;
    [SerializeField] protected bool m_UseRootMotion = false;



    // 水平移動の現在ベクトル
    protected Vector3 m_Velocity;
    // 重力など
    [SerializeField] protected float m_Gravity = -20f;

    protected CharacterController m_Controller;

    // 公開プロパティ読み取り専用にしたいとき
    public bool IsDead => m_IsDead;
    public bool IsGrounded => m_IsGrounded;
    public float Speed => m_CharacterSpeed;

    //一回だけ呼ばれる
    protected virtual void Awake()
    {
        m_Controller = GetComponent<CharacterController>();
        m_CharacterPos = transform.position;
    }

    protected virtual void Update()
    {
        if (m_IsDead) return;

        // 継承先がここで入力やAIからm_Velocityを組み立てる
        Tick();

        ApplyGravity();
        MoveCharacter();

        // 表示用キャッシュ更新
        m_CharacterPos = transform.position;
    }

    /// <summary>
    /// 継承先で毎フレームの水平速度決定
    /// プレイヤーは入力ベクトル、敵は追跡ベクトル
    /// </summary>
    protected abstract void Tick();

    protected virtual void ApplyGravity()
    {
        // CharacterController の接地判定
        if (m_Controller.isGrounded)
        {
            m_IsGrounded = true;
            // 接地押し付け
            if (m_Velocity.y < 0f) m_Velocity.y = -2f; 
        }
        else
        {
            m_IsGrounded = false;
            m_Velocity.y += m_Gravity * Time.deltaTime;
        }
    }

    protected virtual void MoveCharacter()
    {
        m_Controller.Move(m_Velocity * Time.deltaTime);
    }

    // 共通の死亡時処理。コライダ無効化等
    protected virtual void OnDead()
    {
        enabled = false;
    }
}

