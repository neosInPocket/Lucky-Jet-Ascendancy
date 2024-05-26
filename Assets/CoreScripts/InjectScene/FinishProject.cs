using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinishProject : MonoBehaviour
{
	[SerializeField] public TMP_Text shiners;
	[SerializeField] public TMP_Text degreeCompleted;
	[SerializeField] public TMP_Text finishControlText;
	[SerializeField] public string winStringState;
	[SerializeField] public Button winStateControl;
	[SerializeField] public Button loseStateControl;

	public void Start()
	{
		winStateControl.onClick.AddListener(() => SceneManager.LoadScene("InjectScene"));
		loseStateControl.onClick.AddListener(() => SceneManager.LoadScene("ProjectScene"));
	}

	public void FinishWithSuccess(int shiners, int degree)
	{
		gameObject.SetActive(true);
		this.shiners.text = shiners.ToString();
		degreeCompleted.text = "level " + degree.ToString() + " completed!";
		finishControlText.text = "level " + (degree + 1).ToString();

		ProjectManagment.Manager.CurrentDegree++;
		ProjectManagment.Manager.Shiners += shiners;
		ProjectManagment.Manager.ManageSavings();
	}

	public void FinishWithLose(int degree)
	{
		gameObject.SetActive(true);
		this.shiners.text = 0.ToString();
		degreeCompleted.text = "level " + degree.ToString() + " failed";
		finishControlText.text = "retry";
	}
}
