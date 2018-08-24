﻿//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using UnityEngine;
//using UnityEngine.Audio;
//using UnityEngine.Networking;

//public class MusicInformation : MonoBehaviour
//{

//    public AudioMixer fantasy_mixer;
//    private float bpm;
//    private int beatsPerMeasure = 4;
//    private double singleMeasureTime;
//    private double delayEvent;
//    private bool running = false;
//    private int i;
//    private int y;
//    private int j;
//    private int k;
//    private bool reset = false;
//    private bool first_run = false;
//    private double time;
//    public float reset_time;
//    public float reset_timer;
//    public bool stop_music;
//    public bool use_triggers;
//    private bool in_time;
//    private bool switch_audio_source = false;

//    public Transform enemy_1;
//    public Transform enemy_2;
//    public Transform enemy_3;
//    public Transform enemy_4;
//    public Transform enemy_5;
//    public Transform Player;

//    public bool adventure;
//    public bool heroic;
//    public bool ambiant_light_forest;
//    public bool play_transition;
//    public bool background;

//    private float enemy1;
//    private float enemy2;
//    private float enemy3;
//    private float enemy4;
//    private float enemy5;

//    private float soft_mood;
//    private float med_mood;
//    private float forte_mood;
//    private float soft_distance = 5000;
//    private float med_distance = 5000;
//    private float forte_distance = 5000;
//    public float nearest_enemy;

//    public int trigger_med;
//    public int trigger_forte;

//    private bool soft_trigger = false;
//    private bool med_trigger = false;
//    private bool forte_trigger = false;

//    private int random_theme;
//    private int random_ambiant;


//    public float fadeout_speed = 15.0f;
//    public float fadein_speed = 25.0f;

//    private AudioSource audio_soft1;
//    private AudioSource audio_med1;
//    private AudioSource audio_forte1;
//    private AudioSource audio_soft2;
//    private AudioSource audio_med2;
//    private AudioSource audio_forte2;
//    private AudioSource audio_end;
//    private AudioSource audio_ambiant1;
//    private AudioSource audio_ambiant2;
//    private AudioSource audio_background;




//    private float audio_end_vol = 0.0f;
//    private float audio_soft_vol = -80.0f;
//    private float audio_med_vol = -80.0f;
//    private float audio_forte_vol = -80.0f;
//    private float audio_background_vol = -25.0f;
//    private float audio_ambiant_vol = -10.0f;

//    public bool soft;
//    public bool med;
//    public bool forte;

//    public bool soft_isPlaying;
//    public bool med_isPlaying;
//    public bool forte_isPlaying;
//    public bool ambiant_isPlaying;

//    private int set_bpm;

//    public bool start = false;

//    private AudioSource audio_demo;

//    private static Object[] AudioArray_soft_adventure;
//    private static Object[] AudioArray_med_adventure;
//    private static Object[] AudioArray_forte_adventure;
//    private static Object[] AudioArray_end_soft_adventure;
//    private static Object[] AudioArray_end_med_adventure;
//    private static Object[] AudioArray_end_forte_adventure;


//    private static Object[] AudioArray_soft_heroic;
//    private static Object[] AudioArray_med_heroic;
//    private static Object[] AudioArray_forte_heroic;
//    private static Object[] AudioArray_end_soft_heroic;
//    private static Object[] AudioArray_end_med_heroic;
//    private static Object[] AudioArray_end_forte_heroic;

//    private static Object[] AudioArray_ambiant_light_forest;

//    private bool IsAttach;
//    public bool IsMusicOn
//    {
//        get { return !stop_music; }
//        set
//        {
//            stop_music = !value;
//        }
//    }
//    public static bool IsSetup
//    {
//        get;private set;
//    }
//    private static MusicInformation _musicInformation;
//    public static MusicInformation Music
//    {
//        get
//        {
//            if (CommonFunction.IsNull(_musicInformation) == false)
//            {
//                GameObject gm = GameObject.Find("MusicManager");
//                if (CommonFunction.IsNull(gm) == true)
//                {
//                    GameObject m2 = new GameObject("MusicManager");
                    
//                    _musicInformation = m2.AddComponent<MusicInformation>();
//                }
//                return _musicInformation;
//            }
//            GameObject m = new GameObject("MusicManager");
//            _musicInformation = m.AddComponent<MusicInformation>();
//            _musicInformation.IsMusicOn = false;
//            IsSetup = false;
//            return _musicInformation;
//        }
//    }

