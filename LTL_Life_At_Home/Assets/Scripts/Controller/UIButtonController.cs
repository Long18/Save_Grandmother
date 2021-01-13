using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButtonController : MonoBehaviour
{
    public GameObject mainMenu, pauseMenu, overMenu, HUD;
    public static UIButtonController instance;
    public Sprite musicon, musicoff, soundon, soundoff;
    public Image soundBtn, musicBtn;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }
    }
    public void OnPlayClick()
    {
        mainMenu.SetActive(false);
        HUD.SetActive(true);
        GameController.instance.gameStage = GameStage.Playing;
        Time.timeScale = 1;
        SoundManager.instance.PlayOneShotClip("Button");
    }
    public void OnSoundClick()
    {
        if (!SoundManager.instance.soundMute)
        {
            SoundManager.instance.soundMute = true;
            soundBtn.sprite = soundoff;
            SoundManager.instance.BGaudio.Stop();
        }
        else
        {
            SoundManager.instance.soundMute = false;
            soundBtn.sprite = soundon;
            SoundManager.instance.PlayOneShotClip("Button");
            SoundManager.instance.BGaudio.Play();
        }
    }
    public void OnMusicClick()
    {
        if (!SoundManager.instance.musicMute)
        {
            SoundManager.instance.musicMute = true;
            musicBtn.sprite = musicoff;
        }
        else
        {
            SoundManager.instance.musicMute = false;
            musicBtn.sprite = musicon;
            SoundManager.instance.PlayOneShotClip("Button");
        }
    }
    public void OnPauseClick()
    {
        HUD.SetActive(false);
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        GameController.instance.gameStage = GameStage.Pause;
        SoundManager.instance.PlayOneShotClip("Button");
    }
    public void OnResumeClick()
    {
        HUD.SetActive(true);
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        GameController.instance.gameStage = GameStage.Playing;
        SoundManager.instance.PlayOneShotClip("Button");
    }
    public void OnHomeClick()
    {
        mainMenu.SetActive(true);
        overMenu.SetActive(false);
        HUD.SetActive(false);
        pauseMenu.SetActive(false);
        GameController.instance.gameStage = GameStage.Ready;
        SoundManager.instance.PlayOneShotClip("Button");
    }
    public void OnRePlayClick()
    {
        HUD.SetActive(true);
        overMenu.SetActive(false);
        GameController.instance.gameStage = GameStage.Playing;
        Time.timeScale = 1;
        SoundManager.instance.PlayOneShotClip("Button");
    }
}
