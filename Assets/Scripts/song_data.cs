using System.Collections.Generic;
using UnityEngine;

public static class song_data
{
    public static string songFolder = "Songs";
    public static string songFolderPath = "Assets/Resources/" + songFolder;
    public static List<Song> songs = new List<Song>();
    public static Song currentSong;
    public static bool isSongPlaying = false;
}
