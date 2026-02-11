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

    float m_red, m_green, m_blue, m_alpha; //RGBA

    public enum FadeType               //フェードタイプ
    {
        In,     //フェードイン
        Normal, //通常
        Out,    //フェードアウト
    };
    public FadeType m_type;            //フェードタイプ宣言

    [SerializeField] Image m_fadeImage;                  //フェードに使う黒い画像

    // Start is called before the first frame update
    void Start()
    {
        //Imageコンポーネントを取得
        m_fadeImage = GetComponent<Image>();

        //画像のRedの値を取得
        m_red = m_fadeImage.color.r;
        //画像のGreenの値を取得
        m_green = m_fadeImage.color.g;
        //画像のBlueの値を取得
        m_blue = m_fadeImage.color.b;
        //画像のAlphaの値を取得
        m_alpha = m_fadeImage.color.a;

        //フェードタイプを初期化
        m_type = FadeType.Normal;
    }

    // Update is called once per frame
    void Update()
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
        m_alpha -= Constants.fade_speed;
        //不透明度を画像に適用する
        Alpha();
        //不透明度が0以下になったら
        if (m_alpha <= 0)
        {
            //通常状態にする
            m_type = FadeType.Normal;
        }
    }

    private void FadeOut()
    {
        m_fadeImage.enabled = true;
        m_alpha += Constants.fade_speed;
        Alpha();
        if (m_alpha >= 1)
        {
            m_type = FadeType.Normal;
        }
    }
    void Alpha()
    {
        m_fadeImage.color = new Color(m_red, m_green, m_blue, m_alpha);
    }
}
