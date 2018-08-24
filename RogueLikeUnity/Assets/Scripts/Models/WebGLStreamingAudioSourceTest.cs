using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WebGLStreamingAudioSourceTest : MonoBehaviour
{
	public GameObject audioSourcePrefab;
	public GameObject audioSourceContentPanel;

	public GameObject sound3d1;
	public GameObject sound3d2;
	public GameObject sound3d3;

	List<WebGLStreamingAudioSourceInterop> m_audioList = new List<WebGLStreamingAudioSourceInterop>();
	
	void Start ()
	{
		//SpawnMozillaTest();
		//SpawnMoonlightSonata();
		//RefreshPanel();
	}

	//void RefreshPanel()
	//{
	//	for (int i = audioSourceContentPanel.transform.childCount - 1; i >= 0; --i)
	//	{
	//		GameObject.Destroy(audioSourceContentPanel.transform.GetChild(i).gameObject);
	//	}

	//	for (int i = 0; i < m_audioList.Count; ++i)
	//	{
	//		if (m_audioList[i].IsValid == false)
	//			continue;

	//		AudioPanelScript s = Instantiate<GameObject>(audioSourcePrefab).GetComponent<AudioPanelScript>();
	//		s.Init(m_audioList[i]);

	//		s.transform.SetParent(audioSourceContentPanel.transform);
	//	}

	//	var sources = FindObjectsOfType<WebGLStreamingAudioSource>();
	//	for (int i = 0; i < sources.Length; ++i)
	//	{
	//		AudioPanelScript s = Instantiate<GameObject>(audioSourcePrefab).GetComponent<AudioPanelScript>();
	//		s.Init(sources[i]);

	//		s.transform.SetParent(audioSourceContentPanel.transform);
	//	}	
	//}

	void SpawnAudio(WebGLStreamingAudioSourceInterop audio)
	{
		audio.invalidated += (sender) => {
			
			//RefreshPanel();
		};

		m_audioList.Add(audio);

		audio.Play ();

		//RefreshPanel();
	}

	//public void Spawn3dSound1()
	//{
	//	GameObject.Instantiate(sound3d1);
	//	RefreshPanel();
	//}

	//public void Spawn3dSound2()
	//{
	//	GameObject.Instantiate(sound3d2);
	//	RefreshPanel();
	//}

	//public void Spawn3dSound3()
	//{
	//	GameObject.Instantiate(sound3d3);
	//	RefreshPanel();
	//}

	public void SpawnMozillaTest()
	{
        SpawnAudio(new WebGLStreamingAudioSourceInterop("https://mdn.mozillademos.org/files/2587/AudioTest%20(1).ogg", this.gameObject, ""));
	}

	// Update is called once per frame
	void Update () {
		
	}
}
