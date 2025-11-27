using WMPLib;
using System.IO;
using System.Windows.Forms;

public static class MusicPlayer
{
    private static WindowsMediaPlayer backgroundPlayer;

    public static void StartMusic()
    {
        if (backgroundPlayer == null)
        {
            backgroundPlayer = new WindowsMediaPlayer();

            string musicPath = Path.Combine(
                Application.StartupPath,
                "Sounds",
                "game-gaming-minecraft-background-music-377647.wav"
            );

            backgroundPlayer.URL = musicPath;
            backgroundPlayer.settings.setMode("loop", true);
            backgroundPlayer.controls.play();
        }
    }

    public static void PlayEatSound()
    {
        string effectPath = Path.Combine(
            Application.StartupPath,
            "Sounds",
            "food_G1U6tlb.wav"
        );

        WindowsMediaPlayer sfx = new WindowsMediaPlayer();
        sfx.URL = effectPath;
        sfx.controls.play();
    }

    public static void StopMusic()
    {
        if (backgroundPlayer != null)
            backgroundPlayer.controls.stop();
    }
}

