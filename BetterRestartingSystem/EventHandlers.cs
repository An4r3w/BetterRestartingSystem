using Exiled.API.Features;
using Exiled.Events.EventArgs.Server;
using Exiled.Events.EventArgs.Warhead;
using MEC;
using UnityEngine;

namespace BetterRestartingSystem
{
    public class EventHandlers
    {
        private float stopProbability = Plugin.Singleton.Config.FailChance;
        private bool roundEnding = false;
        private CoroutineHandle detonationCoroutine;

        public void OnRoundStarted()
        {
            roundEnding = false;
        }

        public void OnRoundEnding(EndingRoundEventArgs ev)
        {
            roundEnding = true;

            if (detonationCoroutine.IsRunning)
            {
                Timing.KillCoroutines(detonationCoroutine);
            }
        }

        public void OnRestartingRound()
        {
            roundEnding = true;

            if (detonationCoroutine.IsRunning)
            {
                Timing.KillCoroutines(detonationCoroutine);
            }
        }

        public void OnStopping(StoppingEventArgs ev)
        {
            float randomValue = Random.Range(0f, 100f);

            if (Warhead.IsLocked == false && !roundEnding)
            {
                Map.Broadcast(Plugin.Singleton.Config.WhileRestartingBroadcast);

                Timing.CallDelayed(Plugin.Singleton.Config.WhileRestartingBroadcast.Duration, () =>
                {
                    if (randomValue <= stopProbability)
                    {
                        Cassie.MessageTranslated(message: Plugin.Singleton.Config.FailCassie, translation: Plugin.Singleton.Config.FailCassieTranslation, isNoisy: false);
                        Map.Broadcast(Plugin.Singleton.Config.FailBroadcast);

                        Map.TurnOffAllLights(1);

                        foreach (RoomLightController controller in RoomLightController.Instances)
                        {
                            Color color = new Color(Plugin.Singleton.Config.RoomsColorR / 255f, Plugin.Singleton.Config.RoomsColorG / 255f, Plugin.Singleton.Config.RoomsColorB / 255f);
                            controller.NetworkOverrideColor = color;
                        }

                        detonationCoroutine = Timing.CallDelayed(Plugin.Singleton.Config.TimeForDetonation, () => { Warhead.Detonate(); });
                    }
                    else
                    {
                        Cassie.MessageTranslated(message: Plugin.Singleton.Config.SuccessfulCassie, translation: Plugin.Singleton.Config.SuccessfulCassieTranslation, isNoisy: false);
                        Map.Broadcast(Plugin.Singleton.Config.SuccessfulBroadcast);
                    }
                });
            }
        }

        public void OnDetonated()
        {
            if (Plugin.Singleton.Config.ResetLights)
            {
                Map.TurnOffAllLights(1);

                foreach (Room room in Room.List)
                {
                    room?.ResetColor();
                }
            }
        }
    }
}
