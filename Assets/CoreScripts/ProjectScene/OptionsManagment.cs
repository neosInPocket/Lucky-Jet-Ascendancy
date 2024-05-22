using UnityEngine;
using UnityEngine.UI;

public class OptionsManagment : MonoBehaviour
{
	[SerializeField] public Image buttonSource;
	[SerializeField] public Sprite activeMusic;
	[SerializeField] public Sprite inactiveMusic;

	public void Start()
	{
		if (MusicManagment.MusicManager.Volume == 0)
		{
			buttonSource.sprite = inactiveMusic;
		}
		else
		{
			buttonSource.sprite = activeMusic;
		}
	}

	public void ReverseMusicPitch()
	{
		if (MusicManagment.MusicManager.Volume == 0)
		{
			MusicManagment.MusicManager.Volume = 1;
			buttonSource.sprite = activeMusic;
		}
		else
		{
			MusicManagment.MusicManager.Volume = 0;
			buttonSource.sprite = inactiveMusic;
		}
	}

	public void ReverseEnter()
	{
		Application.Quit();
	}
}
