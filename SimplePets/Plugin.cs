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
        private EventHandler eventHandler;
        public static Dictionary<Player, Npc> PetDictionary { get; set; } = new();

        public override void OnEnabled()
        {
            Instance = this;
            eventHandler = new EventHandler();
            Exiled.Events.Handlers.Player.TriggeringTesla += eventHandler.OnTriggeringTesla;
            Exiled.Events.Handlers.Scp096.Enraging += eventHandler.OnTriggering096;
            Exiled.Events.Handlers.Scp173.BeingObserved += eventHandler.OnTriggering173;
            Exiled.Events.Handlers.Player.Died += eventHandler.OnPlayerDied;
            Exiled.Events.Handlers.Player.Escaping += eventHandler.OnEscaping;
            Exiled.Events.Handlers.Player.Handcuffing += eventHandler.OnHandcuffing;
            Exiled.Events.Handlers.Player.ChangingRole += eventHandler.OnChangingRole;
            Exiled.Events.Handlers.Player.MakingNoise += eventHandler.OnMakingNoise;

            Log.Debug("Loaded succesfully");
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Exiled.Events.Handlers.Player.MakingNoise -= eventHandler.OnMakingNoise;
            Exiled.Events.Handlers.Player.ChangingRole -= eventHandler.OnChangingRole;
            Exiled.Events.Handlers.Player.Handcuffing -= eventHandler.OnHandcuffing;
            Exiled.Events.Handlers.Player.Escaping -= eventHandler.OnEscaping;
            Exiled.Events.Handlers.Player.Died -= eventHandler.OnPlayerDied;
            Exiled.Events.Handlers.Scp173.BeingObserved -= eventHandler.OnTriggering173;
            Exiled.Events.Handlers.Scp096.Enraging -= eventHandler.OnTriggering096;
            Exiled.Events.Handlers.Player.TriggeringTesla += eventHandler.OnTriggeringTesla;
            eventHandler = null;
            Instance = null;

            Log.Debug("Disabled");
            base.OnDisabled();
        }
    }
}