//    public MusicInformation()
//    {
//        IsAttach = false;
//    }
//    void Start()
//    {
//        SetupAttach();
//        //while (!Caching.ready)
//        //    yield return null;
//        //// Start a download of the given URL
//        //WWW www = WWW.LoadFromCacheOrDownload(@"file://C:\Unity\RogueLikeUnity3\AssetBundles\WebGL\musics.sd", 1);

//        //// Wait for download to complete
//        //yield return www;

//        //// Load and retrieve the AssetBundle
//        //AssetBundle bundle = www.assetBundle;

//        //// Load the object asynchronously
//        //AssetBundleRequest request = bundle.LoadAssetAsync("myObject", typeof(GameObject));

//        //// Wait for completion
//        //yield return request;

//        //// Get the reference to the loaded object
//        //GameObject obj = request.asset as GameObject;

//        //// Unload the AssetBundles compressed contents to conserve memory
//        //bundle.Unload(false);

//        //// Frees the memory from the web stream
//        //www.Dispose();
//    }

//    public void Setup()
//    {
//        if (IsSetup == false)
//        {
//            StartCoroutine("SetupData");
//        }

//    }

//    private void SetupAttach()
//    {
//        adventure = true;
//        use_triggers = false;
//        reset_timer = 5.0f;
//        first_run = false;
//        beatsPerMeasure = 4;
//        reset_time = -0.1f;
//        i = 0;
//        k = 0;
//        bpm = set_bpm;

//        audio_background = (AudioSource)gameObject.AddComponent<AudioSource>();
//        audio_soft1 = (AudioSource)gameObject.AddComponent<AudioSource>();
//        audio_med1 = (AudioSource)gameObject.AddComponent<AudioSource>();
//        audio_forte1 = (AudioSource)gameObject.AddComponent<AudioSource>();
//        audio_end = (AudioSource)gameObject.AddComponent<AudioSource>();
//        audio_ambiant1 = (AudioSource)gameObject.AddComponent<AudioSource>();
//        audio_ambiant2 = (AudioSource)gameObject.AddComponent<AudioSource>();
//        audio_soft2 = (AudioSource)gameObject.AddComponent<AudioSource>();
//        audio_med2 = (AudioSource)gameObject.AddComponent<AudioSource>();
//        audio_forte2 = (AudioSource)gameObject.AddComponent<AudioSource>();

//    }
//    private IEnumerator SetupData()
//    {

//        string UrlParent = "";
//#if UNITY_EDITOR
//        UrlParent = @"file://C:\Unity\Music\Resources\{0}\{1}";
//#else
//        UrlParent = "";
//#endif


//        #region adventure/soft 
//        List<AudioClip> list = new List<AudioClip>();
//        string midPath = @"adventure\soft";
//        //
//        yield return GetAudioClip(string.Format(UrlParent, midPath, "1_adventure_soft.ogg"),
//            list);
//        yield return GetAudioClip(string.Format(UrlParent, midPath, "2_adventure_soft.ogg"),
//            list);
//        yield return GetAudioClip(string.Format(UrlParent, midPath, "5_adventure_soft.ogg"),
//            list);
//        yield return GetAudioClip(string.Format(UrlParent, midPath, "7_adventure_soft.ogg"),
//            list);

//        AudioArray_soft_adventure = list.ToArray();

//        #endregion adventure/soft

//        #region adventure/med
//        ////AudioArray_med_adventure = Resources.LoadAll("adventure/med", typeof(AudioClip));
//        list.Clear();
//        midPath = @"adventure\med";

//        yield return GetAudioClip(string.Format(UrlParent, midPath, "1_adventure_med.ogg"),
//            list);
//        yield return GetAudioClip(string.Format(UrlParent, midPath, "2_adventure_med.ogg"),
//            list);
//        yield return GetAudioClip(string.Format(UrlParent, midPath, "5_adventure_med.ogg"),
//            list);
//        yield return GetAudioClip(string.Format(UrlParent, midPath, "7_adventure_med.ogg"),
//            list);

//        AudioArray_med_adventure = list.ToArray();

//        #endregion adventure/med

//        #region adventure/forte
//        //////AudioArray_forte_adventure = Resources.LoadAll("adventure/forte", typeof(AudioClip));
//        list.Clear();
//        midPath = @"adventure\forte";

