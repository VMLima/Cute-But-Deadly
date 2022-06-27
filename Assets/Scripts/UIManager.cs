using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UIManager : MonoBehaviour
{
    [SerializeField] private float fadeTime = 4;
    [SerializeField] private CanvasGroup blackScreen;

    [SerializeField] private AudioManager audioManager;

    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI enemyCountText;
    [SerializeField] private Image healthBar;

    [SerializeField] private Image circleAmmoImage;
    [SerializeField] private Image squareAmmoImage;
    [SerializeField] private Image triangleAmmoImage;

    [SerializeField] private List<GameObject> rapidFireIcons = new List<GameObject>();

    [SerializeField] private CanvasGroup victoryScreen;
    [SerializeField] private CanvasGroup defeatScreen;
    [SerializeField] private List<GameObject>  endOfGameButtons = new List<GameObject>();
    
    private Animator circleAnimator;
    private Animator squareAnimator;
    private Animator triangleAnimator;

    private Coroutine fadeCoroutine;
    public event Action FadeCompleted;

    private void Awake()
    {
        circleAnimator = circleAmmoImage.GetComponent<Animator>();
        squareAnimator = squareAmmoImage.GetComponent<Animator>();
        triangleAnimator = triangleAmmoImage.GetComponent<Animator>();

        EnableRapidFireIcons(false);

        victoryScreen.DisableCanvasGroup(0);
        defeatScreen.DisableCanvasGroup(0);
        //blackScreen.alpha = 1;

        foreach (var button in endOfGameButtons)
        {
            button.SetActive(false);
        }
    }

    public void ShowVictoryScreen()
    {
        victoryScreen.EnableCanvasGroup(1);

        foreach (var button in endOfGameButtons)
        {
            button.SetActive(true);
        }
    }
    public void ShowDefeatScreen()
    {
        defeatScreen.EnableCanvasGroup(1);

        foreach (var button in endOfGameButtons)
        {
            button.SetActive(true);
        }
    }

    public void SetWaveTimer(float timer)
    {
        timerText.text = Mathf.Floor(timer).ToString();
    }
    public void DisableWaveTimer()
    {
        timerText.gameObject.SetActive(false);
    }
    public void SetEnemyCount(int count)
    {
        enemyCountText.text = count.ToString();
    }
    public void SetPlayerHealth(float healthPercent)
    {
        healthBar.fillAmount = healthPercent;
    }
    public void EnableRapidFireIcons(bool enabled)
    {
        foreach (var icon in rapidFireIcons)
        {
            icon.SetActive(enabled);
        }
    }
    public void SetAmmoType(AmmoType ammoType)
    {
        switch (ammoType)
        {
            case AmmoType.Circle:
                circleAnimator.SetInteger("Animate",1);
                squareAnimator.SetInteger("Animate", 0);
                triangleAnimator.SetInteger("Animate", 0);
                break;
            case AmmoType.Square:
                circleAnimator.SetInteger("Animate", 0);
                squareAnimator.SetInteger("Animate", 1);
                triangleAnimator.SetInteger("Animate", 0);
                break;
            case AmmoType.Triangle:
                circleAnimator.SetInteger("Animate", 0);
                squareAnimator.SetInteger("Animate", 0);
                triangleAnimator.SetInteger("Animate", 1);
                break;
            default:
                break;
        }
    }

    public void FadeOut()
    {
        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);

        fadeCoroutine = StartCoroutine(FadeOutCoroutine());
    }
    public void FadeIn()
    {
        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);

        fadeCoroutine = StartCoroutine(FadeInCoroutine());
    }
    IEnumerator FadeOutCoroutine()
    {
        float timer = fadeTime * blackScreen.alpha;
        
        while (timer < fadeTime)
        {
            blackScreen.alpha = timer / fadeTime;
            audioManager.SetVolume(1 - (timer / fadeTime));
            timer += Time.deltaTime;
            yield return null;
        }
        FadeCompleted?.Invoke();
    }
    IEnumerator FadeInCoroutine()
    {
        float timer = fadeTime * blackScreen.alpha;

        while (timer > 0)
        {
            blackScreen.alpha = timer / fadeTime;

            timer -= Time.deltaTime;
            yield return null;
        }
        FadeCompleted?.Invoke();
    }
}
