using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public sealed class GameFlowManager : MonoBehaviour
{
    private static GameFlowManager m_instance;

    [SerializeField] private TitleManager m_titleManger;   //タイトルマネージャー
    [SerializeField] private InGameManager m_inGameManger;//インゲームマネージャー
    [SerializeField] private ResultManager m_resultManger;//リザルトマネージャー
    [SerializeField] private FadeManager m_fadeManager;//フェードマネージャー

    //カメラを実際に切り替えるクラスの参照
    [SerializeField] private cameraManager m_cameraManager;

    private bool m_isTransitioning;//シーンを切り替え中か

    //パッドの数
    private int m_padNum;

    public enum Scene
    {
        None,   //何もない
        Title,  //タイトルシーン
        InGame, //ゲーム中
        Result, //リザルトシーン
    }
    private static Scene m_scene;

    // Start is called before the first frame update
    void Start()
    {
        //念のためマネージャーをすべて非アクティブ化する
        m_titleManger.enabled = false;
        m_inGameManger.enabled = false;
        m_resultManger.enabled = false;

        //タイトルを出す前に黒→透明とする
        StartCoroutine(BootTitle());
    }

    private IEnumerator BootTitle()
    {
        //シーンをタイトルに切り替える
        ChangeScene(Scene.Title);
        //フェードを行う
        yield return m_fadeManager.FadeIn();
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
        m_padNum = Gamepad.all.Count;
        Debug.Log("現在のコントローラーの数:" +  m_padNum);

        switch (m_scene)
        {
            case Scene.Title://タイトル

                Debug.Log("タイトルシーンです");

                //タイトルマネージャーからゲームスタートされてるかを取得して
                //スタートされている&フェード中じゃなければゲームシーンに遷移する
                if (!m_isTransitioning &&
                    m_titleManger.GetIsStart() &&
                    !m_fadeManager.m_isFading)
                {
                    m_isTransitioning = true;

                    StartCoroutine(ChangeInGame());
                }
                break;
            case Scene.InGame:
                Debug.Log("インゲームシーンです");
                //TODO:Result遷移も同様の手順で行う
                break;
            case Scene.Result:
                Debug.Log("リザルトシーンです");
                break;
        }
    }

    private IEnumerator ChangeInGame()
    {
        //まずフェードアウトを行う
        yield return m_fadeManager.FadeOut(0.0f, 1.0f);

        //暗転中にマネージャーを切り替え
        ChangeScene(Scene.InGame);
        //カメラも切り替え
        m_cameraManager.SetGame();

        //カメラを完全に切り替えるために1フレーム待つ
        yield return null;

        //フェードインを行う
        yield return m_fadeManager.FadeIn(1.0f, 0.0f);

        m_isTransitioning = false;
    }

    public void ChangeScene(Scene scene)
    {
        m_scene = scene;
        switch (scene)
        {
            case Scene.Title:
                AllEnabled();
                m_titleManger.enabled = true;
                break;
            case Scene.InGame:
                AllEnabled();
                m_inGameManger.enabled = true;
                break;
            case Scene.Result:
                AllEnabled();
                m_resultManger.enabled = true;
                break;
        }
    }

    private void AllEnabled()
    {
        m_titleManger.enabled = false;
        m_inGameManger.enabled = false;
        m_resultManger.enabled = false;
    }
    /// <summary>
    /// 現在接続されているコントローラーの数を返す関数
    /// </summary>
    /// <returns></returns>
    public int GetPadNum() 
    {
        return m_padNum;
    }
}
