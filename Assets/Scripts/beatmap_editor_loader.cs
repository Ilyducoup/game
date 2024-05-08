using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class beatmap_editor_loader : MonoBehaviour
{
    public Song song;
    public void LoadScene(string sceneName)
    {
        Debug.Log(song.songName + " " + sceneName);
        song_data.currentSong = song;
        SceneManager.LoadScene(sceneName);
    }
}
