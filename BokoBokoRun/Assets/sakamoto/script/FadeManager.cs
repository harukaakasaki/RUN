using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.UI;

public class FadeManager : MonoBehaviour
{
    static class Constants
    {
        public const float fade_speed = 1.0f;//α1.0に達するまでの秒速
    }

    [SerializeField] private Image m_fadeImage;//最前面の黒い画像

    float m_graphRed, m_graphGreen, m_graphBlue, m_graphAlpha; //画像のRGBA
    public bool m_isFading { get; private set; }//セッターとゲッターをメンバ変数と同時に宣言

    /// <summary>
    /// Start()より先に走る関数
    /// </summary>
    private void Awake()
    {
        //オブジェクトの黒い画像をアクティブ化する
        m_fadeImage.gameObject.SetActive(true);
        //黒い画像のRGBA情報を取得
        var imageColor = m_fadeImage.color;
        m_graphRed = imageColor.r;//Red
        m_graphGreen = imageColor.g;//Green
        m_graphBlue = imageColor.b;//青
        m_graphAlpha = imageColor.a;//透明度
    }

    /// <summary>
    /// フェードを適用する
    /// </summary>
    private void ApplyAlpha()
    {
        //画像にRGBAを適用
        m_fadeImage.color = new Color(m_graphRed, m_graphGreen, m_graphBlue, m_graphAlpha);
    }

    public IEnumerator FadeOut(float startAopha = 0f, float endAlpha = 1.0f)
    {
        //フェード中のフラグを立てる
        m_isFading = true;
        //画像をアクティブ化する
        m_fadeImage.enabled = true;

        //αを初期化する
        m_graphAlpha = startAopha;
        ApplyAlpha();//αを適用

        while (m_graphAlpha < endAlpha)
        {
            //非同期でフェードを行う
            float d = Constants.fade_speed * Time.unscaledDeltaTime;
            m_graphAlpha = Mathf.Min(endAlpha, m_graphAlpha + d);
            ApplyAlpha();
            yield return null;
        }
        m_isFading = false;//フラグをおろす
    }

    public IEnumerator FadeIn(float startAlpha = 1.0f, float endAlpha = 0.0f)
    {
        //フェードフラグを立てる
        m_isFading = true;
        //黒い画像を非アクティブにする
        m_fadeImage.enabled = true;

        //αを初期化
        m_graphAlpha = startAlpha;
        ApplyAlpha();

        while (m_graphAlpha > endAlpha)
        {
            //非同期でフェードを行う
            float d = Constants.fade_speed * Time.unscaledDeltaTime;
            m_graphAlpha = Mathf.Max(endAlpha, m_graphAlpha - d);
            ApplyAlpha();
            yield return null;
        }

        //完全透明なら止めてUIの邪魔をしない
        m_fadeImage.enabled = false;
        m_isFading = false;
    }
}