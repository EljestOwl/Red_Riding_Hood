using System.Collections;
using UnityEngine;

public class LevelTransition : MonoBehaviour
{
	[SerializeField] private string toScene;
	[SerializeField] private float delaySeconds;

	IEnumerator TriggerSceneWithDelay()
	{
		yield return new WaitForSeconds(delaySeconds);
		GameManagerScript.instance.ChangeScene(toScene);
	}
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			StartCoroutine(TriggerSceneWithDelay());
		}
	}
}
