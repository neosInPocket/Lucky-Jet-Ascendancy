using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class ProjectEvents : MonoBehaviour
{
	[SerializeField] private CrossBall crossBall;
	[SerializeField] private FinishProject finishProject;
	[SerializeField] private BriefingProject briefingProject;
	[SerializeField] private ProjectState projectState;
	[SerializeField] private OpenProject openProject;
	[SerializeField] private BurnFloor burnFloor;
	[HideInInspector] public List<IInitializableProject> projects;

	public void Start()
	{
		projects.Add(projectState);

		foreach (var project in projects)
		{
			project.Initialize();
		}

		crossBall.CrossBallDestroyed += CrossBallDestroyed;
		crossBall.EnableCrossBall();
		burnFloor.EnableFloor();
	}

	public void CrossBallDestroyed()
	{
		crossBall.CrossBallDestroyed -= CrossBallDestroyed;
		burnFloor.DisableFloor();
	}

	private void OnDestroy()
	{
		crossBall.CrossBallDestroyed -= CrossBallDestroyed;
	}
}
