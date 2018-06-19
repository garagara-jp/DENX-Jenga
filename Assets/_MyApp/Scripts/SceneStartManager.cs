using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SceneStartManager : MonoBehaviour
{
    CalcRealDeltaTime calcTime;

    [SerializeField]
    Image panel_image;      // フェードに使うパネルイメージ
    [SerializeField]
    float fade_second = 1;  // フェードにかかる時間(秒)
    [SerializeField, Range(0,1)]
    float red = 0;
    [SerializeField, Range(0, 1)]
    float green = 0;
    [SerializeField, Range(0, 1)]
    float blue = 0;

    float fade_deltatime;
    float real_deltatime;
    float alfa;

    void Start()
    {
        panel_image.enabled = true;
        panel_image.color = new Color(red, green, blue, 1);
        alfa = 1;
        calcTime = GetComponent<CalcRealDeltaTime>();
        fade_deltatime = 0;
    }

    void Update()
    {
        if (alfa != 0)
            FadeStartScene();
    }

    // パネルのアルファ値を1→0にする
    void FadeStartScene()
    {
        real_deltatime = calcTime.realDeltaTime;
        fade_deltatime += real_deltatime;
        if (fade_deltatime >= fade_second)
        {
            fade_deltatime = fade_second;
        }
        alfa = 1 - (fade_deltatime / fade_second);
        panel_image.color = new Color(red, green, blue, alfa);
        
        if (alfa == 0)
            panel_image.enabled = false;
    }
}