//        yield return GetAudioClip(string.Format(UrlParent, midPath, "1_adventure_forte.ogg"),
//            list);
//        yield return GetAudioClip(string.Format(UrlParent, midPath, "1_adventure_forte.ogg"),
//            list);
//        yield return GetAudioClip(string.Format(UrlParent, midPath, "1_adventure_forte.ogg"),
//            list);
//        yield return GetAudioClip(string.Format(UrlParent, midPath, "1_adventure_forte.ogg"),
//            list);

//        AudioArray_forte_adventure = list.ToArray();

//        #endregion adventure/forte

//        #region adventure/soft_end
//        //////AudioArray_end_soft_adventure = Resources.LoadAll("adventure/soft_end", typeof(AudioClip));
//        list.Clear();
//        midPath = @"adventure\soft_end";

//        yield return GetAudioClip(string.Format(UrlParent, midPath, "1_adventure_start.ogg"),
//            list);

//        AudioArray_end_soft_adventure = list.ToArray();
//        #endregion adventure/soft_end

//        #region adventure/med_end
//        //////AudioArray_end_med_adventure = Resources.LoadAll("adventure/med_end", typeof(AudioClip));
//        list.Clear();
//        midPath = @"adventure\med_end";

//        yield return GetAudioClip(string.Format(UrlParent, midPath, "2_adventure_start.ogg"),
//            list);

//        AudioArray_end_med_adventure = list.ToArray();
//        #endregion adventure/med_end

//        #region adventure/forte_end
//        //////AudioArray_end_forte_adventure = Resources.LoadAll("adventure/forte_end", typeof(AudioClip));
//        list.Clear();
//        midPath = @"adventure\forte_end";

//        yield return GetAudioClip(string.Format(UrlParent, midPath, "3_adventure_start.ogg"),
//            list);

//        AudioArray_end_forte_adventure = list.ToArray();
//        #endregion adventure/forte_end

//        #region heroic/soft
//        //////AudioArray_soft_heroic = Resources.LoadAll("heroic/soft", typeof(AudioClip));
//        list.Clear();
//        midPath = @"heroic\soft";
//        //
//        yield return GetAudioClip(string.Format(UrlParent, midPath, "1_heroic_soft.ogg"),
//            list);
//        yield return GetAudioClip(string.Format(UrlParent, midPath, "2_heroic_soft.ogg"),
//            list);
//        yield return GetAudioClip(string.Format(UrlParent, midPath, "4_heroic_soft.ogg"),
//            list);
//        yield return GetAudioClip(string.Format(UrlParent, midPath, "7_heroic_soft.ogg"),
//            list);

//        AudioArray_soft_heroic = list.ToArray();
//        #endregion heroic/soft

//        #region heroic/med
//        //////AudioArray_med_heroic = Resources.LoadAll("heroic/med", typeof(AudioClip));
//        list.Clear();
//        midPath = @"heroic\med";
//        //
//        yield return GetAudioClip(string.Format(UrlParent, midPath, "1_heroic_med.ogg"),
//            list);
//        yield return GetAudioClip(string.Format(UrlParent, midPath, "2_heroic_med.ogg"),
//            list);
//        yield return GetAudioClip(string.Format(UrlParent, midPath, "4_heroic_med.ogg"),
//            list);
//        yield return GetAudioClip(string.Format(UrlParent, midPath, "7_heroic_med.ogg"),
//            list);

//        AudioArray_med_heroic = list.ToArray();
//        #endregion heroic/med

//        #region heroic/forte
//        //////AudioArray_forte_heroic = Resources.LoadAll("heroic/forte", typeof(AudioClip));
//        list.Clear();
//        midPath = @"heroic\forte";
//        //
//        yield return GetAudioClip(string.Format(UrlParent, midPath, "1_heroic_forte.ogg"),
//            list);
//        yield return GetAudioClip(string.Format(UrlParent, midPath, "2_heroic_forte.ogg"),
//            list);
//        yield return GetAudioClip(string.Format(UrlParent, midPath, "4_heroic_forte.ogg"),
//            list);
//        yield return GetAudioClip(string.Format(UrlParent, midPath, "7_heroic_forte.ogg"),
//            list);

//        AudioArray_forte_heroic = list.ToArray();
//        #endregion heroic/forte

