using System;
using System.Linq;
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
	[SerializeField] public GameObject crossBallDestroy;
	public Action CrossBallDestroyed;
	public Action PathPointHit;
	private Ease currentEase;
	private bool isInTrigger;
	private void Awake()
	{
		EnhancedTouchSupport.Enable();
		TouchSimulation.Enable();
	}

	private void Start()
	{
		tweenSpeed = tweenSpeeds[ProjectManagment.Manager.StartAdvance];
		currentEase = ProjectManagment.Manager.EndAdvance == 1 ? Ease.Linear : tweenEaseFunction;
	}

	public void EnableCrossBall()
	{
		Touch.onFingerDown += GoToPathPoint;
	}

	public void DisableCrossBall()
	{
		Touch.onFingerDown -= GoToPathPoint;
		Touch.onFingerDown -= StopCrossBall;
	}

	public void GoToPathPoint(Finger finger)
	{
		Touch.onFingerDown -= GoToPathPoint;
		Touch.onFingerDown += StopCrossBall;

		Ray ray = Camera.main.ScreenPointToRay(finger.screenPosition);
		RaycastHit hit;

		Physics.Raycast(ray, out hit);
		Vector2 tapPosition = ray.origin;

		if (tapPosition.y < transform.position.y)
		{
			Touch.onFingerDown += GoToPathPoint;
			Touch.onFingerDown -= StopCrossBall;
			return;
		}

		float tweenTime = Vector2.Distance(tapPosition, transform.position) / tweenSpeed;
		transform.DOMove(tapPosition, tweenTime).SetEase(currentEase).OnComplete(TweenCompleted);
	}

	public void TweenCompleted()
	{
		StopCrossBall(null);
	}

	public void StopCrossBall(Finger finger)
	{
		Touch.onFingerDown -= StopCrossBall;
		Touch.onFingerDown += GoToPathPoint;

		transform.DOKill();

		if (isInTrigger)
		{
			PathPointHit?.Invoke();
			OnTweenCompleted();
		}
		else
		{
			DestroyCross();
		}
	}

	public void OnTweenCompleted()
	{
		path.GenerateNextPathPoint();
	}

	public void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.TryGetComponent<BurnFloor>(out var burnFloor))
		{
			DestroyCross();
			return;
		}

		if (collider.TryGetComponent<BridgeZone>(out var zone))
		{
			DestroyCross();
			return;
		}

		if (collider.TryGetComponent<PathPoint>(out var PathPoint))
		{
			isInTrigger = true;
		}
	}

	public void OnTriggerExit2D(Collider2D collider)
	{
		if (collider.TryGetComponent<PathPoint>(out var PathPoint))
		{
			isInTrigger = false;
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

	public void OnDestroy()
	{
		Touch.onFingerDown -= GoToPathPoint;
		Touch.onFingerDown -= StopCrossBall;
	}
}
