using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 音声を鳴らす
/// </summary>
public class SoundPlayer : MonoBehaviour
{
    [SerializeField]
    AudioSource place_jenga;

    public void play_place_jenga()
    {
        place_jenga.Play();
    } 
}
