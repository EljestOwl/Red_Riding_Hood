using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
	public static bool GameIsPaused = false;
	public GameObject pauseMenuUI;
	public GameObject creditsUI;

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
		{
			if (GameIsPaused)
			{
				Resume();
			}
			else
			{
				Pause();
			}
		}
	}

	public void Resume()
	{
		pauseMenuUI.SetActive(false);
		Time.timeScale = 1f;
		GameIsPaused = false;
	}

	public void Pause()
	{
		pauseMenuUI.SetActive(true);
		Time.timeScale = 0f;
		GameIsPaused = true;
	}

	public void ButtonRestart()
	{
		pauseMenuUI.SetActive(false);
		Time.timeScale = 1f;
		GameIsPaused = false;
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public void ButtonMainMenu()
	{
		SceneManager.LoadScene("MainMenu");
	}

	public void ButtonPlay()
	{
		SceneManager.LoadScene("Level1");
	}
	public void ButtonToggleCredits()
	{
		if (creditsUI.activeSelf)
		{
			creditsUI.SetActive(false);
		}
		else
		{
			creditsUI.SetActive(true);
		}

	}

	public void ButtonOptions()
	{

	}

	public void ButtonQuit()
	{
		Application.Quit();
	}
}
