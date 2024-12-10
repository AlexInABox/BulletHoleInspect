namespace BulletHoleInspect.Events
{
    using System.Threading.Tasks;
    using Exiled.API.Features;
    using Exiled.Events.EventArgs.Cassie;
    using AudioPlayer.API;
    using MEC;
    using Utils;

    internal sealed class CassieHandler
    {
        public async void OnSendingCassieMessage(SendingCassieMessageEventArgs ev)
        {
            Log.Info("CASSIE Announcement:");
            Log.Info($"Words: {ev.Words}");
            Log.Info($"MakeHold: {ev.MakeHold}");
            Log.Info($"MakeNoise: {ev.MakeNoise}");
            Log.Info($"IsAllowed: {ev.IsAllowed}");

            // Generate the voiceline asynchronously
            Timing.RunCoroutine(ElevenlabsWrapper.GenerateVoiceline(ev.Words));

            // Clear constantly for 2 seconds
            const int MAX_DELAY = 2000;
            int waited_for = 0;
            while (waited_for < MAX_DELAY)
            {
                Cassie.Clear();
                waited_for++;
                await Task.Delay(1);
            }
        }
    }
}