//        #region heroic/soft_end
//        //////AudioArray_end_soft_heroic = Resources.LoadAll("heroic/soft_end", typeof(AudioClip));
//        list.Clear();
//        midPath = @"heroic\soft_end";
//        //
//        yield return GetAudioClip(string.Format(UrlParent, midPath, "1_heroic_start.ogg"),
//            list);

//        AudioArray_end_soft_heroic = list.ToArray();
//        #endregion heroic/soft_end

//        #region heroic/med_end
//        //////AudioArray_end_med_heroic = Resources.LoadAll("heroic/med_end", typeof(AudioClip));
//        list.Clear();
//        midPath = @"heroic\med_end";
//        //
//        yield return GetAudioClip(string.Format(UrlParent, midPath, "2_heroic_start.ogg"),
//            list);

//        AudioArray_end_med_heroic = list.ToArray();
//        #endregion heroic/med_end

//        #region heroic/forte_end
//        //////AudioArray_end_forte_heroic = Resources.LoadAll("heroic/forte_end", typeof(AudioClip));
//        list.Clear();
//        midPath = @"heroic\forte_end";
//        //
//        yield return GetAudioClip(string.Format(UrlParent, midPath, "3_heroic_start.ogg"),
//            list);

//        AudioArray_end_forte_heroic = list.ToArray();
//        #endregion heroic/forte_end

//        #region ambiant/light_forest
//        //////AudioArray_ambiant_light_forest = Resources.LoadAll("ambiant/light_forest", typeof(AudioClip));
//        list.Clear();
//        midPath = @"ambiant\light_forest";
//        //
//        yield return GetAudioClip(string.Format(UrlParent, midPath, "1_ambiant.ogg"),
//            list);
//        yield return GetAudioClip(string.Format(UrlParent, midPath, "2_ambiant.ogg"),
//            list);
//        yield return GetAudioClip(string.Format(UrlParent, midPath, "6_ambiant.ogg"),
//            list);
//        yield return GetAudioClip(string.Format(UrlParent, midPath, "7_ambiant.ogg"),
//            list);

//        AudioArray_ambiant_light_forest = list.ToArray();
//        #endregion ambiant/light_forest


//        RandomStructure();

//        SetTempo();

//        IsSetup = true;
//    }

//    public void SetupAttachAft()
//    {
//        audio_ambiant1.outputAudioMixerGroup = fantasy_mixer.FindMatchingGroups("Ambiant")[0];
//        audio_ambiant2.outputAudioMixerGroup = fantasy_mixer.FindMatchingGroups("Ambiant")[0];
//        audio_background.outputAudioMixerGroup = fantasy_mixer.FindMatchingGroups("Background")[0];
//        audio_end.outputAudioMixerGroup = fantasy_mixer.FindMatchingGroups("End")[0];
//        audio_soft1.outputAudioMixerGroup = fantasy_mixer.FindMatchingGroups("Soft")[0];
//        audio_med1.outputAudioMixerGroup = fantasy_mixer.FindMatchingGroups("Med")[0];
//        audio_forte1.outputAudioMixerGroup = fantasy_mixer.FindMatchingGroups("Forte")[0];
//        audio_soft2.outputAudioMixerGroup = fantasy_mixer.FindMatchingGroups("Soft")[0];
//        audio_med2.outputAudioMixerGroup = fantasy_mixer.FindMatchingGroups("Med")[0];
//        audio_forte2.outputAudioMixerGroup = fantasy_mixer.FindMatchingGroups("Forte")[0];

//        IsAttach = true;
//    }

//    public enum MusicType
//    {
//        AdventureSoft,
//        AdventureMed,
//        AdventureForte,
//        HeroicSoft,
//        HeroicMed,
//        HeroicForte,
//        LightForest
//    }
//    public void SetMusicType(MusicType t)
//    {
//        switch(t)
//        {
//            case MusicType.AdventureSoft:
//                Adventure_onClick();
//                Soft_onClick();
//                break;
//            case MusicType.AdventureMed:
//                Adventure_onClick();
//                Med_onClick();
//                break;
//            case MusicType.AdventureForte:
//                Adventure_onClick();
//                Forte_onClick();
//                break;
//            case MusicType.HeroicSoft:
//                Heroic_onClick();
//                Soft_onClick();
//                break;
//            case MusicType.HeroicMed:
//                Heroic_onClick();
//                Med_onClick();
//                break;
//            case MusicType.HeroicForte:
//                Heroic_onClick();
//                Forte_onClick();
//                break;
//            case MusicType.LightForest:
//                Light_Forest_onClick();
//                break;
//        }
//    }

