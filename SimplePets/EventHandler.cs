using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;

namespace SimplePets
{
    public class EventHandler
    {
        // pet despawn
        public void OnPlayerDied(DiedEventArgs ev)
        {
            if (Plugin.PetDictionary.TryGetValue(ev.Player, out var dummy) && Plugin.Instance.Config.DespawnPetsAfterDeath)
            {
                dummy.Destroy();
                Plugin.PetDictionary.Remove(ev.Player);

                // yes i knoww this is a bad way to position broadcasts but i dont care
                ev.Player.Broadcast(5, "<size=30>\n\n\n\nYour pet was despawned as you died.</size>");

                Log.Debug("Pet despawned due to players death");
            }
        }

        // deny pet & chaning role
        public void OnChangingRole(ChangingRoleEventArgs ev)
        {
            if (Plugin.PetDictionary.TryGetValue(ev.Player, out var dummy))
            {
                dummy.Role.Set(ev.Player.Role);
            }
        }
        public void OnEscaping(EscapingEventArgs ev)
        {
            if (ev.Player.RankName == "pet" && ev.Player.IsNPC)
            { ev.IsAllowed = false; }
        }
        public void OnHandcuffing(HandcuffingEventArgs ev)
        {
            if (ev.Player.RankName == "pet" && ev.Player.IsNPC)
            { ev.IsAllowed = false; }
        }
        public void OnMakingNoise(MakingNoiseEventArgs ev)
        {
            if (ev.Player.RankName == "pet" && ev.Player.IsNPC)
            { ev.IsAllowed = false; }
        }
    }
}
