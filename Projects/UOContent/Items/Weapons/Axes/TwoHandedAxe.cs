namespace Server.Items
{
    [Flippable(0x1443, 0x1442)]
    public class TwoHandedAxe : BaseAxe
    {
        [Constructible]
        public TwoHandedAxe() : base(0x1443) => Weight = 8.0;

        public TwoHandedAxe(Serial serial) : base(serial)
        {
        }

        public override WeaponAbility PrimaryAbility => WeaponAbility.DoubleStrike;
        public override WeaponAbility SecondaryAbility => WeaponAbility.ShadowStrike;

        public override int AosStrengthReq => 40;
        public override int AosMinDamage => 16;
        public override int AosMaxDamage => 17;
        public override int AosSpeed => 31;
        public override float MlSpeed => 3.50f;

        public override int OldStrengthReq => 35;
        public override int OldMinDamage => 5;
        public override int OldMaxDamage => 39;
        public override int OldSpeed => 30;

        public override int InitMinHits => 31;
        public override int InitMaxHits => 90;

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write(0); // version
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
}
