using Exiled.API.Features;
using Exiled.Events.EventArgs.Warhead;
using MEC;
using UnityEngine;

namespace BetterRestartingSystem
{
    public class EventHandlers
    {
        private float stopProbability = Plugin.Singleton.Config.FailChance;

        public void OnStopping(StoppingEventArgs ev)
        {
            float randomValue = Random.Range(0f, 100f);

            if (Plugin.Singleton.Config.TurnOffLights == true)
            {
                Timing.CallDelayed(Plugin.Singleton.Config.TurnOffLightsDelay, () => { Map.TurnOffAllLights(Plugin.Singleton.Config.TurnOffLightsTime); });
            }

            Map.Broadcast(Plugin.Singleton.Config.WhileRestartingBroadcast);

            Timing.CallDelayed(Plugin.Singleton.Config.WhileRestartingBroadcast.Duration, () =>
            {
                if (randomValue <= stopProbability)
                {
                    Cassie.MessageTranslated(message: Plugin.Singleton.Config.FailCassie, translation: Plugin.Singleton.Config.FailCassieTranslation, isNoisy: false);
                    Map.Broadcast(Plugin.Singleton.Config.FailBroadcast);

                    foreach (RoomLightController controller in RoomLightController.Instances)
                    {
                        Color color = new Color(Plugin.Singleton.Config.RoomsColorR / 255f, Plugin.Singleton.Config.RoomsColorG / 255f, Plugin.Singleton.Config.RoomsColorB / 255f);
                        controller.NetworkOverrideColor = color;
                    }
                    Timing.CallDelayed(Plugin.Singleton.Config.TimeForDetonation, () => { Warhead.Detonate(); });
                }
                else
                {
                    Cassie.MessageTranslated(message: Plugin.Singleton.Config.SuccessfulCassie, translation: Plugin.Singleton.Config.SuccessfulCassieTranslation, isNoisy: false);
                    Map.Broadcast(Plugin.Singleton.Config.SuccessfulBroadcast);
                }
            });
        }

        public void OnDetonated()
        {
            if (Plugin.Singleton.Config.ResetLights)
            {
                foreach (Room room in Room.List)
                {
                    room?.ResetColor();
                }
            }
        }
    }
}