using TMPro;
using UnityEngine;

public class CoinsResolver : MonoBehaviour
{
	[SerializeField] public TMP_Text resolver1;
	[SerializeField] public TMP_Text resolver2;
	[SerializeField] public TMP_Text degree;
	[SerializeField] public TMP_Text degreeButtonText;

	public void Start()
	{
		resolver1.text = ProjectManagment.Manager.Shiners.ToString();
		resolver2.text = ProjectManagment.Manager.Shiners.ToString();
		degree.text = "level " + ProjectManagment.Manager.CurrentDegree.ToString();
		degreeButtonText.text = degree.text;
	}

	public void ResolveCoins()
	{
		resolver2.text = ProjectManagment.Manager.Shiners.ToString();
		degree.text = "level " + ProjectManagment.Manager.CurrentDegree.ToString();
		degreeButtonText.text = degree.text;
	}
}
