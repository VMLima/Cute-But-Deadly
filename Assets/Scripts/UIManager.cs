using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI enemyCountText;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Image ammoTypeImage;

    [Space]
    [SerializeField] private Sprite circleSprite;
    [SerializeField] private Sprite squareSprite;
    [SerializeField] private Sprite triangleSprite;

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
    public void SetPlayerHealth(int health)
    {
        healthSlider.value = health;
    }
    public void SetAmmoType(AmmoType ammoType)
    {
        switch (ammoType)
        {
            case AmmoType.Circle:
                ammoTypeImage.sprite = circleSprite;
                break;
            case AmmoType.Square:
                ammoTypeImage.sprite = squareSprite;
                break;
            case AmmoType.Triangle:
                ammoTypeImage.sprite = triangleSprite;
                break;
            default:
                break;
        }
    }
}
