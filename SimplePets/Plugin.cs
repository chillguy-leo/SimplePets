using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using System;
using System.Collections.Generic;

namespace SimplePets
{
    public class Plugin : Plugin<Config>
    {
        public override string Name => "SimplePets";
        public override string Author => "chillguy-leo";
        public override Version Version => new Version(1, 0, 0);
        public static Plugin Instance { get; private set; }
        public static Dictionary<Player, Npc> PetDictionary { get; set; } = new();

        public override void OnEnabled()
        {
            Instance = this;
            Exiled.Events.Handlers.Player.Died += OnPlayerDied;

            Log.Debug("Loaded succesfully");
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Exiled.Events.Handlers.Player.Died -= OnPlayerDied;
            Instance = null;

            Log.Debug("Disabled");
            base.OnDisabled();
        }
        private void OnPlayerDied(DiedEventArgs ev)
        {
            if (Plugin.PetDictionary.TryGetValue(ev.Player, out var dummy) && Instance.Config.DespawnPetsAfterDeath)
            {
                dummy.Destroy();
                Plugin.PetDictionary.Remove(ev.Player);

                // yes i knoww this is a bad way to position broadcasts but i dont care
                ev.Player.Broadcast(5, "<size=30>\n\n\n\nYour pet was despawned as you died.</size>");

                Log.Debug("Pet despawned due to players death");
            }
        }

    }
}