//    public IEnumerator GetAudioClip(string url, List<AudioClip> list)
//    {
//        WWW www = new WWW(url);

//        while (www.isDone == false && www.progress != 1)
//        {
//            yield return null;
//        }

//        AudioClip clip = www.GetAudioClip(false, true, AudioType.OGGVORBIS);
        
//        list.Add(clip);

//        www.Dispose();

//    }

//    // Update is called once per frame
//    void Update()
//    {
//        //if(IsMusicOn == false)
//        //{
//        //    return;
//        //}
//        if (IsSetup == false)
//        {
//            return;
//        }

//        if (IsAttach == false)
//        {
//            SetupAttachAft();
//        }
//        SetVolumes();

//        if (!stop_music & use_triggers)
//        {
//            CheckDistanceToTrigger();
//            CheckMood();
//        }
//        if (stop_music)
//        {
//            soft = false;
//            med = false;
//            forte = false;
//            StopAll();
//        }


//        if (y == 1 | y == 5 | y == 9 | y == 13)
//        {
//            if (!soft & !med & !forte)
//            {
//                StopAll();
//            }
//        }

//        if (start)
//        {
//            if (!first_run)
//            {
//                RandomStructure();
//                SetTempo();
//                singleMeasureTime = AudioSettings.dspTime + 2.0F;
//                running = true;
//                i = 0;
//                y = 0;
//            }
//            first_run = true;
//            Counter();
//        }

//    }


//    public void Counter()
//    {
//        //Debug.Log ("Counter was executed");

//        if (!running)
//            return;
//        time = AudioSettings.dspTime;


//        if (time + 1.0F > singleMeasureTime)
//        {

//        }
//        if (in_time)
//        {
//            if (y == 1 | y == 5 | y == 9)
//            {
//                Fantasy_Play();
//            }

//        }


//        //THE most important part of this script: this is the metronome, keeping count of the measures and making sure the audio is in sync
//        if (time + 1.0F > singleMeasureTime & start)
//        {
//            i += 1;
//            y += 1;
//            j += 1;
//            k += 1;

//            if (y == 9)
//            {
//                SetTempo();
//            }
//            singleMeasureTime += 60.0F / bpm * beatsPerMeasure;
//            in_time = true;
//            //Debug.Log("The y int equals  " + y + " " + bpm);
//        }
//        else
//        {
//            in_time = false;
//        }

//    }

//    public void Fantasy_Play()
//    {

//        if (y == 9 & !ambiant_light_forest & play_transition)
//        {
//            PlayTransition();
//            SetTempo();
//        }

//        if (y == 9)
//        {
//            y = 1;
//        }



//        if (y == 5 & !ambiant_light_forest & !ambiant_isPlaying & play_transition)
//        {
//            play_transition = false;
//            y = 1;
//        }

//        if (y == 1 & !play_transition)
//        {
//            switch_audio_source = !switch_audio_source;
//            RandomStructure();
//            SetTempo();
//            SetMood();

//            if (!ambiant_light_forest)
//            {
//                if (switch_audio_source)
//                {
//                    audio_soft1.PlayOneShot(audio_soft1.clip, 1.0f);
//                    audio_med1.PlayOneShot(audio_med1.clip, 1.0f);
//                    audio_forte1.PlayOneShot(audio_forte1.clip, 1.0f);
//                }
//                if (!switch_audio_source)
//                {
//                    audio_soft2.PlayOneShot(audio_soft2.clip, 1.0f);
//                    audio_med2.PlayOneShot(audio_med2.clip, 1.0f);
//                    audio_forte2.PlayOneShot(audio_forte2.clip, 1.0f);
//                }

//            }

//            if (ambiant_light_forest)
//            {
//                ambiant_isPlaying = true;
//                if (switch_audio_source)
//                {
//                    audio_ambiant1.PlayOneShot(audio_ambiant1.clip, 1.0f);
//                }
//                if (!switch_audio_source)
//                {
//                    audio_ambiant2.PlayOneShot(audio_ambiant2.clip, 1.0f);
//                }
//            }


