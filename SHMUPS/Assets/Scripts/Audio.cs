using UnityEngine;
using System.Collections;

public class Audio : MonoBehaviour {

	public AudioSource Bgm;

	public AudioClip[] BackgroundMusic;

	private static void Play(AudioSource source, AudioClip[] clips)
	{
		source.PlayOneShot (clips [Random.Range (0, clips.Length)]);
	}

	void Update()
	{
		if(!GetComponent<AudioSource>().isPlaying)
		{
			GetComponent<AudioSource>().clip = BackgroundMusic[Random.Range(0, BackgroundMusic.Length)];
			GetComponent<AudioSource>().Play();
		}
	}
}
