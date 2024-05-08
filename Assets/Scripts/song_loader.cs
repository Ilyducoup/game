using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

[System.Serializable]
public class Song
{
    public string songName;
    public AudioClip clip;
    public string info;
    public string songInfo;
    public string title;
    public string artist;
    public int duration;
    public float volume;
    public float pitch;
    public Sprite thumbnail;
}

[System.Serializable]
public class songJson
{
    public string song_title;
    public string song_artist;
    public int duration;
    public float volume;
    public float pitch;
}

public class song_loader : MonoBehaviour
{
    public Transform contentContainer;
    public GameObject rowItemPrefab;
    
    void Start()
    {
        if (song_data.songs.Count == 0)
        {
            LoadSongs();
        }
        foreach (Song song in song_data.songs)
        {
            var songRow = Instantiate(rowItemPrefab);
            songRow.transform.Find("Artist").GetComponent<TextMeshProUGUI>().SetText(song.artist);
            songRow.transform.Find("Title").GetComponent<TextMeshProUGUI>().SetText(song.title);
            songRow.transform.Find("EditorButton").GetComponent<beatmap_editor_loader>().song = song;
            songRow.transform.Find("Thumbnail").GetComponent<Image>().sprite = song.thumbnail;
            songRow.transform.SetParent(contentContainer);
            songRow.transform.localScale = Vector2.one;

        }
    }

    public static void LoadSongs()
    {
        string[] songNames = Directory.GetFiles(song_data.songFolderPath, "*.ogg");
        foreach (string songName_t in songNames)
        {
            string songName = songName_t.Replace(song_data.songFolderPath + "\\", "");
            songName = songName.Replace(".ogg", "");
            Song song = new Song();
            song.songName = songName;
            song_data.songs.Add(song);
            Debug.Log($"Found song {songName}");
        }
        foreach(Song song in song_data.songs)
        {
            string jsonString = File.ReadAllText($"{song_data.songFolderPath}/{song.songName}.info.json");
            songJson songData = JsonUtility.FromJson<songJson>(jsonString);
            song.title = songData.song_title;
            song.artist = songData.song_artist;
            song.duration = songData.duration;
            song.volume = songData.volume;
            song.pitch = songData.pitch;
            song.songInfo = jsonString;
            Debug.Log($"{song.songName} : {song.artist} - {song.title}, {(int)song.duration / 60}:{song.duration % 60}");
            Sprite thumbnail = Resources.Load<Sprite>($"{song_data.songFolder}/{song.songName}");
            if (thumbnail != null)
            {
                song.thumbnail = thumbnail;
            }
            else
            {
                Debug.LogError($"Sprite with filename '{song_data.songFolderPath}/{song.songName}' not found!");
            }
            // string audioClipUrl = $"file://{song_data.songFolderPath}/{song.songName}.ogg";
            // using (UnityWebRequest web = UnityWebRequestMultimedia.GetAudioClip(audioClipUrl))
            // {
            //     yield return web.SendWebRequest();
            //     if(!web.isNetworkError && !web.isHttpError)
            //     {
            //         var clip = DownloadHandlerAudioClip.GetContent(web);
            //         if(clip != null)
            //         {
            //             songs.Add(clip);
            //         }
            //     }
            // }
        }
    }
}
