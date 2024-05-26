using System.Collections;
using UnityEngine;

public class BridgeZone : MonoBehaviour
{
	[SerializeField] private GameObject bridge;
	[SerializeField] private ParticleSystem bridgeParticles;
	[SerializeField] private Collider2D edge;
	[SerializeField] private float blinkTime;
	[SerializeField] private float sleepTime;

	private void Start()
	{
		StartCoroutine(ChangeElectricFieldState());
	}

	public IEnumerator ChangeElectricFieldState()
	{
		yield return new WaitForSeconds(blinkTime);
		bridgeParticles.Stop(false, ParticleSystemStopBehavior.StopEmitting);
		bridge.SetActive(false);
		edge.enabled = false;
		yield return new WaitForSeconds(sleepTime);
		bridgeParticles.Play();
		bridge.SetActive(true);
		edge.enabled = true;
		StartCoroutine(ChangeElectricFieldState());
	}
}
