using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Sound
{
    public string soundName;
    public AudioClip clip;
    public string info;
    public string soundInfo;
    public string title;
    public string artist;
    public int duration;
    public float volume;
    public float pitch;
}

[System.Serializable]
public class SoundJson
{
    public string song_title;
    public string song_artist;
    public int duration;
    public float volume;
    public float pitch;
}

public class audio_manager : MonoBehaviour
{
    public Transform contentContainer;
    public GameObject rowItemPrefab;
    public string musicFolder = "Assets/Resources/Musics";
    [HideInInspector] public List<Sound> sounds;
    
    void Start()
    {
        string[] soundNames = Directory.GetFiles(musicFolder, "*.ogg");
        foreach (string soundName_t in soundNames)
        {
            string soundName = soundName_t.Replace(musicFolder + "\\", "");
            soundName = soundName.Replace(".ogg", "");
            Sound sound = new Sound();
            sound.soundName = soundName;
            sounds.Add(sound);
            Debug.Log($"Found sound {soundName}");
        }
        foreach(Sound sound in sounds)
        {
            string jsonString = File.ReadAllText($"{musicFolder}/{sound.soundName}.info.json");
            SoundJson soundData = JsonUtility.FromJson<SoundJson>(jsonString);
            sound.title = soundData.song_title;
            sound.artist = soundData.song_artist;
            sound.duration = soundData.duration;
            sound.volume = soundData.volume;
            sound.pitch = soundData.pitch;
            sound.soundInfo = jsonString;
            Debug.Log($"{sound.soundName} : {sound.artist} - {sound.title}, {(int)sound.duration / 60}:{sound.duration % 60}");
            var soundRow = Instantiate(rowItemPrefab);
            soundRow.transform.Find("Artist").GetComponent<TextMeshProUGUI>().SetText(sound.artist);
            soundRow.transform.Find("Title").GetComponent<TextMeshProUGUI>().SetText(sound.title);
            Sprite thumbnail = Resources.Load<Sprite>($"Musics/{sound.soundName}");
            if (thumbnail != null)
            {
                soundRow.transform.Find("Thumbnail").GetComponent<Image>().sprite = thumbnail;
            }
            else
            {
                Debug.LogError($"Sprite with filename '{musicFolder}/{sound.soundName}' not found!");
            }
            // soundRow.transform.Find("Thumbnail").GetComponent<Image>().sprite = Resources.Load<Sprite>($"Assets/Musics/{sound.soundName}.webp");
            soundRow.transform.SetParent(contentContainer);
            soundRow.transform.localScale = Vector2.one;
        }
    }
}
