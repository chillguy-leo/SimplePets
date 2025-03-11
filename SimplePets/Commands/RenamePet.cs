using CommandSystem;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;
using MEC;
using System;
using System.Linq;
using UnityEngine;

namespace SimplePets.Commands
{
    [CommandHandler(typeof(ClientCommandHandler))]
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class RenamePet : ICommand, IUsageProvider
    {
        public string Command => "rpet";

        public string[] Aliases => new[] { "renamepet" };

        public string Description => "Renames your pet.";

        public string[] Usage { get; } = new string[] { "<new name>" };

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            var player = Player.Get(sender);

            if (!sender.CheckPermission("sp.rpet"))
            {
                response = "You do not have permission to use this command.";
                return false;
            }

            if (arguments.Count != 1)
            {
                response = "Usage: rpet <new name>.";
                return false;
            }

            if (Plugin.PetDictionary.TryGetValue(player, out var dummy))
            {
                string newName = string.Join(" ", arguments.ElementAt(0));

                if (string.IsNullOrWhiteSpace(newName) || newName.Length > 25)
                {
                    response = "The new name must be between 1 and 25 characters long.";
                    return false;
                }

                if (newName == "reset")
                {
                    dummy.DisplayNickname = null;
                    Log.Debug("Pet name reset");
                }

                dummy.DisplayNickname = newName;
                Log.Debug($"Pet renamed to {newName}");

                response = $"Your pet has been renamed to {newName}. \nSet your pet's name to 'reset' to reset their name. \nRemember, you will be held accountable for innapropriate names.";
                return true;
            }
            else
            {
                response = "You do not have a pet to rename.";
                return false;
            }
        }
    }
}
