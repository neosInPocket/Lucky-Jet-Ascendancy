using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using Finger = UnityEngine.InputSystem.EnhancedTouch.Finger;

public class BriefingProject : MonoBehaviour
{
	[SerializeField] private TMP_Text useText;
	[SerializeField] private Image uiImageCursor;
	public string[] briefTexts;
	public Action Briefed { get; private set; }
	private Animator briefAnimatorObject;
	[HideInInspector] public int currentBriefing;

	private void Start()
	{
		briefAnimatorObject = uiImageCursor.GetComponent<Animator>();
	}

	public void SetBrief(Action briefEnd)
	{
		Briefed = briefEnd;
		currentBriefing = 0;
		useText.text = briefTexts[currentBriefing];
		Touch.onFingerDown += ExecuteBrief;

		gameObject.SetActive(true);
	}

	public void ExecuteBrief(Finger finger)
	{
		currentBriefing++;

		if (currentBriefing >= briefTexts.Length)
		{
			UnsetBrief();
			return;
		}

		useText.text = briefTexts[currentBriefing];
		briefAnimatorObject.SetTrigger("Brief");
	}

	private void UnsetBrief()
	{
		Briefed();
		Touch.onFingerDown -= ExecuteBrief;

		if (gameObject != null)
		{
			gameObject.SetActive(false);
		}
	}

	public void OnDestroy()
	{
		Touch.onFingerDown -= ExecuteBrief;
	}
}
