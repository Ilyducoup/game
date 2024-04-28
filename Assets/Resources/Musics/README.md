# Musics

All musics used are considered No Copyright / Royalty Free.

## Credits

- #1 : [Jeremy Blake - Powerup!](https://www.youtube.com/watch?v=mrgVpZhjOWk)

## Acquisition

Musics are acquired through [yp-dlp](https://github.com/yt-dlp/yt-dlp) :

```powershell
yt-dlp.exe -x -f ba --audio-format vorbis --write-thumbnail --convert-thumbnails png --write-info-json -o "<name>.%(ext)s" <url/shorturl>
```

Only sound is extracted thanks to -x (requires [ffmpeg](https://ffmpeg.org/documentation.html)).
