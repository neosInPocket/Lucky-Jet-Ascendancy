using UnityEngine;

public class BurnFloor : MonoBehaviour
{
	[SerializeField] private float floorSpeed;
	public Vector2 CurrentPosition;
	private bool floorEnabled;

	private void Start()
	{
		CurrentPosition = transform.position;
	}

	public void EnableFloor()
	{
		floorEnabled = true;
	}

	public void DisableFloor()
	{
		floorEnabled = false;
	}

	private void Update()
	{
		if (!floorEnabled) return;

		CurrentPosition.y += floorSpeed * Time.deltaTime;
		transform.position = CurrentPosition;
	}
}
