using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class CrossBall : MonoBehaviour
{
	[SerializeField] public SpriteRenderer crossBallRenderer;
	[SerializeField] public CrossPath path;
	[SerializeField] public float[] tweenSpeeds;
	[SerializeField] public Ease tweenEaseFunction;
	public float CrossBallRendererRadius => crossBallRenderer.bounds.size.x / 2;
	[HideInInspector] public float tweenSpeed;
	[HideInInspector] public bool IsCurrentlyTweening;
	[SerializeField] public GameObject crossBallDestroy;
	public Action CrossBallDestroyed;
	private void Awake()
	{
		EnhancedTouchSupport.Enable();
		TouchSimulation.Enable();
	}

	private void Start()
	{
		tweenSpeed = tweenSpeeds[ProjectManagment.Manager.StartAdvance];
	}

	public void EnableCrossBall()
	{
		Touch.onFingerDown += GoToPathPoint;
	}

	public void DisableCrossBall()
	{
		Touch.onFingerDown -= GoToPathPoint;
	}

	public void GoToPathPoint(Finger finger)
	{
		if (IsCurrentlyTweening) return;

		IsCurrentlyTweening = true;
		float tweenTime = Vector2.Distance(path.nextPathPoint.transform.position, transform.position) / tweenSpeed;
		transform.DOMove(path.nextPathPoint.transform.position, tweenTime).SetEase(tweenEaseFunction).OnComplete(OnTweenCompleted);
	}

	public void OnTweenCompleted()
	{
		path.GenerateNextPathPoint();
		transform.DOKill();
	}

	public void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.TryGetComponent<BurnFloor>(out var burnFloor))
		{
			DestroyCross();
		}
	}

	public void DestroyCross()
	{
		crossBallDestroy.SetActive(true);
		crossBallRenderer.enabled = false;
		DisableCrossBall();
		transform.DOKill();
		CrossBallDestroyed?.Invoke();
	}

	// public void OnTriggerEnter2D(Collider2D pathPoint)
	// {
	// 	if (pathPoint.TryGetComponent<PathPoint>(out PathPoint point))
	// 	{

	// 	}
	// }



	// public IEnumerator CheckCenterMatch(Transform toCenter, Vector2 initialVector)
	// {
	// 	while (Vector2.Dot(toCenter.transform.position - transform.position, initialVector) > 0)
	// 	{
	// 		yield return null;
	// 	}

	// 	IsCurrentlyTweening = false;
	// }

	public void OnDestroy()
	{
		Touch.onFingerDown -= GoToPathPoint;
	}
}
