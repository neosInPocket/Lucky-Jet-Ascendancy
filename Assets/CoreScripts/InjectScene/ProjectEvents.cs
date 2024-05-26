using System.Collections.Generic;
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
	[SerializeField] public InstantTimer instantTimer;

	public void Start()
	{
		projects.Add(projectState);

		foreach (var project in projects)
		{
			project.Initialize();
		}

		if (ProjectManagment.Manager.BriefAllowed != 1)
		{
			Briefed();
		}
		else
		{
			briefingProject.SetBrief(Briefed);
			ProjectManagment.Manager.BriefAllowed = 0;
			ProjectManagment.Manager.ManageSavings();
		}
	}

	public void Briefed()
	{
		openProject.ToggleOpenProject(true, ProjectOpened);
	}

	public void ToggleOpenProjectDisabled()
	{
		openProject.ToggleOpenProject(false);
	}

	public void ProjectOpened()
	{
		crossBall.PathPointHit += PathPointHit;
		crossBall.CrossBallDestroyed += CrossBallDestroyed;
		instantTimer.TimeEnd += TimeEnd;
		projectState.DegreeCompleted += DegreeCompleted;
		crossBall.EnableCrossBall();
		burnFloor.EnableFloor();
		instantTimer.StopInstantTimer();
	}

	public void PathPointHit()
	{
		projectState.IncrementProgression();
	}

	public void DegreeCompleted(int shinersReward)
	{
		crossBall.PathPointHit -= PathPointHit;
		crossBall.CrossBallDestroyed -= CrossBallDestroyed;
		instantTimer.TimeEnd -= TimeEnd;
		projectState.DegreeCompleted -= DegreeCompleted;
		crossBall.DisableCrossBall();
		burnFloor.DisableFloor();
		instantTimer.StopInstantTimer();

		finishProject.FinishWithSuccess(shinersReward, ProjectManagment.Manager.CurrentDegree);
	}

	public void CrossBallDestroyed()
	{
		crossBall.PathPointHit -= PathPointHit;
		crossBall.CrossBallDestroyed -= CrossBallDestroyed;
		instantTimer.TimeEnd -= TimeEnd;
		projectState.DegreeCompleted -= DegreeCompleted;
		crossBall.DisableCrossBall();
		burnFloor.DisableFloor();
		instantTimer.StopInstantTimer();

		finishProject.FinishWithLose(ProjectManagment.Manager.CurrentDegree);
	}

	public void TimeEnd()
	{
		crossBall.PathPointHit -= PathPointHit;
		crossBall.CrossBallDestroyed -= CrossBallDestroyed;
		instantTimer.TimeEnd -= TimeEnd;
		projectState.DegreeCompleted -= DegreeCompleted;
		crossBall.DisableCrossBall();
		burnFloor.DisableFloor();
		instantTimer.StopInstantTimer();

		finishProject.FinishWithLose(ProjectManagment.Manager.CurrentDegree);
		crossBall.DestroyCross();
	}

	private void OnDestroy()
	{
		crossBall.PathPointHit -= PathPointHit;
		crossBall.CrossBallDestroyed -= CrossBallDestroyed;
		instantTimer.TimeEnd -= TimeEnd;
		projectState.DegreeCompleted -= DegreeCompleted;
	}
}
