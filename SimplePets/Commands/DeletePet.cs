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
    public class DeletePet : ICommand
    {
        public string Command => "dpet";

        public string[] Aliases => new[] { "deletepet" };

        public string Description => "Deletes your pet.";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.CheckPermission("sp.pet"))
            {
                response = "You do not have permission to use this command.";
                return false;
            }

            var player = Player.Get(sender);


            if (Plugin.PetDictionary.TryGetValue(player, out var dummy))
            {
                dummy.Destroy();
                Plugin.PetDictionary.Remove(player);

                Log.Debug("Deleted pet");

                response = "Your pet has been deleted.";
                return true;
            }
            else
            {
                response = "You do not have a pet to delete.";
                return false;
            }
        }
    }
}
