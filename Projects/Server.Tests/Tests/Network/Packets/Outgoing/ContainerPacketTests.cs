using System;
using Server.Items;
using Server.Network;
using Xunit;

namespace Server.Tests.Network
{
    [Collection("Sequential Tests")]
    public class ContainerPacketTests : IClassFixture<ServerFixture>
    {

        [Fact]
        public void TestContainerDisplay()
        {
            Serial serial = 0x1000;
            ushort gumpId = 100;

            var expected = new ContainerDisplay(serial, gumpId).Compile();

            using var ns = PacketTestUtilities.CreateTestNetState();
            ns.SendDisplayContainer(serial, gumpId);

            var result = ns.SendPipe.Reader.TryRead();
            AssertThat.Equal(result.Buffer[0].AsSpan(0), expected);
        }

        [Fact]
        public void TestContainerDisplayHS()
        {
            Serial serial = 0x1000;
            ushort gumpId = 100;

            var expected = new ContainerDisplayHS(serial, gumpId).Compile();

            using var ns = PacketTestUtilities.CreateTestNetState();
            ns.ProtocolChanges = ns.ProtocolChanges | ProtocolChanges.ContainerGridLines | ProtocolChanges.HighSeas;
            ns.SendDisplayContainer(serial, gumpId);

            var result = ns.SendPipe.Reader.TryRead();
            AssertThat.Equal(result.Buffer[0].AsSpan(0), expected);
        }

        [Fact]
        public void TestDisplaySpellbook()
        {
            Serial serial = 0x1000;

            var expected = new DisplaySpellbook(serial).Compile();

            using var ns = PacketTestUtilities.CreateTestNetState();
            ns.SendDisplaySpellbook(serial);

            var result = ns.SendPipe.Reader.TryRead();
            AssertThat.Equal(result.Buffer[0].AsSpan(0), expected);
        }

        [Fact]
        public void TestDisplaySpellbookHS()
        {
            Serial serial = 0x1000;

            var expected = new DisplaySpellbookHS(serial).Compile();

            using var ns = PacketTestUtilities.CreateTestNetState();
            ns.ProtocolChanges = ns.ProtocolChanges | ProtocolChanges.ContainerGridLines | ProtocolChanges.HighSeas;
            ns.SendDisplaySpellbook(serial);

            var result = ns.SendPipe.Reader.TryRead();
            AssertThat.Equal(result.Buffer[0].AsSpan(0), expected);
        }

        [Fact]
        public void TestNewSpellbookContent()
        {
            Serial serial = 0x1000;
            ushort graphic = 100;
            ushort offset = 10;
            ulong content = 0x123456789ABCDEF0;
            bool opl = ObjectPropertyList.Enabled;

            var expected = new NewSpellbookContent(serial, graphic, offset, content).Compile();

            using var ns = PacketTestUtilities.CreateTestNetState();
            ns.ProtocolChanges = ns.ProtocolChanges | ProtocolChanges.ContainerGridLines | ProtocolChanges.NewSpellbook;
            ObjectPropertyList.Enabled = true;
            ns.SendSpellbookContent(serial, graphic, offset, content);
            ObjectPropertyList.Enabled = opl;

            var result = ns.SendPipe.Reader.TryRead();
            AssertThat.Equal(result.Buffer[0].AsSpan(0), expected);
        }

        [Fact]
        public void TestSpellbookContent()
        {
            Serial serial = 0x1000;
            ushort offset = 10;
            ushort graphic = 100;
            ulong content = 0x123456789ABCDEF0;

            var expected = new SpellbookContent(serial, offset, content).Compile();

            using var ns = PacketTestUtilities.CreateTestNetState();
            ns.SendSpellbookContent(serial, graphic, offset, content);

            var result = ns.SendPipe.Reader.TryRead();
            AssertThat.Equal(result.Buffer[0].AsSpan(0), expected);
        }

        [Fact]
        public void TestSpellbookContent6017()
        {
            Serial serial = 0x1000;
            ushort offset = 10;
            ushort graphic = 100;
            ulong content = 0x123456789ABCDEF0;

            var expected = new SpellbookContent6017(serial, offset, content).Compile();

            using var ns = PacketTestUtilities.CreateTestNetState();
            ns.ProtocolChanges |= ProtocolChanges.ContainerGridLines;
            ns.SendSpellbookContent(serial, graphic, offset, content);

            var result = ns.SendPipe.Reader.TryRead();
            AssertThat.Equal(result.Buffer[0].AsSpan(0), expected);
        }

        [Fact]
        public void TestContainerContentUpdate()
        {
            Serial serial = 0x1;
            var item = new Item(serial);

            var expected = new ContainerContentUpdate(item).Compile();

            using var ns = PacketTestUtilities.CreateTestNetState();
            ns.SendContainerContentUpdate(item);

            var result = ns.SendPipe.Reader.TryRead();
            AssertThat.Equal(result.Buffer[0].AsSpan(0), expected);
        }

        [Fact]
        public void TestContainerContentUpdate6017()
        {
            Serial serial = 0x1;
            var item = new Item(serial);

            var expected = new ContainerContentUpdate6017(item).Compile();

            using var ns = PacketTestUtilities.CreateTestNetState();
            ns.ProtocolChanges |= ProtocolChanges.ContainerGridLines;
            ns.SendContainerContentUpdate(item);

            var result = ns.SendPipe.Reader.TryRead();
            AssertThat.Equal(result.Buffer[0].AsSpan(0), expected);
        }

        [Fact]
        public void TestContainerContent()
        {
            var cont = new Container(World.NewItem);
            cont.AddItem(new Item(World.NewItem));

            var m = new Mobile(0x1);
            m.DefaultMobileInit();

            var expected = new ContainerContent(m, cont).Compile();

            using var ns = PacketTestUtilities.CreateTestNetState();
            ns.SendContainerContent(m, cont);

            var result = ns.SendPipe.Reader.TryRead();
            AssertThat.Equal(result.Buffer[0].AsSpan(0), expected);
        }

        [Fact]
        public void TestContainerContent6017()
        {
            var cont = new Container(World.NewItem);
            cont.AddItem(new Item(World.NewItem));

            var m = new Mobile(0x1);
            m.DefaultMobileInit();

            var expected = new ContainerContent6017(m, cont).Compile();

            using var ns = PacketTestUtilities.CreateTestNetState();
            ns.ProtocolChanges |= ProtocolChanges.ContainerGridLines;
            ns.SendContainerContent(m, cont);

            var result = ns.SendPipe.Reader.TryRead();
            AssertThat.Equal(result.Buffer[0].AsSpan(0), expected);
        }
    }
}
