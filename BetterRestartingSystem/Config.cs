using Exiled.API.Interfaces;
using System.ComponentModel;

namespace BetterRestartingSystem
{
    public class Config : IConfig
    {
        [Description("Determines whether the plugin is enabled.")]
        public bool IsEnabled { get; set; } = true;

        [Description("Enable debug mode.")]
        public bool Debug { get; set; } = false;

        [Description("Should lights go off after the warhead stopped?")]
        public bool TurnOffLights { get; set; } = true;

        [Description("If enabled, specifies how much time after stopping the warhead the lights should go off.")]
        public float TurnOffLightsDelay { get; set; } = 20;

        [Description("If enabled, specifies how long the lights should stay off.")]
        public float TurnOffLightsTime { get; set; } = 15;

        [Description("Broadcast shown while the systems are restarting.")]
        public Exiled.API.Features.Broadcast WhileRestartingBroadcast { get; set; } = new Exiled.API.Features.Broadcast()
        {
            Content = "<i>Restarting Systems...</i>",
            Duration = 10,
            Show = true,
            Type = Broadcast.BroadcastFlags.Normal
        };

        [Description("Probability for a failed system restart to occur.")]
        public float FailChance { get; set; } = 50f;

        [Description("Cassie announcement played after a failed system restart.")]
        public string FailCassie { get; set; } = "pitch_0.8 warning pitch_0.9 warning pitch_1 bell_start . alpha warhead systems failure . evacuate the facility now . repeat evacuate the facility now . warhead will detonate in tminus 120 seconds bell_end";

        public string FailCassieTranslation { get; set; } = "warning, warning warhead restart failed.";

        [Description("Broadcast shown after a failed system restart. I recommend to sync this broadcast's duration to the time required for detonation.")]
        public Exiled.API.Features.Broadcast FailBroadcast { get; set; } = new Exiled.API.Features.Broadcast()
        {
            Content = "<b><color=purple> ALERT: There was an error while restarting systems! Estimated explosion in T-128 seconds!</color></b>",
            Duration = 10,
            Show = true,
            Type = Broadcast.BroadcastFlags.Normal
        };

        [Description("Specifies how much time after a failed restart the warhead explodes.")]
        public float TimeForDetonation { get; set; } = 128f;

        [Description("Cassie announcement played after a successful system restart.")]
        public string SuccessfulCassie { get; set; } = "bell_start . no anomaly found . alpha warhead process has been disabled correct . bell_end";

        public string SuccessfulCassieTranslation { get; set; } = "system restart successful";

        [Description("Broadcast shown after a successful system restart.")]
        public Exiled.API.Features.Broadcast SuccessfulBroadcast { get; set; } = new Exiled.API.Features.Broadcast()
        {
            Content = "<b><color=green>System restarted successfully.</color></b>",
            Duration = 10,
            Show = true,
            Type = Broadcast.BroadcastFlags.Normal
        };

        [Description("Light color after failed restart. From 0 to 255")]
        public int RoomsColorR { get; set; } = 240;

        public int RoomsColorG { get; set; } = 55;

        public int RoomsColorB { get; set; } = 191;

        [Description("Determines whether to reset room colors after the warhead detonates.")]
        public bool ResetLights { get; set; } = true;
    }
}