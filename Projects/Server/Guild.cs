using System;
using System.Collections.Generic;
using System.Linq;

namespace Server.Guilds
{
    public enum GuildType
    {
        Regular,
        Chaos,
        Order
    }

    public abstract class BaseGuild : ISerializable
    {
        protected BaseGuild(Serial serial)
        {
            Serial = serial;
            World.AddGuild(this);
        }

        protected BaseGuild()
        {
            Serial = World.NewGuild;
            World.AddGuild(this);
        }

        public abstract string Abbreviation { get; set; }
        public abstract string Name { get; set; }
        public abstract GuildType Type { get; set; }
        public abstract bool Disbanded { get; }
        public abstract void Delete();

        public BufferWriter SaveBuffer { get; set; }

        [CommandProperty(AccessLevel.Counselor)]
        public Serial Serial { get; }

        public int TypeRef => 0;

        public void Serialize()
        {
            SaveBuffer ??= new BufferWriter(true);
            SaveBuffer.Reset();
            Serialize(SaveBuffer);
        }

        public abstract void Serialize(IGenericWriter writer);
        public abstract void Deserialize(IGenericReader reader);
        public abstract void OnDelete(Mobile mob);

        public static BaseGuild FindByName(string name) =>
            World.Guilds.Values.FirstOrDefault(g => g.Name == name);

        public static BaseGuild FindByAbbrev(string abbr) =>
            World.Guilds.Values.FirstOrDefault(g => g.Abbreviation == abbr);

        public static List<BaseGuild> Search(string find)
        {
            var words = find.ToLower().Split(' ');
            var results = new List<BaseGuild>();

            foreach (var g in World.Guilds.Values)
            {
                var name = g.Name.ToLower();

                if (words.All(t => name.IndexOf(t, StringComparison.Ordinal) != -1))
                {
                    results.Add(g);
                }
            }

            return results;
        }

        public override string ToString() => $"0x{Serial.Value:X} \"{Name} [{Abbreviation}]\"";
    }
}
