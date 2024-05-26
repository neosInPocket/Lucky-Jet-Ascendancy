using System;
using UnityEngine;

public class OpenProject : MonoBehaviour
{
	public Action ProjectOpened;

	public void ToggleOpenProject(bool toggleValue, Action projectOpened = null)
	{
		if (toggleValue)
		{
			ProjectOpened = projectOpened;
			gameObject.SetActive(true);
		}
		else
		{
			if (ProjectOpened != null)
			{
				ProjectOpened();
				gameObject.SetActive(false);
			}
			else
			{
				throw new NullReferenceException("Null reference on project opened action");
			}
		}
	}
}
