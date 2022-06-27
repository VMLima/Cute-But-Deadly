using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource backgroundMusicAS;

    private void OnEnable()
    {
        GameManager.GameStarted += StartBGM;
    }
    private void OnDisable()
    {
        GameManager.GameStarted -= StartBGM;
    }
    private void StartBGM()
    {
        backgroundMusicAS.Play();
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetVolume(float percent)
    {
        backgroundMusicAS.volume = percent;
    }
}
