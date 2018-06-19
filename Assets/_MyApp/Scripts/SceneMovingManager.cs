using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// GameSystemにアタッチ
// Scene遷移をコントロールする

public class SceneMovingManager : MonoBehaviour
{
    CalcRealDeltaTime calcTime;

    [SerializeField]
    string[] target_scene_name;   // 遷移する先のシーン
    [SerializeField]
    int scene_number = 0;   // 遷移する先のシーンの番号
    [SerializeField]
    bool move_timing;   // シーン遷移するかどうか

    public int _sceneNumber
    {
        get { return scene_number; }
        set { scene_number = value; }
    }

    public bool moveTiming
    {
        get { return move_timing; }
        set { move_timing = value; }
    }

    [SerializeField]
    Image panel_image;  // フェードに使うパネルイメージ
    [SerializeField]
    float fade_second = 3f; // フェードにかかる時間(秒)  
    [SerializeField]
    bool is_fade = true;    // フェードするかどうか

    float alfa;
    float red = 0;
    float green = 0;
    float blue = 0;
    float real_deltatime;
    float fade_deltatime;
    float last_realtime;

    void Start()
    {
        calcTime = GetComponent<CalcRealDeltaTime>();
        fade_deltatime = 0;
        move_timing = false;
    }

    void Update()
    {
        if (move_timing == true && is_fade == false)
            MoveScene();

        if (move_timing == true && is_fade == true)
            FadeMoveScene();
    }

    void MoveScene()
    {
        if (move_timing)
            SceneManager.LoadScene(target_scene_name[scene_number]);
    }

    // パネルのアルファ値を0→1にする
    void FadeMoveScene()
    {
        if (!panel_image.enabled)
            panel_image.enabled = true;

        real_deltatime = calcTime.realDeltaTime;
        fade_deltatime += real_deltatime;
        if (fade_deltatime >= fade_second)
        {
            fade_deltatime = fade_second;
        }
        alfa = fade_deltatime / fade_second;
        panel_image.color = new Color(red, green, blue, alfa);

        if (alfa == 1)
            SceneManager.LoadScene(target_scene_name[scene_number]);
    }
}