//        }


//    }






//    public void Stop_onClick()
//    {
//        reset = true;
//        stop_music = true;

//    }

//    public void Soft_onClick()
//    {
//        start = true;
//        soft = true;
//        med = false;
//        forte = false;

//    }

//    public void Med_onClick()
//    {
//        start = true;
//        med = true;
//        soft = false;
//        forte = false;
//    }

//    public void Forte_onClick()
//    {
//        start = true;
//        forte = true;
//        med = false;
//        soft = false;

//    }

//    public void Adventure_onClick()
//    {
//        if (ambiant_isPlaying)
//        {
//            play_transition = true;
//        }
//        adventure = true;
//        heroic = false;
//        ambiant_light_forest = false;

//    }

//    public void Heroic_onClick()
//    {
//        if (ambiant_isPlaying)
//        {
//            play_transition = true;
//        }
//        adventure = false;
//        heroic = true;
//        ambiant_light_forest = false;

//    }


//    public void Light_Forest_onClick()
//    {
//        start = true;
//        soft = true;
//        med = false;
//        forte = false;
//        adventure = false;
//        heroic = false;
//        ambiant_light_forest = true;
//        play_transition = false;
//    }
    
//    public void RandomStructure()
//    {

//        random_theme = Random.Range(0, AudioArray_soft_adventure.Length);
//        random_ambiant = Random.Range(0, AudioArray_ambiant_light_forest.Length);


//    }

//    public void StopAll()
//    {

//        if (!soft & !med & !forte & !soft_isPlaying & !med_isPlaying & !forte_isPlaying)
//        {
//            i = 0;
//            y = 0;
//            k = 0;
//            j = 0;
//            singleMeasureTime = 0;
//            time = 0;
//            start = false;
//            running = false;
//            first_run = false;
//        }
//    }




//    public void SetVolumes()
//    {
//        fantasy_mixer.SetFloat("End", audio_end_vol);
//        fantasy_mixer.SetFloat("Soft", audio_soft_vol);
//        fantasy_mixer.SetFloat("Med", audio_med_vol);
//        fantasy_mixer.SetFloat("Forte", audio_forte_vol);
//        fantasy_mixer.SetFloat("Background", audio_background_vol);
//        fantasy_mixer.SetFloat("Ambiant", audio_ambiant_vol);




//        if (audio_soft_vol > -1.0f | audio_med_vol > -1.0f | audio_forte_vol > -1.0f)
//        {
//            fadein_speed = 60.0f;
//        }
//        if (soft & audio_soft_vol > -10.0f)
//        {
//            fadein_speed = 8.0f;
//        }
//        if (med & audio_med_vol > -10.0f)
//        {
//            fadein_speed = 8.0f;
//        }
//        if (forte & audio_forte_vol > -10.0f)
//        {
//            fadein_speed = 8.0f;
//        }

//        if (ambiant_light_forest)
//        {
//            if (audio_ambiant_vol < -10.0f)
//            {
//                audio_ambiant_vol += fadein_speed * Time.deltaTime;
//            }
//        }

//        if (!ambiant_light_forest)
//        {
//            if (audio_ambiant_vol > -80.0f)
//            {
//                audio_ambiant_vol -= fadeout_speed * Time.deltaTime;
//            }
//            if (audio_ambiant_vol < -30.0f)
//            {
//                if (play_transition & ambiant_isPlaying)
//                {
//                    y = 8;
//                    SetTempo();
//                    Counter();
//                    ambiant_isPlaying = false;

//                }

//            }
//        }

//        if (background)
//        {
//            if (audio_background_vol < -25.0f)
//            {
//                audio_background_vol += fadein_speed * Time.deltaTime;
//            }
//        }
//        if (!background)
//        {
//            if (audio_background_vol > -80.0f)
//            {
//                audio_background_vol -= fadeout_speed * Time.deltaTime;
//            }
//            if (audio_background_vol < -70.0f)
//            {
//                audio_background.Stop();
//            }
//        }

//        if (soft)
//        {
//            if (audio_soft_vol < 0.0f)
//            {
//                audio_soft_vol += fadein_speed * Time.deltaTime;
//            }
//            if (audio_med_vol > -80.0f & audio_soft_vol > -5.0f)
//            {
//                audio_med_vol -= fadeout_speed * Time.deltaTime;
//            }
//            if (audio_forte_vol > -80.0f & audio_soft_vol > -5.0f)
//            {
//                audio_forte_vol -= fadeout_speed * Time.deltaTime;
//            }
//        }

