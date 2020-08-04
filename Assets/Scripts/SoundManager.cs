using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    [Header("Only use this script to play small audio clips")]
    [Header("Small Audio Clips not to play in loop.")]
    [SerializeField] private AudioClip coinCollectSound;
    [SerializeField] private AudioSource outputSource;

    public void PlayCoinCollectSound()
    {
        outputSource.PlayOneShot(coinCollectSound);
    }

}
