using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalLineChecker : MonoBehaviour
{
    [SerializeField] int Rank = 0;//順位(どこかに渡す)
    bool isFinish = false;//このbool文がtrueになった時、カメラがScene遷移する

    //問題点
    //Playerが何人参加しているのかということを知りたい
    //どこに渡したらいいのかわからない
    //




    // Start is called before the first frame update
    void Start()
    {
        //最初に受け取る

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Rank++;
        }
    }
}