//        if (med)
//        {
//            if (audio_med_vol < 0.0f)
//            {
//                audio_med_vol += fadein_speed * Time.deltaTime;
//            }
//            if (audio_soft_vol > -80.0f & audio_med_vol > -5.0f)
//            {
//                audio_soft_vol -= fadeout_speed * Time.deltaTime;
//            }
//            if (audio_forte_vol > -80.0f & audio_med_vol > -5.0f)
//            {
//                audio_forte_vol -= fadeout_speed * Time.deltaTime;
//            }
//        }

//        if (forte)
//        {
//            if (audio_forte_vol < 0.0f)
//            {
//                audio_forte_vol += fadein_speed * Time.deltaTime;
//            }
//            if (audio_med_vol > -80.0f & audio_forte_vol > -5.0f)
//            {
//                audio_med_vol -= fadeout_speed * Time.deltaTime;
//            }
//            if (audio_soft_vol > -80.0f & audio_forte_vol > -5.0f)
//            {
//                audio_soft_vol -= fadeout_speed * Time.deltaTime;
//            }
//        }

//        if (!soft & !med & !forte)
//        {
//            if (audio_forte_vol > -80.0f)
//            {
//                audio_forte_vol -= fadeout_speed * Time.deltaTime;
//            }
//            if (audio_forte_vol < -70.0f)
//            {
//                forte_isPlaying = false;
//            }
//            if (audio_med_vol > -80.0f)
//            {
//                audio_med_vol -= fadeout_speed * Time.deltaTime;
//            }
//            if (audio_med_vol < -70.0f)
//            {
//                med_isPlaying = false;
//            }
//            if (audio_soft_vol > -80.0f)
//            {
//                audio_soft_vol -= fadeout_speed * Time.deltaTime;
//            }
//            if (audio_soft_vol < -70.0f)
//            {
//                soft_isPlaying = false;
//            }

//        }
//    }







//    public void CheckDistanceToTrigger()
//    {
//        try
//        {
//            enemy1 = Vector3.Distance(Player.position, enemy_1.position);
//        }
//        catch
//        {
//            //do nothing
//        }
//        try
//        {
//            enemy2 = Vector3.Distance(Player.position, enemy_2.position);
//        }
//        catch
//        {
//            //do nothing
//        }
//        try
//        {
//            enemy3 = Vector3.Distance(Player.position, enemy_3.position);
//        }
//        catch
//        {
//            //do nothing
//        }
//        try
//        {
//            enemy4 = Vector3.Distance(Player.position, enemy_4.position);
//        }
//        catch
//        {
//            //do nothing
//        }
//        try
//        {
//            enemy5 = Vector3.Distance(Player.position, enemy_5.position);
//        }
//        catch
//        {
//            //do nothing
//        }
//        if (enemy1 == 0 | enemy_1 == null)
//        {
//            enemy1 = 5000;
//        }
//        if (enemy2 == 0 | enemy_2 == null)
//        {
//            enemy2 = 5000;
//        }
//        if (enemy3 == 0 | enemy_3 == null)
//        {
//            enemy3 = 5000;
//        }
//        if (enemy4 == 0 | enemy_4 == null)
//        {
//            enemy4 = 5000;
//        }
//        if (enemy5 == 0 | enemy_5 == null)
//        {
//            enemy5 = 5000;
//        }

//        float[] distance_to_enemy = {enemy1 ,
//            enemy2,
//            enemy3,
//            enemy4 ,
//            enemy5};
//        System.Array.Sort(distance_to_enemy);
//        nearest_enemy = distance_to_enemy[0];

//    }

//    public void CheckMood()
//    {


//        if (nearest_enemy > trigger_med)
//        {
//            if (!start)
//            {
//                start = true;
//            }
//            if (reset_time >= 0)
//            {
//                reset_time -= Time.deltaTime;
//            }
//            if (reset_time < 0)
//            {
//                soft = true;
//                med = false;
//                forte = false;
//            }

