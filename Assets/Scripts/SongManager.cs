using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using System.IO;
using System.Net;
using UnityEngine.Networking;
using System;
using System.Linq;

public class SongManager : MonoBehaviour
{
    [SerializeField] public static SongManager Instance;
    [SerializeField] public AudioSource audioSource;
    [SerializeField] public Lane[] lanes;
    [SerializeField] public float songDelayInSeconds;
    [SerializeField] public int inputDelayInMilliseconds;
    [SerializeField] public double marginOfError;
    [SerializeField] public string fileLocation;
    [SerializeField] public float noteTime;
    [SerializeField] public float noteSpawnX;
    [SerializeField] public float noteTapX;
    private float timeLeft;
    private bool finished;
    public float noteDespawnX
    {
        get
        {
            return noteTapX - (noteSpawnX - noteTapX);
        }
    }
    public static MidiFile midiFile;
    void Start()
    {
        finished = false;
        Instance = this;
        if (Application.streamingAssetsPath.StartsWith("http://") ||
            Application.streamingAssetsPath.StartsWith("https://"))
        {
            StartCoroutine(ReadFromWebsite());
        }
        else
        {
            ReadFromFile();
        }
        TempoMap tempoMap = midiFile.GetTempoMap();
        TimeSpan midiFileDuration = midiFile.GetTimedEvents().LastOrDefault(e => e.Event is NoteOffEvent)?.TimeAs<MetricTimeSpan>(tempoMap) ?? new MetricTimeSpan();
        timeLeft = (float)midiFileDuration.TotalSeconds;
        Debug.Log(timeLeft+" seceond.");
    }
    private void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0)
        {
            if (finished == false)
            {
                StartCoroutine(SongEnded());
                finished = true;
            }
        }
    }
    private IEnumerator ReadFromWebsite()
    {
        using (UnityWebRequest www = UnityWebRequest.Get(Application.streamingAssetsPath + "/" + fileLocation))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.LogError(www.error);
            }
            else
            {
                byte[] results = www.downloadHandler.data;
                using (var stream = new MemoryStream(results))
                {
                    midiFile = MidiFile.Read(stream);
                    GetDataFromMidi();
                }
            }
        }
    }
    private void ReadFromFile()
    {
        midiFile = MidiFile.Read(Application.streamingAssetsPath + "/" + fileLocation);
        GetDataFromMidi();
    }

    public void GetDataFromMidi()
    {
        var notes = midiFile.GetNotes();
        var array = new Melanchall.DryWetMidi.Interaction.Note[notes.Count];
        notes.CopyTo(array, 0);
        foreach (var lane in lanes) lane.SetTimeStamps(array);
        Invoke(nameof(StartSong), songDelayInSeconds);
    }
    public void StartSong()
    {
        audioSource.Play();
    }
    public static double GetAudioSourceTime()
    {
        return (double)Instance.audioSource.timeSamples / Instance.audioSource.clip.frequency;
    }
    private IEnumerator SongEnded()
    {
        Debug.Log("Song ended waiting for result.");
        yield return new WaitForSeconds(5); 
        Debug.Log("Result");
    }
}