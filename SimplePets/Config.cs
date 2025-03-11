using System.ComponentModel;
using Exiled.API.Features.Core.Generic;
using Exiled.API.Interfaces;
using PlayerRoles.FirstPersonControl.Thirdperson.Subcontrollers;

namespace SimplePets
{
    public class Config : IConfig
    {
        [Description("Is the plugin enabled?")]
        public bool IsEnabled { get; set; } = true;

        [Description("Should pets be godded?")]
        public bool GodPets { get; set; } = true;

        [Description("What emotion should pets use? (Happy, AwkwardSmile, Scared, Angry, Chad, Ogre, Neutral)")]
        public EmotionPresetType PetEmotion { get; set; } = EmotionPresetType.Happy;

        [Description("Should pets be despawned after death?")]
        public bool DespawnPetsAfterDeath { get; set; } = true;

        [Description("Should debug messages be printed in console?")]
        public bool Debug { get; set; } = false;
    }
}