using System.IO.Compression;

namespace Net6Test.TestGroups
{
    public class CompressionEncrypt
    {
        public static string OriXml =
        #region MyRegion
            @"
<?xml version=""1.0"" encoding=""utf-16""?>
<SpeedTransaction xmlns=""Taylor.Speed"">
  <Routing>
    <SenderId>156</SenderId>
    <ReceiverId>MINTED</ReceiverId>
    <DocumentType>MINTEDUPDATE</DocumentType>
    <DocumentId>19760519</DocumentId>
    <ReferenceId>19760519</ReferenceId>
    <Environment>Development</Environment>
    <ConversationId>b57083f0-377d-4ede-adc2-d2d8a22169a0</ConversationId>
    <SenderParty>TOG</SenderParty>
    <TimestampUTC>2024-11-10T19:22:31.9046036Z</TimestampUTC>
  </Routing>
  <Message>
    <TradingPartnerMessage>
      <ShipmentUpdate>
        <Order>
          <OrderIsComplete>True</OrderIsComplete>
          <CompleteDate>2025-08-04T19:00:50-05:00</CompleteDate>
          <ExpectedShipDate>2025-08-04T18:00:00-05:00</ExpectedShipDate>
          <ShippingCode>FEDEX_HOME</ShippingCode>
          <TrackingNumber>445486401560</TrackingNumber>
          <Contents>
            <Content>
              <BundleId>174528394</BundleId>
              <ItemId>71057037</ItemId>
              <Quantity>25</Quantity>
            </Content>
            <Content>
              <BundleId>174528394</BundleId>
              <ItemId>71057040</ItemId>
              <Quantity>26</Quantity>
            </Content>
            <Content>
              <BundleId>174528394</BundleId>
              <ItemId>71057038</ItemId>
              <Quantity>25</Quantity>
            </Content>
            <Content>
              <BundleId>174528416</BundleId>
              <ItemId>71057041</ItemId>
              <Quantity>25</Quantity>
            </Content>
            <Content>
              <BundleId>174528416</BundleId>
              <ItemId>71057044</ItemId>
              <Quantity>26</Quantity>
            </Content>
            <Content>
              <BundleId>174528416</BundleId>
              <ItemId>71057042</ItemId>
              <Quantity>25</Quantity>
            </Content>
            <Content>
              <BundleId>174528240</BundleId>
              <ItemId>71057036</ItemId>
              <Quantity>1</Quantity>
            </Content>
          </Contents>
        </Order>

      </ShipmentUpdate>
    </TradingPartnerMessage>
  </Message>
</SpeedTransaction>
";
        #endregion
        public async static Task CETest1Async()
        {
            using MemoryStream msr = new MemoryStream();
            using StreamWriter sw = new StreamWriter(msr);
            sw.Write(OriXml);
            sw.Flush();

            using MemoryStream ms = new MemoryStream();
            using DeflateStream cs = new DeflateStream(ms, CompressionMode.Compress);
            cs.Write(msr.ToArray());
            cs.Flush();
            var str = Convert.ToBase64String(ms.ToArray());
        }
    }
}
