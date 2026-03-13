using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class CountDown : MonoBehaviour
{
    [SerializeField] private GameFlowManager m_gameFlowManager;

    //カウントダウンに使う画像
    [SerializeField] private Image[] m_images;
    private int m_frame = 0;//最初のカウントダウン用のフレーム

    static class Constants
    {
        public const int kCountDownFrame = 150;
    }
    
    //画像の順番に対応したenum
    private enum ImageNum
    {
        Three,  //3
        Two,    //2
        One,    //1
    }

    // Start is called before the first frame update
    void Start()
    {
        //カウントダウン用のフレームを代入
        m_frame = Constants.kCountDownFrame;

        //数字のサイズが小さい→大きいになるように
        //初期化で小さくする
        for (int i = 0; i < m_images.Length; i++)
        {
            //サイズをゼロにする
            m_images[i].transform.localScale = Vector3.zero;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //1秒かけて大きくなる
        //1秒経ったら消えて次の数字が大きくなる
        if (m_gameFlowManager.GetNowScene() == GameFlowManager.Scene.InGame)
        {
            //sizeUpの値ごとにサイズアップしていく
            Vector3 sizeUp = new Vector3(0.1f, 0.1f, 0.1f);

            m_frame--;
            //フレームが100以上の時はオブジェクト3を大きくする
            if (m_frame >= 100)
            {
                //3をサイズアップさせる
                m_images[(int)ImageNum.Three].transform.localScale += sizeUp;
            }
            else if (m_frame >= 50)
            {
                //3を非アクティブ化する
                m_images[(int)(ImageNum.Three)].gameObject.SetActive(false);

                //2をサイズアップさせる
                m_images[(int)ImageNum.Two].transform.localScale += sizeUp;
            }
            else if (m_frame >= 0)
            {
                //2を非アクティブ化する
                m_images[(int)(ImageNum.Two)].gameObject.SetActive(false);

                //1をサイズアップさせる
                m_images[(int)ImageNum.One].transform.localScale += sizeUp;
            }
            else
            {
                //1を非アクティブ化する
                m_images[(int)(ImageNum.One)].gameObject.SetActive(false);
            }
        }
    }
}