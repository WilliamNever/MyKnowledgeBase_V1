using StandardLibrary.SecurityCryptography;

namespace Net6Test.TestGroups
{
    public class SecurityCryptography_Tests
    {
        public static void ToHashSha256_Test()
        {
            string str = "111111";
            var enc = str.ToHashSha256();
            string str1 = "222222";
            var enc1 = str1.ToHashSha256();
        }
    }
}
