using CerberusFramework.Utilities;

namespace CFGameClient
{
    public record GameSoundKeys : Enumeration<GameSoundKeys>
    {
        protected GameSoundKeys(int id, string name) : base(id, name)
        {
        }
    }
}