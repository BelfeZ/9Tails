using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using Melanchall.DryWetMidi.Interaction;
using MidiJack;

public class Lane : MonoBehaviour
{
    [SerializeField] public Melanchall.DryWetMidi.MusicTheory.NoteName noteRestriction;
    [SerializeField] public KeyCode input;
    [SerializeField] public int pianoInput;
    [SerializeField] public GameObject notePrefab;
    List<Note> notes = new List<Note>();
    [SerializeField] public List<double> timeStamps;
    [SerializeField] public CharacterAnimation charAnimation;
    private double timeStamp;
    private double marginOfError;
    private double audioTime;
    private bool[] noteHeld;
    int spawnIndex = 0;
    int inputIndex = 0;

    public Healthbar healthbar;

    void Start()
    {
        noteHeld = new bool[128];
    }

    public void SetTimeStamps(Melanchall.DryWetMidi.Interaction.Note[] array)
    {
        foreach (var note in array)
        {
            if (note.NoteName == noteRestriction)
            {
                var metricTimeSpan = TimeConverter.ConvertTo<MetricTimeSpan>(note.Time, SongManager.midiFile.GetTempoMap());
                timeStamps.Add((double)metricTimeSpan.Minutes * 60f + metricTimeSpan.Seconds + (double)metricTimeSpan.Milliseconds / 1000f);
            }
        }
    }
    void Update()
    {

        if (spawnIndex < timeStamps.Count)
        {
            if (SongManager.GetAudioSourceTime() >= timeStamps[spawnIndex] - SongManager.Instance.noteTime)
            {
                var note = Instantiate(notePrefab, transform);
                notes.Add(note.GetComponent<Note>());
                note.GetComponent<Note>().assignedTime = (float)timeStamps[spawnIndex];
                spawnIndex++;
            }
        }

        if (inputIndex < timeStamps.Count)
        {
            timeStamp = timeStamps[inputIndex];
            marginOfError = SongManager.Instance.marginOfError;
            audioTime = SongManager.GetAudioSourceTime() - (SongManager.Instance.inputDelayInMilliseconds / 1000.0);

            if (Input.GetKeyDown(input))
            {
                if (Math.Abs(audioTime - timeStamp) < marginOfError)
                {
                    PerfectHit();
                    print($"Hit on {inputIndex} note");
                    Destroy(notes[inputIndex].gameObject);
                    inputIndex++;
                }
                else if (Math.Abs(audioTime - timeStamp) < (marginOfError + 0.07f))
                {
                    GreatHit();
                    print($"GreatHit on {inputIndex} note");
                    Destroy(notes[inputIndex].gameObject);
                    inputIndex++;
                }
            }
            if (timeStamp + marginOfError <= audioTime)
            {
                Miss();
                print($"Missed {inputIndex} note");
                inputIndex++;
            }
        }       
    
    }

    private void OnEnable()
    {
        MidiMaster.noteOnDelegate += NoteOn;
        //MidiMaster.noteOffDelegate += NoteOff;
    }

    private void OnDisable()
    {
        MidiMaster.noteOnDelegate -= NoteOn;
        //MidiMaster.noteOffDelegate -= NoteOff;
    }

    void NoteOn(MidiChannel channel, int note, float velocity)
    {
        if (inputIndex < notes.Count)
        {
            if (note == pianoInput && notes[inputIndex] != null)
            {
                if (Math.Abs(audioTime - timeStamp) < marginOfError)
                {
                    PerfectHit();
                    print($"Hit on MIDI note {note}");
                    Destroy(notes[inputIndex].gameObject);
                    inputIndex++;
                }
                else if (Math.Abs(audioTime - timeStamp) < (marginOfError + 0.07f))
                {
                    GreatHit();
                    print($"GreatHit on MIDI note {note}");
                    Destroy(notes[inputIndex].gameObject);
                    inputIndex++;
                }
            }
            else if (timeStamp + marginOfError <= audioTime)
            {
                Miss();
                print($"Missed {inputIndex} note");
                inputIndex++;
            }
        }
    }
    /*void NoteOff(MidiChannel channel, int note)
    {

    }*/

    private void PerfectHit()
    {
        charAnimation.TriggerPerfectAnimation();
        ScoreManager.Perfect();
        healthbar.Perfect_Healthbar();
    }

    public void GreatHit()
    {
        charAnimation.TriggerGoodAnimation();
        ScoreManager.Great();
        healthbar.Great_Healthbar();
    }
    public void Miss()
    {
        charAnimation.TriggerMissedAnimation();
        ScoreManager.Miss();
        healthbar.Missed_Healthbar();
    }
}
