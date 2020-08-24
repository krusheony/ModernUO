namespace Server.Items
{
    public class Heartseeker : CompositeBow
    {
        [Constructible]
        public Heartseeker()
        {
            LootType = LootType.Blessed;

            Attributes.AttackChance = 5;
            Attributes.WeaponSpeed = 10;
            Attributes.WeaponDamage = 25;
            WeaponAttributes.LowerStatReq = 70;
        }

        public Heartseeker(Serial serial) : base(serial)
        {
        }

        public override int LabelNumber => 1078210; // Heartseeker

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);

            writer.WriteEncodedInt(0); // version
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadEncodedInt();
        }
    }
}
