using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    private GameObject[] BackGround;
    private bool canCreate;
    private bool isGameOver;

    public GameObject gameOverPanel;
    public GameObject PausePaenl;
    public TextMeshProUGUI scoreText;
    private int score;

    public AudioController bgm;
    public Image musicButton;
    public Sprite closeMusic;
    public Sprite openMusic;

    public Sfx sfx;
    public Image sfxButton;
    public Sprite closesfx;
    public Sprite opensfx;

    public bool GetCanCreate
    {
        set { canCreate = value; }
        get { return canCreate; }
    }

    void Start()
    {
        canCreate = true;
        isGameOver = false;
        BackGround = Resources.LoadAll<GameObject>("BG");

        bgm = FindObjectOfType<AudioController>();
        sfx = FindObjectOfType<Sfx>();

        score = 0;
        UpdateScoreText();

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }

        InitializeAudioSettings();

        ToggleCanvas();
    }

    void Update()
    {
        if (canCreate && !isGameOver)
        {
            GameObject bg = Instantiate(BackGround[Random.Range(0, BackGround.Length)], new Vector3(17.5f, 2.49f, 5), Quaternion.identity);
            if (bg.GetComponent<BGmove>() == null)
            {
                bg.AddComponent<BGmove>();
            }
            canCreate = false;
        }
    }

    public void GameOver()
    {
        isGameOver = true;
        bgm.StopAudio();

        Time.timeScale = 0;
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1;

        bgm.PlayAudio();

        isGameOver = false;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }

    public void LoadSceneByIndex()
    {
        SceneManager.LoadScene(0);
        bgm.PlayAudio();
    }

    public void ToggleCanvas()
    {
        PausePaenl.SetActive(!PausePaenl.activeSelf);

        if (PausePaenl.activeSelf)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void ToggleMusic()
    {
        if (bgm.IsOff())
        {
            musicButton.sprite = openMusic;
            bgm.SetVolume(1);
            PlayerPrefs.SetInt("MusicMuted", 0);
        }
        else
        {
            musicButton.sprite = closeMusic;
            bgm.SetVolume(0);
            PlayerPrefs.SetInt("MusicMuted", 1);
        }
        PlayerPrefs.Save();
    }

    public void ToggleSfx()
    {
        if (sfx.IsOff())
        {
            sfxButton.sprite = opensfx;
            sfx.SetVolume(1);
            PlayerPrefs.SetInt("SfxMuted", 0);
        }
        else
        {
            sfxButton.sprite = closesfx;
            sfx.SetVolume(0);
            PlayerPrefs.SetInt("SfxMuted", 1);
        }
        PlayerPrefs.Save();
    }

    // 初始化音频设置
    private void InitializeAudioSettings()
    {
        if (PlayerPrefs.GetInt("MusicMuted", 0) == 1)
        {
            bgm.SetVolume(0);
            musicButton.sprite = closeMusic;
        }
        else
        {
            bgm.SetVolume(1);
            musicButton.sprite = openMusic;
        }

        if (PlayerPrefs.GetInt("SfxMuted", 0) == 1)
        {
            sfx.SetVolume(0);
            sfxButton.sprite = closesfx;
        }
        else
        {
            sfx.SetVolume(1);
            sfxButton.sprite = opensfx;
        }
    }
}


