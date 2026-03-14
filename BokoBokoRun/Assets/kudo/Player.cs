using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;


public enum PlayerState
{
    Idle,
    Move,
}

public class Player : Character
{

   Vector3 resetPosition = new Vector3(35.0f,-33,0);// 戻す位置
    bool m_isNoActive;//生きているか死んでいるか
    static class Constants
    {
        public const float kMoveSpeed = 6.0f;
    }

    private Controller m_controller;
    PlayerState m_state;
    Animator m_animator;

    Vector3 InitPos = new Vector3(0,0,0);//初期座標

    private GameFlowManager m_flowManager;
    private InGameManager m_inGameManager;
    
    private Vector3 m_playerPos;
    [SerializeField] private Vector3 m_spawnPos;
    private Vector3 m_prevPos;
    private Vector3 m_targetPos;

    // ノックバック用オフセット
    private Vector3 m_knockbackOffset = Vector3.zero;
    private Coroutine m_knockbackCoroutine;

    //エフェクト呼び出す
    private onEffectManager m_efManager;
    private Vector3 m_effectPos;//エフェクトを出す位置
    private static readonly int IsMovingHash = Animator.StringToHash("IsMoving");
    // Start is called before the first frame update
    void Start()
    {
        m_state = PlayerState.Idle;
        //初期座標の更新
        //transform.position = InitPos;

        //GameFlowManagerコンポーネントを取得
        m_flowManager = FindObjectOfType<GameFlowManager>();
        if (m_flowManager == null)
        {
            Debug.LogError("flowManager が見つかりません.");
        }

        //InGameManagerコンポーネントを取得
        m_inGameManager = FindObjectOfType<InGameManager>();
        if (m_inGameManager == null)
        {
            Debug.LogError("InGameManager が見つかりません.");
        }

        // Animatorコンポーネントを取得
        m_animator = GetComponent<Animator>();
        if (m_animator == null)
        {
            Debug.LogError("Animator が見つかりません.");
        }

        //InGameManagerコンポーネントを取得
        m_efManager = FindObjectOfType<onEffectManager>();
        if (m_efManager == null)
        {
            Debug.LogError("onEffectManager が見つかりません.");
        }


        //初期位置を保存
        //m_playerPos = m_spawnPos;
        //transform.position = m_playerPos;

        // 同じオブジェクトにアタッチされているControllerを取得
        m_controller = GetComponent<Controller>();
        m_isNoActive = false;//生きているか//最初は生きている
    }



    protected override void Update()
    {
        
    }

    private void FixedUpdate()
    {
        //if (Keyboard.current != null && Keyboard.current.jKey.wasPressedThisFrame)
        //{
        //    string name = "Impact";
        //    //エフェクトを出す
        //    m_efManager.PlayEffect(this.transform.position, name);
        //    Debug.Log("エフェクトを押したよ");
        //}

        m_effectPos = transform.position;
        //インゲームの時のみ、さらにInGame中のm_isCanMoveがtrueのみ動けるようにする
        if (m_flowManager.GetNowScene() == GameFlowManager.Scene.InGame &&
            m_inGameManager.IsCanMove())
        {
            if (!m_isNoActive)//生きているかfalseが生きている
            {
                Vector3 move = new Vector3();

                //  入力ターゲット位置を取得
                if (m_controller != null)//現在はmoveInputをそのまま私
                {
                    m_targetPos = m_controller.MoveInput.normalized;//padの入力を更新

                    //移動量//そのフレームの
                    move = m_controller.MoveInput;
                }

                if (move.sqrMagnitude > 0.001f)
                {
                    Quaternion targetRot = Quaternion.LookRotation(move);

                    transform.rotation = Quaternion.Slerp(transform.rotation,
                        targetRot, 0.5f);
                }

                //0.01倍していたため移動が極端に遅くなっていた。移動速度はMoveSpeedで制御する
                m_targetPos = move.normalized * Constants.kMoveSpeed * Time.deltaTime;

                //自身の位置を元の座標+ターゲット + ノックバックオフセットを適用
                transform.position = transform.position + m_targetPos + m_knockbackOffset;

                if (move.sqrMagnitude > 0.01f)//そのフレームで移動していたらStateを変える
                {
                    m_state = PlayerState.Move;
                    Debug.Log("m_stateはmoveです");
                }
                else
                {
                    m_state = PlayerState.Idle;
                    Debug.Log("m_stateはIdleです");
                }
            }

            
        }

        if (m_animator)
        {
            m_animator.SetBool(IsMovingHash, m_state == PlayerState.Move);//動いているかをboolでanimatorのセット
        }

        //// 回転移動しているときだけ
        //if (delta.sqrMagnitude > 0.0001f)
        //{
        //    Vector3 dir = delta.normalized;
        //    transform.forward = Vector3.Slerp(transform.forward, dir, Time.deltaTime * m_rotateSpeed);
        //}

        // 前フレーム位置を更新
        // m_prevPos = now;//使わない
        //使ってない
        Tick();
    }

    // トリガーで敵と接触したときの処理はクラスレベルで定義する

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GameEnemy"))
        {
            Debug.Log("敵に当たりました！");

            // 衝突相手からプレイヤーが離れる向きにベクトルを作る
            Vector3 dir = (transform.position - other.transform.position).normalized;
            // 斜めに飛ばしたいのでY成分を追加
            dir.y = Mathf.Max(dir.y, 0.5f); //必要に応じて上昇量を調整
            dir = dir.normalized;

            // 力の強さ
            float strength = 2.0f;

            Vector3 knockback = dir * strength;

            // 既存のノックバックがあれば止める
            if (m_knockbackCoroutine != null)
            {
                StopCoroutine(m_knockbackCoroutine);
            }
            // 指定時間で減衰させる
            m_knockbackCoroutine = StartCoroutine(KnockbackRoutine(knockback, 0.5f));
            //インゲームマネージャーにぶっ飛ばされたということを渡す

            //敵と当たったらエフェクトを出す
            string name = "Impact";
            //エフェクトを出す
            m_efManager.PlayEffect(transform.position, name);
            Debug.Log("エフェクトを出しました");
            //敵と当たったら音を出す
            SoundManager.Instance.PlaySE(SoundManager.Instance.HitSE);
            Debug.Log("音を出しました");

        }
        // 天井タグに当たったらプレイヤーの位置をリセット
        if (other.CompareTag("Ceiling"))
            {
            //敵と当たったら死亡判定を入れる
            m_isNoActive = true;
                Debug.Log("Playerの位置を変更");
                // プレイヤーをリセット位置に戻す
                transform.position = resetPosition;
            }
       


    }

    // ノックバックの減衰処理
    private IEnumerator KnockbackRoutine(Vector3 force, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            // 線形減衰
            float t = 1f - (elapsed / duration);
            m_knockbackOffset = force * t;
            elapsed += Time.deltaTime;
            yield return null;
        }
        m_knockbackOffset = Vector3.zero;
        m_knockbackCoroutine = null;
    }

    protected override void Tick()
    {
        switch (m_state)
        {
            case PlayerState.Idle:
                break;
            case PlayerState.Move:
                break;
        }
    }

    
    public bool GetNoActive()
    {
        return m_isNoActive;
    }
    


}

