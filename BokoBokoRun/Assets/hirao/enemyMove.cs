using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    Idle,
    Move,
    Attack,
}


public class enemyMove : C
{
    //Enemy‚Ìó‘Ô
    EnemyState m_State;
    //ŠÖ”
    private void Move()
    {
       // m_velovity = new Vector3{3f,0f,0f};
    }



    // Start is called before the first frame update
    void Start()
    {
        m_State = EnemyState.Move;
    }

    // Update is called once per frame
    protected override void Update()
    {
        Tick();
        ApplyGravity();

        this.transform.position = m_Velocity;

    }
    protected override void Tick()//enemy‚Ì“®‚«
    {
        switch(m_State)
        {
            case EnemyState.Idle:
                break;
            case EnemyState.Move:
                break;
            case EnemyState.Attack:
                break;
            default:
                break;
        }
    }

    


}
