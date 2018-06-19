using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UniRx;

/// <summary>
/// ゲームの進行とUIを管理
/// GameManagerにアタッチ
/// </summary>
public class GameManager : MonoBehaviour
{
    [SerializeField]
    CreatJengaTower creatJengaTower;
    [SerializeField]
    SoundPlayer soundPlayer;
    [SerializeField]
    GameOverManager gameOverManager;
    [SerializeField]
    SceneMovingManager sceneMovingManager;
    [SerializeField]
    PowerCalculation powerCalculation;

    // 画面に表示するUI
    [SerializeField]
    Button putOneLayer;
    [SerializeField]
    Button autoPut;
    [SerializeField]
    Text layerCounter;
    [SerializeField]
    Text powerCounter;
    [SerializeField]
    Button tutorial;
    [SerializeField]
    Text gameOver;
    [SerializeField]
    Image gameOverPanel;
    [SerializeField]
    Button retry;
    [SerializeField]
    Button BackToTitle;
    [SerializeField]
    GameObject ranking;

    // UI操作に使うAudioSource
    [SerializeField]
    AudioSource generalDecisionSound;
    [SerializeField]
    AudioSource retrySound;
    [SerializeField]
    AudioSource backToTitleSound;

    [SerializeField]
    GameObject Tenban; // ジェンガの接触を判定するオブジェクト

    [SerializeField]
    int putting_number;     // AutoPut()で積み上げる段数

    [SerializeField]
    float timeSpeed = 2;

    void Start()
    {
        Time.timeScale = timeSpeed;

        // UIを初期化
        //putOneLayer.enabled = true;
        //autoPut.enabled = true;
        gameOver.enabled = false;
        gameOverPanel.enabled = false;
        retry.gameObject.SetActive(false);
        BackToTitle.gameObject.SetActive(false);
        ranking.SetActive(false);
        tutorial.interactable = true;

        // 一段積むボタンの挙動
        putOneLayer.onClick.AddListener(() =>
        {
            creatJengaTower.AlignJenga();
            soundPlayer.play_place_jenga();
        });

        // 自動で積むボタンの挙動
        autoPut.onClick.AddListener(() =>
        {
            AutoPut();
            //TestAutoPut();
        });

        // もう一回ボタンの挙動
        retry.onClick.AddListener(() =>
        {
            retrySound.Play();
            sceneMovingManager._sceneNumber = 0;
            sceneMovingManager.moveTiming = true;
        });

        // タイトルへ戻るボタンの挙動
        BackToTitle.onClick.AddListener(() =>
        {
            backToTitleSound.Play();
            sceneMovingManager._sceneNumber = 1;
            sceneMovingManager.moveTiming = true;
        });

        AutoPut();
    }

    void Update()
    {
        if (gameOverManager._isFinish)
        {
            FinishGame();
        }
        else
        {
            CountLayer();
            CountPower();
        }
    }

    /// <summary>
    /// ジェンガ積みを自動で行う
    /// </summary>
    void AutoPut()
    {
        // ストリームを流す回数を算出
        var auto_put_count = putting_number - creatJengaTower._layerCount;

        // つむつむ
        Observable.Interval(TimeSpan.FromSeconds(0.2f))
            .Where(x => x < auto_put_count)
            .Subscribe(_ =>
            {
                creatJengaTower.AlignJenga();
                soundPlayer.play_place_jenga();
            });
    }

    /// <summary>
    /// 無限につむつむする デバッグ用
    /// </summary>
    void TestAutoPut()
    {
        // つむつむ
        Observable.Interval(TimeSpan.FromSeconds(0.01f))
            .Subscribe(_ =>
            {
                creatJengaTower.AlignJenga();
                soundPlayer.play_place_jenga();
            });
    }

    /// <summary>
    /// 現在ジェンガが何段積まれているかをテキスト表示
    /// </summary>
    void CountLayer()
    {
        // 段
        var countLayer = creatJengaTower._layerCount.ToString();
        // 個
        var countPiece = creatJengaTower._replaceCount.ToString();

        if (countLayer.Length <= 22)
            layerCounter.text = countLayer + "段\r\n" + countPiece + "個";
    }

    /// <summary>
    /// どれくらいのドラッグをしているかを表示
    /// </summary>
    void CountPower()
    {
        var countText = powerCalculation._forceVec.magnitude.ToString();

        if (countText.Length >= 5)
        {
            var cou = countText.Remove(4);
            powerCounter.text = "Power : " + cou;
        }
        else
        {
            powerCounter.text = "Power : " + countText;
        }
    }

    /// <summary>
    /// ゲームを終了させる
    /// </summary>
    void FinishGame()
    {
        gameOver.enabled = true;
        gameOverPanel.enabled = true;
        retry.gameObject.SetActive(true);
        BackToTitle.gameObject.SetActive(true);
        //putOneLayer.interactable = false;
        //autoPut.interactable = false;
        tutorial.interactable = false;
        //ranking.SetActive(true);
    }
}
