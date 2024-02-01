using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    public static GameManagerScript instance;
    
    public bool altMusic = false;

    private string _currentLevel;

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

        _currentLevel = SceneManager.GetActiveScene().name;
    }

    private void Start()
    {
        switch (_currentLevel)
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
}
