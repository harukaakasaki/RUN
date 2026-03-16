using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
  Enter,
  Active
}


public class enemyMove : Character
{
    [Header("EnemyState")]
    //Enemyの状態
    public EnemyState m_State = EnemyState.Enter;
    int m_count = 0;
    bool isRotate = false;

    //どのSceneだと動かすのか
    private GameFlowManager m_flowManager;
    //インゲームに入って最初の4秒はカウントダウンなので
    //動けないようにするためのInGameManager
    private InGameManager m_inGameManager;

    //関数
    private void Move()//移動中
    {
        
      m_Velocity = new Vector3(0.09f, 0.0f, 0.0f);
    }
    private void Entering()//出場中
    
    {
        //transform.rotation = Quaternion.Euler(0, 90, 0)の時、zを＋

        bool enemyRot = transform.rotation == Quaternion.identity;

        if (enemyRot)
        {
            m_Velocity = new Vector3(0.0f, 0.0f, +0.02f);
        }
        else
        {
            m_Velocity = new Vector3(0.0f, 0.0f, -0.02f);
        }

        m_count++;
        if(m_count >= 140)//100フレーム経過したら
        {
        //Countを数えて、そのあとScene繊維
        m_State = EnemyState.Active;
            isRotate = true;
        }
          
        
    }



    // Start is called before the first frame update
    void Start()//初期化
    {
    
        m_count = 0;
        isRotate = false;

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
    }

    // Update is called once per frame
    protected override void Update()//毎フレーム更新
    {

    }
    public void SetStateEnemy()
    {
        m_State = EnemyState.Enter;
    }

    private void FixedUpdate()
    {
        //インゲームの時のみ、又はInGame中のm_isCanMoveがtrueのみ動けるようにする
        if (m_flowManager.GetNowScene() == GameFlowManager.Scene.InGame &&
            m_inGameManager.IsCanMove())
        {
            Tick();
            this.transform.position += m_Velocity;
        }
    }
    protected override void Tick()//enemyの動き
    {
        if(isRotate)
        {
            Quaternion targetRotation = Quaternion.Euler(0, 90, 0);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
        }


        switch(m_State)
        {
            case EnemyState.Enter:
                Entering();
                break;
            case EnemyState.Active:
                Move();
                break;
            default:
                break;
        }
    }

    


}
