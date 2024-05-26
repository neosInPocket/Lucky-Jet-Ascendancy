using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoreProject : MonoBehaviour
{
	[Header("First advance")]
	[SerializeField] private TMP_Text progression1;
	[SerializeField] private TMP_Text worthText1;
	[SerializeField] private Image progressionFilling1;
	[SerializeField] private Button button1;
	[SerializeField] private GameObject buttonMainInner1;
	[SerializeField] private GameObject afterButton1;
	[SerializeField] private int worth1;
	[Header("Second advance")]
	[SerializeField] private TMP_Text progression2;
	[SerializeField] private TMP_Text worthText2;
	[SerializeField] private Image progressionFilling2;
	[SerializeField] private Button button2;
	[SerializeField] private GameObject buttonMainInner2;
	[SerializeField] private GameObject afterButton2;
	[SerializeField] private int worth2;
	[Header("Misc")]
	[SerializeField] private CoinsResolver resolver;

	private void Start()
	{
		ManageStore();
	}

	public void ManageStore()
	{
		if (ProjectManagment.Manager.Shiners >= worth1)
		{
			buttonMainInner1.SetActive(true);
			afterButton1.SetActive(false);

			if (ProjectManagment.Manager.StartAdvance < 5)
			{
				button1.interactable = true;
			}
			else
			{
				button1.interactable = false;
			}
		}
		else
		{
			button1.interactable = false;
			buttonMainInner1.SetActive(true);
			afterButton1.SetActive(false);

			if (ProjectManagment.Manager.StartAdvance < 5)
			{
				buttonMainInner1.SetActive(false);
				afterButton1.SetActive(true);
			}
			else
			{
				buttonMainInner1.SetActive(true);
				afterButton1.SetActive(false);
			}
		}

		if (ProjectManagment.Manager.Shiners >= worth2)
		{
			buttonMainInner2.SetActive(true);
			afterButton2.SetActive(false);

			if (ProjectManagment.Manager.EndAdvance < 1)
			{
				button2.interactable = true;
			}
			else
			{
				button2.interactable = false;
			}
		}
		else
		{
			button2.interactable = false;
			buttonMainInner2.SetActive(true);
			afterButton2.SetActive(false);

			if (ProjectManagment.Manager.EndAdvance < 5)
			{
				buttonMainInner2.SetActive(false);
				afterButton2.SetActive(true);
			}
			else
			{
				buttonMainInner2.SetActive(true);
				afterButton2.SetActive(false);
			}
		}

		progression1.text = ProjectManagment.Manager.StartAdvance.ToString() + "/" + "5";
		progression2.text = ProjectManagment.Manager.EndAdvance.ToString() + "/" + "1";
		progressionFilling1.fillAmount = (float)ProjectManagment.Manager.StartAdvance / 5f;
		progressionFilling2.fillAmount = (float)ProjectManagment.Manager.EndAdvance / 1f;
		worthText1.text = worth1.ToString();
		worthText2.text = worth2.ToString();
		resolver.ResolveCoins();
	}

	public void GetFromFirst()
	{
		ProjectManagment.Manager.Shiners -= worth1;
		ProjectManagment.Manager.StartAdvance += 1;
		ProjectManagment.Manager.ManageSavings();

		ManageStore();
	}

	public void GetFromSecond()
	{
		ProjectManagment.Manager.Shiners -= worth2;
		ProjectManagment.Manager.EndAdvance += 1;
		ProjectManagment.Manager.ManageSavings();

		ManageStore();
	}
}
