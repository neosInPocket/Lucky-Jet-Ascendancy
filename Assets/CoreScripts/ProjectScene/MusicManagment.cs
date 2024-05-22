using UnityEngine;

public class MusicManagment : MonoBehaviour
{
	[SerializeField] public AudioSource audioSource;
	public static MusicManagment MusicManager { get; private set; }

	public float Volume
	{
		get => audioSource.volume;
		set => audioSource.volume = value;
	}

	public void Awake()
	{
		if (MusicManager != null)
		{
			Destroy(this.gameObject);
		}
		else
		{
			MusicManager = this;
			DontDestroyOnLoad(this.gameObject);
		}
	}

	public void Start()
	{
		audioSource.volume = ProjectManagment.Manager.MusicPitch;
	}
}
