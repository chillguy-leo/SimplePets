using CommandSystem;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;
using MEC;
using System;
using UnityEngine;

namespace SimplePets.Commands
{
    [CommandHandler(typeof(ClientCommandHandler))]
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class CreatePet : ICommand
    {
        public string Command => "pet";

        public string[] Aliases => new[] { "makepet" };

        public string Description => "Creates a pet version of yourself.";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.CheckPermission("sp.pet"))
            {
                response = "You do not have permission to use this command.";
                return false;
            }

            var player = Player.Get(sender); // gets player

            if (player.IsDead)
            {
                response = "You cannot use this command if you are dead.";
                return false;
            }

            if (Plugin.PetDictionary.ContainsKey(player))
            {
                response = "You already have a pet. Use the '.dpet' command to delete it before creating a new one.";
                return false;
            }

            var dummy = Npc.Spawn($"{player.Nickname}'s Pet", player.Role, true, player.Position); // makes dummy 
            Log.Debug($"Created pet for {player.Nickname}");

            Plugin.PetDictionary.Add(player, dummy);

            // sets values
            Vector3 size = new(0.4f, 0.4f, 0.4f);
            dummy.Scale = size;

            Timing.CallDelayed(0.5f, () =>
            {
                dummy.Follow(player);
                dummy.EnableEffect(EffectType.Ghostly, 1, -1f);
                dummy.EnableEffect(EffectType.SilentWalk, 255, -1f);

                if (Plugin.Instance.Config.GodPets)
                { dummy.IsGodModeEnabled = true; }

                if (!dummy.IsScp)
                { dummy.Emotion = Plugin.Instance.Config.PetEmotion; }

                Log.Debug("Pet has been set up with settings");
            });

            response = "Your pet has been created! Use the '.dpet' command to remove it.";
            return true;
        }
    }
}

