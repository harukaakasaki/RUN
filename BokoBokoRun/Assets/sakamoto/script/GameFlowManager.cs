using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public sealed class GameFlowManager : MonoBehaviour
{
    private static GameFlowManager m_instance;

    [SerializeField] private TitleManager m_titleManager;        //タイトルマネージャー
    [SerializeField] private SelectManager m_selectManger;      //セレクトマネージャー
    [SerializeField] private InGameManager m_inGameManger;      //インゲームマネージャー
    [SerializeField] private ResultManager m_resultManger;      //リザルトマネージャー
    [SerializeField] private FadeManager m_fadeManager;         //フェードマネージャー
    [SerializeField] private UIManager m_uiManager;         //UIマネージャー

    // 参加したプレイヤーのリスト//保存用
    private List<PlayerInput> m_joinedPlayers = new List<PlayerInput>();


    //シーンを切り替えるときにplayersを移動させる先の位置
    [SerializeField] private Transform[] m_spawnPositions;

    //カメラを実際に切り替えるクラスの参照
    [SerializeField] private cameraManager m_cameraManager;

    private bool m_isTransitioning;//シーンを切り替え中か

    //パッドの数
    private int m_padNum;

    public enum Scene
    {
        None,   //何もない
        Title,  //タイトルシーン
        Select, //セレクトシーン
        InGame, //ゲーム中
        Result, //リザルトシーン
    }
    private static Scene m_scene;

    // Start is called before the first frame update
    void Start()
    {
        //念のためマネージャーをすべて非アクティブ化する
        m_titleManager.enabled = false;
        m_inGameManger.enabled = false;
        m_resultManger.enabled = false;

        //タイトルを出す前に黒→透明とする
        StartCoroutine(BootTitle());
    }

    private IEnumerator BootTitle()
    {
        //シーンをタイトルに切り替える
        ChangeScene(Scene.Title);
        //カメラもタイトルに切り替える
        m_cameraManager.SetTitle();
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
    void FixedUpdate()
    {
        //Test:パッドの数を取得
        m_padNum = Gamepad.all.Count;
        Debug.Log("現在のコントローラーの数:" + GetPadNum());

        switch (m_scene)
        {
            case Scene.Title://タイトル

                Debug.Log("タイトルシーンです");

                //タイトルマネージャーからゲームスタートされてるかを取得して
                //スタートされている&フェード中じゃなければセレクトシーンに遷移する
                if (!m_isTransitioning &&
                    m_titleManager.GetIsStart() &&
                    !m_fadeManager.m_isFading)
                {
                    m_isTransitioning = true;

                    StartCoroutine(ChangeSelect());
                }
                break;
            case Scene.Select:
                Debug.Log("セレクトシーンです");

                //セレクトマネージャーから決定ボタンが押されたかどうかを取得して
                //押されている&フェード中じゃなければゲームシーンに遷移する
                if(!m_isTransitioning &&
                    m_selectManger.IsDecided() &&
                    !m_fadeManager.m_isFading)
                {
                    m_isTransitioning = true;

                    StartCoroutine(ChangeInGame());
                }
                break;

            case Scene.InGame:
                Debug.Log("インゲームシーンです");
                //Result遷移も同様の手順で行う
                if (!m_isTransitioning &&
                    m_inGameManger.IsEnd() &&
                    !m_fadeManager.m_isFading)
                {
                    m_isTransitioning = true;

                    StartCoroutine(ChangeResult());
                }
                break;

            case Scene.Result:
                Debug.Log("リザルトシーンです");
                if (!m_isTransitioning &&
                    m_resultManger.IsBackTitle() &&
                    !m_fadeManager.m_isFading)
                {
                    m_isTransitioning = true;

                    StartCoroutine(ChangeTitle());
                }
                break;
        }
    }

    private IEnumerator ChangeInGame()
    {
        //まずフェードアウトを行う
        yield return m_fadeManager.FadeOut(0.0f, 1.0f);
        //PlayerInputの情報を渡す
        m_joinedPlayers = m_selectManger.GetJoinedPlayers();
        //暗転中にマネージャーを切り替え
        ChangeScene(Scene.InGame);
        //カメラも切り替え
        m_cameraManager.SetGame();
        //プレイヤーを移動させる
        m_inGameManger.SetPlayerInput(m_joinedPlayers);
        m_inGameManger.SetInGamePlayers();

        //カメラを完全に切り替えるために1フレーム待つ
        yield return null;

        //フェードインを行う
        yield return m_fadeManager.FadeIn(1.0f, 0.0f);

        //シーン遷移中フラグを降ろす
        m_isTransitioning = false;
    }

    private IEnumerator ChangeSelect()
    {
        //まずフェードアウトを行う
        yield return m_fadeManager.FadeOut(0.0f, 1.0f);
        //暗転中にセレクトマネージャーに切り替える
        ChangeScene(Scene.Select);
        //カメラも切り替える
        m_cameraManager.SetSelect();
      

        //カメラを完全に切り替えるために1フレーム待つ
        yield return null;

        //フェードインを行う
        yield return m_fadeManager.FadeIn(1.0f, 0.0f);

        //シーン遷移中フラグを降ろす
        m_isTransitioning = false;
    }

    private IEnumerator ChangeResult()
    {
        //まずフェードアウトを行う
        yield return m_fadeManager.FadeOut(0.0f, 1.0f);
        //暗転中にリザルトマネージャーに切り替える
        ChangeScene(Scene.Result);
        //カメラも切り替える
        m_cameraManager.SetResult();

        //カメラを完全に切り替えるために1フレーム待つ
        yield return null;

        //フェードインを行う
        yield return m_fadeManager.FadeIn(1.0f, 0.0f);

        //シーン遷移中フラグを降ろす
        m_isTransitioning = false;
    }

    private IEnumerator ChangeTitle()
    {
        //まずフェードアウトを行う
        yield return m_fadeManager.FadeOut(0.0f, 1.0f);
        //暗転中にリザルトマネージャーに切り替える
        ChangeScene(Scene.Title);
        //カメラも切り替える
        m_cameraManager.SetTitle();

        //カメラを完全に切り替えるために1フレーム待つ
        yield return null;

        //フェードインを行う
        yield return m_fadeManager.FadeIn(1.0f, 0.0f);

        //シーン遷移中フラグを降ろす
        m_isTransitioning = false;
    }

    public void ChangeScene(Scene scene)
    {
        //シーンを切り替える
        m_scene = scene;
        switch (scene)
        {
            case Scene.Title://タイトル
                //一旦すべてのマネージャーを非アクティブにする
                AllEnabled();
                //その後タイトルマネージャーのみをアクティブ化する
                m_titleManager.enabled = true;
                break;
            case Scene.Select://セレクト
                AllEnabled();
                m_selectManger.enabled = true;
                break;
            case Scene.InGame://インゲーム
                AllEnabled();
                m_inGameManger.enabled = true;
                break;
            case Scene.Result://リザルト
                AllEnabled();
                m_resultManger.enabled = true;
                m_resultManger.gameObject.SetActive(true);
                break;
        }
    }

    /// <summary>
    /// 全てのマネージャーを非アクティブ化する
    /// </summary>
    private void AllEnabled()
    {
        m_resultManger.gameObject.SetActive(false);

        m_titleManager.enabled = false;
        m_selectManger.enabled = false;
        m_inGameManger.enabled = false;
        m_resultManger.enabled = false;
    }
    /// <summary>
    /// 現在接続されているコントローラーの数を返す関数
    /// </summary>
    /// <returns>コントローラーの数</returns>
    public int GetPadNum()
    {
        return m_padNum;
    }

    public Scene GetNowScene()
    {
        return m_scene;
    }
}
