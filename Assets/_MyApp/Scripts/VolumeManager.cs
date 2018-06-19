using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ゲーム中で鳴る音量の調節を行う
/// GameManagerにアタッチ
/// </summary>
public class VolumeManager : MonoBehaviour
{
    [SerializeField]
    UnityEngine.Audio.AudioMixer mixer;
    [SerializeField, Range(0, 1)]
    float MasterValue = 1;
    [SerializeField, Range(0, 1)]
    float SEValue = 1;
    [SerializeField, Range(0, 1)]
    float BGMValue = 1;

    void Update()
    {
        BGMVolume();
        SEVolume();
    }

    void MasterVolume()
    {
        mixer.SetFloat("MasterVolume", Mathf.Lerp(-80, 0, MasterValue));
    }

    void BGMVolume()
    {
        mixer.SetFloat("BGMVolume", Mathf.Lerp(-80, 0, BGMValue));
    }

    void SEVolume()
    {
        mixer.SetFloat("SEVolume", Mathf.Lerp(-80, 0, SEValue));
    }
}
