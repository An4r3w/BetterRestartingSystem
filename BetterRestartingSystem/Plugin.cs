using Warhead = Exiled.Events.Handlers.Warhead;
using Server = Exiled.Events.Handlers.Server;
using Exiled.API.Features;
using System;
using Exiled.API.Enums;

namespace BetterRestartingSystem
{
    public class Plugin : Plugin<Config>
    {
        public static Plugin Singleton;

        private EventHandlers events;
        public override string Author => "An4r3w";

        public override string Name { get; } = "BetterRestartingSystem";

        public override string Prefix { get; } = "BetterRestartingSystem";

        public override Version Version => new Version(2, 0, 0);

        public override Version RequiredExiledVersion => new Version(7, 0, 0);

        public override PluginPriority Priority => PluginPriority.Default;

        public override void OnEnabled()
        {
            base.OnEnabled();

            Singleton = this;
            events = new EventHandlers();

            Warhead.Stopping += events.OnStopping;
            Warhead.Detonated += events.OnDetonated;
            Server.EndingRound += events.OnRoundEnding;
            Server.RoundStarted += events.OnRoundStarted;
            Server.RestartingRound += events.OnRestartingRound;
        }

        public override void OnDisabled()
        {
            base.OnDisabled();

            Warhead.Stopping -= events.OnStopping;
            Warhead.Detonated -= events.OnDetonated;
            Server.EndingRound -= events.OnRoundEnding;
            Server.RoundStarted -= events.OnRoundStarted;
            Server.RestartingRound -= events.OnRestartingRound;

            events = null;
        }
    }
}
