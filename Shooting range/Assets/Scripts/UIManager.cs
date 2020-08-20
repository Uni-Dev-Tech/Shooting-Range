using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    static public UIManager instance;
    private float soundSettings;
    private float musicSettings;
    public GameObject mainMenuPanel, mainMenuSettings, pause, gameplayUI, lose;
    public Slider musicSliderMainMenu, soundSliderMainMenu;
    public Slider musicSliderPause, soundSliderPause;
    public int birdsPoints;
    public Text birdsPointsForward, birdsPointsBack;
    public int timer;
    private bool soundTimer;
    private bool soundLose = true;
    public Text timerScoreForward, timerScoreBack;
    public Slider bonus;

    private void Awake()
    {
        if (UIManager.instance != null)
        {
            Destroy(gameObject);
            return;
        }
        UIManager.instance = this;
    }
    private void Start()
    {
        Time.timeScale = 0f;
        InitUI();
        if (PlayerPrefs.HasKey("Reload"))
            if(PlayerPrefs.GetFloat("Reload") == 1f)
            {
                PlayerPrefs.SetFloat("Reload", 0f);
                Switch(mainMenuPanel, gameplayUI);
                Time.timeScale = 1f;
                StartCoroutine(Timer());
            }
        InitSound();
    }
    private void Update()
    {
        GameScore(birdsPoints, timer);
        if (bonus.value == 1f)
        {
            bonus.value *= 0f;
            WorldController.instance.AddBonus();
        }
        if (timer < 6 && soundTimer)
        {
            soundTimer = false;
            SoundManager.instance.PlaySound(SoundManager.instance.timer);
        }
        if (timer > 6) soundTimer = true;
        if (birdsPoints < 1 || timer < 1)
        {
            SoundManager.instance.musicSource.Stop();
            if (soundLose)
                SoundManager.instance.PlaySound(SoundManager.instance.lose);
            soundLose = false;
            Time.timeScale = 0f;
            Switch(gameplayUI, lose);
        }
        if(Input.GetKeyDown(KeyCode.Escape) && gameplayUI.activeSelf ||
           Input.GetKeyDown(KeyCode.Escape) && pause.activeSelf)
        {
            if (!pause.activeSelf)
            {
                Time.timeScale = 0f;
                Switch(gameplayUI, pause);
            }
            else
            {
                Time.timeScale = 1f;
                Switch(pause, gameplayUI);
            }
        }
    }

    #region Main Menu
    public void StartButton()
    {
        SoundManager.instance.PlaySound(SoundManager.instance.click);
        Switch(mainMenuPanel, gameplayUI);
        Time.timeScale = 1f;
        StartCoroutine(Timer());
    }
    public void Settings()
    {
        SoundManager.instance.PlaySound(SoundManager.instance.click);
        Switch(mainMenuPanel, mainMenuSettings);
    }
    public void SliderSound(float vol)
    {
        soundSettings = vol;
        PlayerPrefs.SetFloat("Sound", soundSettings);
    }
    public void SliderMusic(float vol)
    {
        musicSettings = vol;
        PlayerPrefs.SetFloat("Music", musicSettings);
    }
    public void ExitSettings()
    {
        SoundManager.instance.PlaySound(SoundManager.instance.click);
        Switch(mainMenuSettings, mainMenuPanel);
    }
    public void ExitButton()
    {
        Application.Quit();
    }
    #endregion

    #region Pause
    public void Continue()
    {
        SoundManager.instance.PlaySound(SoundManager.instance.click);
        Time.timeScale = 1f;
        Switch(pause, gameplayUI);
    }
    public void MainMenu()
    {
        SoundManager.instance.PlaySound(SoundManager.instance.click);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    #endregion

    #region Lose
    public void TryAgain()
    {
        SoundManager.instance.PlaySound(SoundManager.instance.click);
        PlayerPrefs.SetFloat("Reload", 1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    #endregion

    #region GamePlay
    public void PauseButton()
    {
        SoundManager.instance.PlaySound(SoundManager.instance.click);
        Time.timeScale = 0f;
        Switch(gameplayUI, pause);
    }
    #endregion

    #region Методы
    public void Switch(GameObject off, GameObject on)
    {
        off.SetActive(false);
        on.SetActive(true);
    }
    public void InitUI()
    {
        mainMenuPanel.SetActive(true);
        mainMenuSettings.SetActive(false);
        pause.SetActive(false);
        gameplayUI.SetActive(false);
        lose.SetActive(false);
        birdsPoints = 10;
        timer = 30;
    }
    public void InitSound()
    {
        if (!PlayerPrefs.HasKey("Sound"))
            PlayerPrefs.SetFloat("Sound", 0.5f);
        if (!PlayerPrefs.HasKey("Music"))
            PlayerPrefs.SetFloat("Music", 0.5f);
        soundSettings = PlayerPrefs.GetFloat("Sound");
        soundSliderMainMenu.value = soundSettings;
        soundSliderPause.value = soundSettings;
        musicSettings = PlayerPrefs.GetFloat("Music");
        musicSliderMainMenu.value = musicSettings;
        musicSliderPause.value = musicSettings;
    }
    public void GameScore(int birdsPoints, int timer)
    {
        birdsPointsForward.text = birdsPoints.ToString();
        birdsPointsBack.text = birdsPointsForward.text;

        timerScoreForward.text = timer.ToString();
        timerScoreBack.text = timerScoreForward.text;
    }
    IEnumerator Timer()
    {
        do
        {
            yield return new WaitForSeconds(1f);
            timer--;
        } while (true);
    }
    #endregion
}
