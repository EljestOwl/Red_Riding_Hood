using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
	public static GameManagerScript instance;
	[SerializeField] private GameObject _gameOverUI;
	[SerializeField] private TextMeshProUGUI _deathText;

	public bool altMusic = false;

	[HideInInspector] public string currentLevel;

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else
		{
			Destroy(gameObject);
			return;
		}

		DontDestroyOnLoad(gameObject);

		currentLevel = SceneManager.GetActiveScene().name;
	}

	private void Start()
	{
		initializeLevel();
	}

	public void ChangeScene(string newScene)
	{
		SceneManager.LoadScene(newScene);
	}

	private void initializeLevel()
	{
		switch (currentLevel)
		{
			case "Level1":
				AudioMangagerScript.instance.PlaySound("Level1");
				break;
			case "Level2":
				if (altMusic)
				{
					AudioMangagerScript.instance.PlaySound("Level2Alt");
				}
				else
				{
					AudioMangagerScript.instance.PlaySound("Level2");
				}
				break;
			case "Level3":
				AudioMangagerScript.instance.PlaySound("Level3Calm");
				break;
			default: // MainMenu
				AudioMangagerScript.instance.PlaySound("Level1");
				break;
		}
	}

	public void GameOver(string causeOfDeath)
	{
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		player.GetComponent<PlayerInputHandler>().enabled = false;

		_deathText.text = causeOfDeath;

		Time.timeScale = 0.5f;
		_gameOverUI.SetActive(true);
	}
}
