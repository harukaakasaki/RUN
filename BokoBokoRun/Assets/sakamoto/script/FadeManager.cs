using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.UI;

public class FadeManager : MonoBehaviour
{
    static class Constants
    {
        public const float fade_speed = 0.02f;//フェードする速度
    }

    float m_graphRed, m_graphGreen, m_graphBlue, m_graphAlpha; //画像のRGBA
    float m_textRed, m_textGreen, m_textBlue, m_textAlpha; //テキストのRGBA

    public enum FadeType               //フェードタイプ
    {
        In,     //フェードイン
        Normal, //通常
        Out,    //フェードアウト
    };
    public FadeType m_type;            //フェードタイプ宣言

    [SerializeField] Image m_fadeImage;//フェードに使う黒い画像
    [SerializeField] Text[] m_texts;   //フェードさせるテキストの配列

    // Start is called before the first frame update
    void Start()
    {
        //画像
        //黒い画像の存在をtrueにする
        m_fadeImage.gameObject.SetActive(true);
        //画像のRedの値を取得
        m_graphRed = m_fadeImage.color.r;
        //画像のGreenの値を取得
        m_graphGreen = m_fadeImage.color.g;
        //画像のBlueの値を取得
        m_graphBlue = m_fadeImage.color.b;
        //画像のAlphaの値を取得
        m_graphAlpha = m_fadeImage.color.a;

        //テキスト
        //テキストの存在をすべてtrueにする
        foreach(var text in m_texts)
        {
            text.enabled = true;
            //テキストのRedの値を取得
            m_textRed = text.color.r;
            //テキストのGreenの値を取得
            m_textGreen = text.color.g;
            //テキストのBlueの値を取得
            m_textBlue = text.color.b;
            //テキストのAlphaの値を取得
            m_textAlpha = text.color.a;
        }

        //フェードタイプを初期化
        m_type = FadeType.In;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //フェードタイプに合わせて処理を変更する
        switch (m_type)
        {
            case FadeType.In://フェードイン中
                FadeIn();
                break;
            case FadeType.Normal://通常中
                break;
            case FadeType.Out://フェードアウト中
                FadeOut();
                break;
        }
    }

    private void FadeIn()
    {
        //不透明度を減らしていく
        m_graphAlpha -= Constants.fade_speed;
        m_textAlpha -= Constants.fade_speed;
        //不透明度を適用する
        Alpha();
        //不透明度が0以下になったら
        if (m_graphAlpha <= 0)
        {
            //通常状態にする
            m_type = FadeType.Normal;
        }
    }

    private void FadeOut()
    {
        //存在をtrueにする
        m_fadeImage.enabled = true;
        foreach(var text in m_texts)
        {
            text.enabled = true;//存在をtrueにする
            m_textAlpha += Constants.fade_speed;//テキストの不透明度を上げる
        }
        //画像の不透明度を上げる
        m_graphAlpha += Constants.fade_speed;
        //不透明度を適用
        Alpha();
    }
    void Alpha()
    {
        //画像のアルファを適用
        m_fadeImage.color = new Color(m_graphRed, m_graphGreen, m_graphBlue, m_graphAlpha);
        //テキストのアルファを適用
        foreach (var text in m_texts)
        {
            text.color = new Color(m_textRed, m_textGreen, m_textBlue, m_textAlpha);
        }
    }

    public void OnFadeIn()
    {
        m_type = FadeType.In;
    }

    public void OnFadeOut()
    {
        m_type = FadeType.Out;
    }
}