//        }
//        if (nearest_enemy < trigger_med & nearest_enemy > trigger_forte)
//        {
//            if (!start)
//            {
//                start = true;
//            }
//            if (reset_time >= 0)
//            {
//                reset_time -= Time.deltaTime;
//            }
//            if (reset_time < 0)
//            {
//                soft = false;
//                med = true;
//                forte = false;
//            }
//        }
//        if (nearest_enemy < trigger_forte)
//        {
//            if (!start)
//            {
//                start = true;
//            }
//            reset_time = reset_timer;
//            soft = false;
//            med = false;
//            forte = true;
//        }
//    }

//    public void SetTempo()
//    {
//        if (adventure)
//        {
//            bpm = 100;
//        }
//        if (heroic)
//        {
//            bpm = 120;
//        }
//        if (ambiant_light_forest)
//        {
//            bpm = 120;
//        }
//    }

//    public void PlayTransition()
//    {
//        if (adventure & soft)
//        {
//            audio_end.clip = AudioArray_end_soft_adventure[0] as AudioClip;
//        }
//        if (heroic & soft)
//        {
//            audio_end.clip = AudioArray_end_soft_heroic[0] as AudioClip;

//        }


//        if (adventure & med)
//        {
//            audio_end.clip = AudioArray_end_med_adventure[0] as AudioClip;
//        }
//        if (heroic & med)
//        {
//            audio_end.clip = AudioArray_end_med_heroic[0] as AudioClip;

//        }

//        if (adventure & forte)
//        {
//            audio_end.clip = AudioArray_end_forte_adventure[0] as AudioClip;
//        }
//        if (heroic & forte)
//        {
//            audio_end.clip = AudioArray_end_forte_heroic[0] as AudioClip;

//        }


//        audio_end.PlayOneShot(audio_end.clip, 1.0f);

//    }

//    public void SetMood()
//    {

//        if (switch_audio_source)
//        {

//            if (adventure)
//            {
//                audio_soft1.clip = AudioArray_soft_adventure[random_theme] as AudioClip;
//                audio_med1.clip = AudioArray_med_adventure[random_theme] as AudioClip;
//                audio_forte1.clip = AudioArray_forte_adventure[random_theme] as AudioClip;
//            }
//            if (heroic)
//            {
//                audio_soft1.clip = AudioArray_soft_heroic[random_theme] as AudioClip;
//                audio_med1.clip = AudioArray_med_heroic[random_theme] as AudioClip;
//                audio_forte1.clip = AudioArray_forte_heroic[random_theme] as AudioClip;


//            }

//            if (ambiant_light_forest)
//            {
//                audio_ambiant1.clip = AudioArray_ambiant_light_forest[random_ambiant] as AudioClip;

//            }
//        }
//        if (!switch_audio_source)
//        {
//            if (adventure)
//            {
//                audio_soft2.clip = AudioArray_soft_adventure[random_theme] as AudioClip;
//                audio_med2.clip = AudioArray_med_adventure[random_theme] as AudioClip;
//                audio_forte2.clip = AudioArray_forte_adventure[random_theme] as AudioClip;
//            }
//            if (heroic)
//            {
//                audio_soft2.clip = AudioArray_soft_heroic[random_theme] as AudioClip;
//                audio_med2.clip = AudioArray_med_heroic[random_theme] as AudioClip;
//                audio_forte2.clip = AudioArray_forte_heroic[random_theme] as AudioClip;


//            }

//            if (ambiant_light_forest)
//            {
//                audio_ambiant2.clip = AudioArray_ambiant_light_forest[random_ambiant] as AudioClip;

//            }
//        }



//    }

//    private void myDropdownValueChangedHandler(sbyte target)
//    {
//        if (target == 0)
//        {
//            adventure = false;
//            heroic = false;
//            ambiant_light_forest = false;
//        }
//        if (target == 1)
//        {
//            Light_Forest_onClick();
//        }
//        if (target == 2)
//        {
//            Adventure_onClick();
//        }
//        if (target == 3)
//        {
//            Heroic_onClick();
//        }

//    }

//    public void PlayDemo()
//    {

//        if (in_time)
//        {
//            if (j == 8)
//            {
//                if (soft)
//                {
//                    audio_demo.volume = 0.3f;
//                }

//                if (med)
//                {
//                    audio_demo.volume = 0.5f;
//                }
//                if (forte)
//                {
//                    audio_demo.volume = 0.9f;
//                }
//                audio_demo.PlayScheduled(time);
//                j = 0;
//            }

//        }
//    }
//}

