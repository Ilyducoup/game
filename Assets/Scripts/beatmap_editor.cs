using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class beatmap_editor : MonoBehaviour
{
    public GameObject songTitle;
    public GameObject songArtist;
    public GameObject songThumbnail;
    public GameObject playButton;
    private Song song;
    private AudioSource audioSource;

    void Awake()
    {
        if (song_data.songs.Count == 0)
        {
            song_loader.LoadSongs();
        }
        if (song_data.currentSong == null)
        {
            song_data.currentSong = song_data.songs[0];
        }
    }
    void Start()
    {
        song = song_data.currentSong;
        songTitle.GetComponent<TextMeshProUGUI>().SetText(song.title);
        songArtist.GetComponent<TextMeshProUGUI>().SetText(song.artist);
        songThumbnail.GetComponent<Image>().sprite = song.thumbnail;
        playButton.GetComponent<Button>().onClick.AddListener(playButtonPressed);
        audioSource = GetComponent<AudioSource>();
    }
    void playButtonPressed()
    {
        Debug.Log("Clicked!");
        song_data.isSongPlaying = !song_data.isSongPlaying;
        if (song_data.isSongPlaying)
        {
            audioSource.PlayOneShot(song.clip, song.volume);
        }
        else
        {
            audioSource.Stop();
        }
    }
}
