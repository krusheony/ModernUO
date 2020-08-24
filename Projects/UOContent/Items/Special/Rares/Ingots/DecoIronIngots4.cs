namespace Server.Items
{
    public class DecoIronIngots4 : Item
    {
        [Constructible]
        public DecoIronIngots4() : base(0x1BF1)
        {
            Movable = true;
            Stackable = false;
        }

        public DecoIronIngots4(Serial serial) : base(serial)
        {
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write(0);
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
}
