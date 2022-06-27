using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI enemyCountText;
    [SerializeField] private Image healthBar;

    [SerializeField] private Image circleAmmoImage;
    [SerializeField] private Image squareAmmoImage;
    [SerializeField] private Image triangleAmmoImage;

    [SerializeField] private List<GameObject> rapidFireIcons = new List<GameObject>();

    
    private Animator circleAnimator;
    private Animator squareAnimator;
    private Animator triangleAnimator;

    private void Awake()
    {
        circleAnimator = circleAmmoImage.GetComponent<Animator>();
        squareAnimator = squareAmmoImage.GetComponent<Animator>();
        triangleAnimator = triangleAmmoImage.GetComponent<Animator>();

        EnableRapidFireIcons(false);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

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
}
