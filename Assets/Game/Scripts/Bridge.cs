using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Bridge : MonoBehaviour
{
   private void OnTriggerEnter2D(Collider2D other)
   {
	if (other.CompareTag("Player"))
	{
		other.GetComponent<PlayerInputHandler>().enabled = false;
		other.GetComponent<PlayerInput>().enabled = false;
		
		GetComponent<PolygonCollider2D>().enabled = false;
		GetComponent<Animator>().Play("Collapse");
	}
   }
}
