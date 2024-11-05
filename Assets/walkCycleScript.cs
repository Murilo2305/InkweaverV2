using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class walkCycleScript : MonoBehaviour
{
    public bool IsWalking;

    [SerializeField] private AudioSource ASource;

    [SerializeField] private float timerToResetAudio;
    [SerializeField] private float timerToResetAudioMax = 0.5f;


    private void Start()
    {
        ASource = gameObject.GetComponent<AudioSource>();
    }

    private void Update()
    {
        if(IsWalking && !ASource.isPlaying && Time.timeScale != 0.0f)
        {
            ASource.Play();
            ASource.pitch = Random.Range(0.9f, 1.2f);
            ResetTimer();
        }
        else if (!IsWalking)
        {
            SubtractTimer();
            if(timerToResetAudio <= 0.0f)
            {
                timerToResetAudio = 0.0f;
                ASource.Stop();
            }
        }
    }


    private void ResetTimer()
    {
        timerToResetAudio = timerToResetAudioMax;
    }

    private void SubtractTimer()
    {
        timerToResetAudio -= Time.deltaTime;
    }
}
