using WMPLib;

public static class MusicPlayer
{
    public static WindowsMediaPlayer backgroundPlayer;

    public static void StartMusic()
    {
        if (backgroundPlayer == null)
        {
            backgroundPlayer = new WindowsMediaPlayer();
            backgroundPlayer.URL = @"C:\Users\MSI-Pc\Downloads\game-gaming-minecraft-background-music-377647.mp3"; // include extension
            backgroundPlayer.settings.setMode("loop", true);
            backgroundPlayer.controls.play();
        }
    }

    public static void PlayEatSound()
    {
        WindowsMediaPlayer effect = new WindowsMediaPlayer();
        effect.URL = @"C:\Users\MSI-Pc\Downloads\food_G1U6tlb.wav";
        effect.controls.play();
    }

    public static void StopMusic()
    {
        if (backgroundPlayer != null) backgroundPlayer.controls.stop();
    }
}
