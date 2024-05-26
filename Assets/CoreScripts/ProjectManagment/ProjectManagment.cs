using UnityEngine;

public class ProjectManagment : MonoBehaviour
{
	[SerializeField] private bool loadExistingSaves;
	[SerializeField] private int shiners;
	[SerializeField] private int currentDegree;
	[SerializeField] private int startAdvance;
	[SerializeField] private int endAdvance;
	[SerializeField] private float musicPitch;
	[SerializeField] private float soundsPitch;
	[SerializeField] private int briefAllowed;
	public string shinersPath => nameof(shiners);
	public string currentDegreePath => nameof(currentDegree);
	public string startAdvancePath => nameof(startAdvance);
	public string endAdvancePath => nameof(endAdvance);
	public string musicPitchPath => nameof(musicPitch);
	public string soundsPitchPath => nameof(soundsPitch);
	public string briefAllowedPath => nameof(briefAllowed);
	public int Shiners
	{
		get => shiners;
		set
		{
			shiners = value;
		}
	}
	public int CurrentDegree
	{
		get => currentDegree;
		set
		{
			currentDegree = value;
		}
	}
	public int StartAdvance
	{
		get => startAdvance;
		set
		{
			startAdvance = value;
		}
	}
	public int EndAdvance
	{
		get => endAdvance;
		set
		{
			endAdvance = value;
		}
	}
	public float MusicPitch
	{
		get => musicPitch;
		set
		{
			musicPitch = value;
		}
	}
	public float SoundsPitch
	{
		get => soundsPitch;
		set
		{
			soundsPitch = value;
		}
	}
	public int BriefAllowed
	{
		get => briefAllowed;
		set
		{
			briefAllowed = value;
		}
	}

	public static ProjectManagment Manager { get; private set; }


	public void Awake()
	{
		if (Manager != null)
		{
			Destroy(gameObject);
			return;
		}
		else
		{
			Manager = this;
			DontDestroyOnLoad(this.gameObject);
		}

		if (loadExistingSaves)
		{
			LoadExistingSaves();
		}
		else
		{
			ManageSavings();
		}
	}

	public void LoadExistingSaves()
	{
		currentDegree = PlayerPrefs.GetInt(currentDegreePath, currentDegree);
		shiners = PlayerPrefs.GetInt(shinersPath, shiners);
		startAdvance = PlayerPrefs.GetInt(startAdvancePath, startAdvance);
		endAdvance = PlayerPrefs.GetInt(endAdvancePath, endAdvance);
		musicPitch = PlayerPrefs.GetFloat(musicPitchPath, musicPitch);
		soundsPitch = PlayerPrefs.GetFloat(soundsPitchPath, soundsPitch);
		briefAllowed = PlayerPrefs.GetInt(briefAllowedPath, briefAllowed);
	}

	public void ManageSavings()
	{
		PlayerPrefs.SetInt(shinersPath, shiners);
		PlayerPrefs.SetInt(currentDegreePath, currentDegree);
		PlayerPrefs.SetInt(startAdvancePath, startAdvance);
		PlayerPrefs.SetInt(endAdvancePath, endAdvance);
		PlayerPrefs.SetFloat(musicPitchPath, musicPitch);
		PlayerPrefs.SetFloat(soundsPitchPath, soundsPitch);
		PlayerPrefs.SetInt(briefAllowedPath, briefAllowed);

		PlayerPrefs.Save();
	}
}
