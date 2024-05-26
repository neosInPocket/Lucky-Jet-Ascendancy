using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProjectState : IInitializableProject
{
	[SerializeField] Image fillProject;
	[SerializeField] TMP_Text textState;
	[SerializeField] TMP_Text degreeState;
	[SerializeField] TMP_Text shinersState;
	public Action<int> DegreeCompleted { get; set; }
	public float FloatProgression => (float)progression / (float)goal;
	private int currentDegree => ProjectManagment.Manager.CurrentDegree;
	private int shiners;
	protected int progression;
	protected int goal;

	public override void Initialize()
	{
		StatesMath();
		degreeState.text = $"level {currentDegree} reward:";
		shinersState.text = shiners.ToString();
		RestateProject();
	}

	public void IncrementProgression()
	{
		progression++;

		if (progression > goal)
		{
			DegreeCompleted?.Invoke(shiners);
		}

		RestateProject();
	}

	public void StatesMath()
	{
		var xDegree = ProjectManagment.Manager.CurrentDegree;
		goal = (int)(3f * Mathf.Log((xDegree + 1f) * (xDegree + 1f)) - 1);
		shiners = (int)(5f * Mathf.Log((xDegree + 1f) * (xDegree + 1f)) - 1);
	}

	public void RestateProject()
	{
		textState.text = $"{progression}/{goal}";
		fillProject.fillAmount = FloatProgression;
	}
}
