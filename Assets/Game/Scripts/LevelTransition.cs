using UnityEngine;

public class LevelTransition : MonoBehaviour
{
	[SerializeField] private string toScene;
	
	private void OnTriggerEnter2D(Collider2D other) 
	{
		if (other.CompareTag("Player"))
		{
			GameManagerScript.instance.ChangeScene(toScene);
		}
	}
}
