using DG.Tweening;
using UnityEngine;

public class PathPoint : MonoBehaviour
{
	[SerializeField] private float scaleMinValue;
	[SerializeField] private float rotationDuration;
	[SerializeField] private Ease ease;
	[SerializeField] private SpriteRenderer pathRenderer;
	[SerializeField] private Color completedColor;
	[SerializeField] private GameObject completedEffects;

	void Start()
	{
		transform.DOScale(new Vector3(scaleMinValue, scaleMinValue, scaleMinValue), rotationDuration).SetLoops(-1, LoopType.Yoyo).SetEase(ease);
	}

	public void EnableCompletedEffects()
	{
		completedEffects.SetActive(true);
		pathRenderer.color = completedColor;
		transform.DOKill();
		transform.localScale = new Vector3(1, 1, 1);
	}
}
