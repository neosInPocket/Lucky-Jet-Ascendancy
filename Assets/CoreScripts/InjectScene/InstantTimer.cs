using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class InstantTimer : MonoBehaviour
{
	[SerializeField] private TMP_Text timer;
	[SerializeField] private float defaultWaitTime;
	[SerializeField] private float bridgeWaitTime;
	public float currentTime;
	public float currentMaxTime;
	public Action TimeEnd;


	public void SetTimerCountDown(bool withBridge)
	{
		StopAllCoroutines();
		currentTime = withBridge ? bridgeWaitTime : defaultWaitTime;
		currentMaxTime = currentTime;
		StartCoroutine(CountDownRoutine());
	}

	public IEnumerator CountDownRoutine()
	{
		while (currentTime > 0)
		{
			currentTime -= Time.deltaTime;
			timer.text = currentTime.ToString("0.00") + "s";
			timer.color = GetColorFromNumber(currentTime / currentMaxTime);
			yield return null;
		}

		currentTime = 0;
		timer.text = currentTime.ToString("0.00") + "s";
		timer.color = GetColorFromNumber(currentTime / currentMaxTime);
		TimeEnd?.Invoke();
	}

	public void StopInstantTimer()
	{
		StopAllCoroutines();
	}


	public Color GetColorFromNumber(float number)
	{
		Color returnColor = Color.HSVToRGB(number / 3f, 1, 1);
		return returnColor;
	}
}
