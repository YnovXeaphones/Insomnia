using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI playText;
    [SerializeField] private GameObject continueButton;
    [SerializeField] private GameObject optionsMenu;
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        Debug.Log("Scene Loaded");
        Time.timeScale = 1;
    }

    public void NewGame(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        PlayerPrefs.SetFloat("respawn.x", 0);
        PlayerPrefs.SetFloat("respawn.y", 0);
        PlayerPrefs.SetFloat("respawn.z", 0);
        Time.timeScale = 1;
    }

    private void Start() {
        if (PlayerPrefs.HasKey("respawn.x") && PlayerPrefs.HasKey("respawn.y") && PlayerPrefs.HasKey("respawn.z") && (PlayerPrefs.GetFloat("respawn.x") != 0 || PlayerPrefs.GetFloat("respawn.y") != 0 || PlayerPrefs.GetFloat("respawn.z") != 0))
        {
            continueButton.SetActive(true);
            playText.text = "Continue";
            optionsMenu.SetActive(true);
        }
        else
        {
            continueButton.SetActive(false);
            playText.text = "Play";
            optionsMenu.SetActive(false);
        }
    }
}
