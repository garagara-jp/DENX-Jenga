using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UniRx;
using System;

/// <summary>
/// タイトル画面のUI操作と進行を管理
/// </summary>
public class TitleManager : MonoBehaviour
{
    [SerializeField]
    CreatJengaTower creatJengaTower;
    [SerializeField]
    SceneMovingManager sceneMovingManager;
    [SerializeField]
    RankingManager rankingManager;

    // 画面に表示するUI
    [SerializeField]
    Button gameStartButton;
    [SerializeField]
    Button rankingButton;
    [SerializeField]
    Button tutorialButton;
    [SerializeField]
    Button gameQuitButton;
    [SerializeField]
    Button creditsButton;
    [SerializeField]
    GameObject tutorial;
    [SerializeField]
    Button tutorialBackButton;
    [SerializeField]
    GameObject credits;

    // UI操作で使うAudioSource
    [SerializeField]
    AudioSource generalDecisionSound;
    [SerializeField]
    AudioSource gameStartSound;

    [SerializeField]
    GameObject Tenban; // ジェンガの接触を判定するオブジェクト

    [SerializeField]
    int putting_number = 18;     // AutoPut()で積み上げる段数

    void Start()
    {
        Time.timeScale = 2f;
        AutoPut();

        // UIの初期化
        tutorial.SetActive(false);
        credits.SetActive(false);

        // はじめるボタンの挙動
        gameStartButton.onClick.AddListener(() =>
        {
            gameStartSound.Play();
            sceneMovingManager._sceneNumber = 0;
            sceneMovingManager.moveTiming = true;
        });

        // ランキングボタンの挙動
        rankingButton.onClick.AddListener(() =>
        {
            generalDecisionSound.Play();
            rankingManager.ShowRanking(true);
        });

        // あそびかたボタンの挙動
        tutorialButton.onClick.AddListener(() =>
        {
            generalDecisionSound.Play();
            tutorial.SetActive(true);
        });

        // あそびかたのもどるボタンの挙動
        tutorialBackButton.onClick.AddListener(() =>
        {
            generalDecisionSound.Play();
            tutorialBackButton.image.color = Color.white;
            tutorial.SetActive(false);
        });

        // おわるボタンの挙動
        gameQuitButton.onClick.AddListener(() =>
        {
            generalDecisionSound.Play();
            Application.Quit();
        });

        // クレジットボタンの挙動
        creditsButton.onClick.AddListener(() =>
        {
            generalDecisionSound.Play();
            ShowCredits(true);
        });
    }

    void Update()
    {
        // クレジット画面を閉じる
        if (Input.GetMouseButtonDown(0))
        {
            ShowCredits(false);
            rankingManager.ShowRanking(false);
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
            });
    }

    /// <summary>
    /// クレジットを表示
    /// </summary>
    public void ShowCredits(bool isShowed)
    {
        if (isShowed)
        {
            credits.SetActive(true);
        }
        else
        {
            credits.SetActive(false);
        }
    }
}
