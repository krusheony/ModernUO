namespace Server.Items
{
    [Flippable(0x2B79, 0x3170)]
    public class HideFemaleChest : BaseArmor
    {
        [Constructible]
        public HideFemaleChest() : base(0x2B79) => Weight = 6.0;

        public HideFemaleChest(Serial serial) : base(serial)
        {
        }

        public override Race RequiredRace => Race.Elf;

        public override int BasePhysicalResistance => 3;
        public override int BaseFireResistance => 3;
        public override int BaseColdResistance => 4;
        public override int BasePoisonResistance => 3;
        public override int BaseEnergyResistance => 2;

        public override int InitMinHits => 35;
        public override int InitMaxHits => 45;

        public override int AosStrReq => 35;
        public override int OldStrReq => 35;

        public override int ArmorBase => 15;

        public override ArmorMaterialType MaterialType => ArmorMaterialType.Studded;
        public override CraftResource DefaultResource => CraftResource.RegularLeather;

        public override ArmorMeditationAllowance DefMedAllowance => ArmorMeditationAllowance.Half;

        public override bool AllowMaleWearer => false;

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);

            writer.WriteEncodedInt(0);
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadEncodedInt();
        }
    }
}
