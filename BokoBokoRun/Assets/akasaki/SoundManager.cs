using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // 他のスクリプトからアクセスするための変数
    public static SoundManager Instance;

    public AudioSource BgmSource;// BGMを再生するためのAudioSource

    public AudioSource SeSource; // SEを再生するためのAudioSource

    // BGM系
    [Header("BGM")]// インスペクターで見やすくするためのヘッダー
    public AudioClip TitleBGM; // タイトル画面のBGM
    public AudioClip InGameBGM;// インゲーム画面のBGM
    public AudioClip ResultBGM;// リザルト画面のBGM

    // SE系
    [Header("SE")]// インスペクターで見やすくするためのヘッダー
    public AudioClip DecisionSE;// 決定SE
    public AudioClip HitSE;     // ヒット時のSE

    // SoundManagerを一つだけにする
    void Awake()
    {
        // SoundManagerが存在していない場合
        if (Instance == null)
        {
            // 自分をInstanceにする
            Instance = this;
            DontDestroyOnLoad(gameObject); // シーンが変わってもこのオブジェクトを破壊しない
        }
        else
        {
            Destroy(gameObject); // 既にSoundManagerが存在する場合は新しいものを破壊する
        }
    }

    // BGMを再生する関数
    public void PlayBGM(AudioClip clip)
    {
        // 既に同じBGMが再生されている場合は何もしない
        if (BgmSource.clip == clip)return; 

        // 再生するBGMを設定
        BgmSource.clip = clip;

        // BGMを再生する
        BgmSource.Play();
    }

    // BGMを停止する関数
    public void StopBGM()
    {
        BgmSource.Stop();
    }

    // SEを再生する関数
    public void PlaySE(AudioClip clip)
    {
        // SEを一度だけ再生する
        SeSource.PlayOneShot(clip); 
    }

}
