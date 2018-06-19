using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// GameManagerにアタッチ
// アプリケーションの終了を監視する

public class QuitCheckManager : MonoBehaviour
{
    [SerializeField]
    CalcRealDeltaTime calcTime;

    // 画面に表示するUI
    [SerializeField]
    GameObject quitCheck;
    [SerializeField]
    Button yesButton;
    [SerializeField]
    Button noButton;

    [SerializeField]
    AudioSource generalDecision;  // メニューを決定するときの音

    [SerializeField]
    float quit_time = 1;    // ゲーム終了を選んでから実際にアプリケーションを終了するまでの時間
    float quit_deltatime;

    bool is_canceled;  // ゲーム終了を取り消したかどうか

    public bool cancelCheck
    {
        get { return is_canceled; }
        set { is_canceled = value; }
    }

    void Start()
    {
        quitCheck.SetActive(false);
        quit_deltatime = 0;
        is_canceled = true;

        // はいボタンの挙動
        yesButton.onClick.AddListener(() =>
        {
            OnCallExit();
        });

        // いいえボタンの挙動
        noButton.onClick.AddListener(() =>
        {
            OnCallCancel();
        });
    }

    /// <summary>
    /// アプリケーションを終了する前に警告を表示
    /// </summary>
    void OnApplicationQuit()
    {
        if (quitCheck.activeInHierarchy == false)
            Application.CancelQuit();

        quitCheck.SetActive(true);
        is_canceled = false;
        Time.timeScale = 0;
    }

    /// <summary>
    /// ゲーム終了を実行
    /// </summary>
    public void OnCallExit()
    {
        Application.Quit();
    }

    /// <summary>
    /// ゲーム終了をキャンセル
    /// </summary>
    public void OnCallCancel()
    {
        Time.timeScale = 1.0f;
        is_canceled = true;
        quitCheck.SetActive(false);
    }
}
