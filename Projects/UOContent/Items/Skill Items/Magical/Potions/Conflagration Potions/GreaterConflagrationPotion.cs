namespace Server.Items
{
    public class GreaterConflagrationPotion : BaseConflagrationPotion
    {
        [Constructible]
        public GreaterConflagrationPotion() : base(PotionEffect.ConflagrationGreater)
        {
        }

        public GreaterConflagrationPotion(Serial serial) : base(serial)
        {
        }

        public override int MinDamage => 4;
        public override int MaxDamage => 8;

        public override int LabelNumber => 1072098; // a Greater Conflagration potion

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write(0); // version
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);

            var version = reader.ReadInt();
        }
    }
}
