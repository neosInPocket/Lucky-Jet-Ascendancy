using UnityEngine;
using UnityEngine.SceneManagement;

public class ProjectsAnimator : MonoBehaviour
{
	public void InitializeNextProject()
	{
		SceneManager.LoadScene("InjectScene");
	}
}
