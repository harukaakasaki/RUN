using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public sealed class GameFlowManager : MonoBehaviour
{
    private static GameFlowManager m_instance;

    public enum Scene
    {
        Title,//タイトルシーン
        InGame,//ゲーム中
        Result,//リザルトシーン
    }
    private static Scene m_scene;

    // Start is called before the first frame update
    void Start()
    {
        //初期シーンをタイトルに設定
        m_scene = Scene.Title;
    }

    private void Awake()
    {
        if (m_instance == null)
        {
            m_instance = this;
        }
        else
        {
            Destroy(gameObject);//2つ目以降のインスタンスを破棄
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (m_scene)
        {
            case Scene.Title:
                Debug.Log("タイトルシーン");
                break;
            case Scene.InGame:
                Debug.Log("インゲーム中");
                break;
            case Scene.Result:
                Debug.Log("リザルトシーン");
                break;
        }
    }

    public void ChangeScene(Scene scene)
    {
        m_scene = scene;
    }

    public Scene GetScene()
    {
        return m_scene;
    }
}
