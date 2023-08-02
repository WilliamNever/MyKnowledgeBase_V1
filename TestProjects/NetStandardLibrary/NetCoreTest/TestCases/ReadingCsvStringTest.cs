using DotNet6CoreLibrary.Helpers;
using NetCoreTest.BaseClassModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreTest.TestCases
{
    public class ReadingCsvStringTest : TestCaseBaseModel
    {
        public override async Task ExcuteAsync()
        {
            await ReadTestAsync();
            await base.ExcuteAsync();
        }

        private async Task ReadTestAsync()
        {
            string str = "\"\"\"1\"\"\",\"\"\",aa\"\",vv\",nn,ss,,\"\"\"fff\"\"\",\"\"\"\"\"\",3\r\n".Trim();
            string str1 = "0115,Clayton,GA,\"Relocation February 17, 2020\",Security Finance";
            string str2 = "5,6,7,8,";
            string str3 = "\"\"\"1\"\",\",\",\"\",fff\"\"\"";
            string str4 = "0115,Clayton,GA,\"Relocation February 17, 2020\",Security Finance,210 Highway 441 N Ste 101,30525,4262,PO Box 581,Clayton,30525,0015,(706) 782-4215,(706) 782-4216,www.securityfinance.com,\"$523 to $2,215\",\"$523 to $2,215\",securityfinance.eps,,T,R,G,In Front of Ingles and next to Verizon Wireless,\"February 17, 2020\",11,SFC,ga_0115@sfcbranches.com,Ed Cleveland,securityfinance.eps,,We Do Taxes,Ask about our Referral Program,SF 321-116.eps,Craig McDonough,3579 Atlanta Hwy,Athens,GA,30606,3152,3579 Atlanta Hwy,Athens,GA,30606,3152,,,(863)612-5600,craig.mcdonough@security-finance.com,SF 321-116 wo $$$.eps,Sonja Santoni,3801 Nameoki Rd Ste 15,Granite City,IL,62040,3764,3801 Nameoki Rd Ste 15,Granite City,IL,62040,3764,(618) 307-6272,(864) 237-6779,(636) 487-8082,sonja.santoni@security-finance.com,SF 321-116 wo $$$.eps,,,John Wessel ,123 Amberwood Dr,Anderson,SC,29621,3087,123 Amberwood Dr,Anderson,SC,29621,3087,,,(864) 316-2273,john.wessel@security-finance.com,SF 321-116 wo $$$.eps,,,,,,,,,,,,,,,,,K:\\Admin\\Postalsoft\\Advertising\\Print Ads\\2010 Logos\\Black Logos\\securityfinance.eps,K:\\Admin\\Postalsoft\\Advertising\\Print Ads\\2010 Logos\\Color Logos\\SF 321-116.eps,K:\\Admin\\Postalsoft\\Advertising\\Print Ads\\2010 Logos\\White Logos\\securityfinance_white.eps,,D,Debit card payments accepted,securityfinance_white.eps,,B,B,B,,Branch Manager 0115,ga_0115@sfcbranches.com,500,1000,1000,Sonja Santoni,sonja.santoni@security-finance.com,(706) 782-4215,Block Items only,,,,,,,Y,,Y,Y,*All loans are subject to credit limitations and our underwriting policies. Actual loan proceeds may vary based upon loan terms and any ancillary products selected. Dollar amounts are rounded to the lowest whole dollar and are subject to change. GA NMLS #2029421,*Todos los préstamos están sujetos a límites de crédito y a nuestras políticas de aprobación. El monto del préstamo a recibir puede variar en base a los términos del préstamo y a los productos complementarios seleccionados. Las cantidades en dólares están redondeadas al dólar entero inferior y están sujetas a cambio.,$25.00 ,,K:\\KRESS ____________________\\Bases\\Marketing Flyers Misc\\1422 Back To School Flyer FL15\\Links\\Tax_Free_Weekend_Tag_Color.eps,Tax_Free_Weekend_Tag_Color.eps,K:\\KRESS ____________________\\Bases\\New Logos 2005\\60th Anniversary\\60th Logo Final Version\\EPS\\color\\SF_60th_Logo_SecurityFinance.eps,SF_60th_Logo_SecurityFinance.eps,K:\\KRESS ____________________\\Bases\\New Logos 2005\\60th Anniversary\\60th Logo Final Version\\EPS\\bw\\SF_60th_Logo_BW_SecurityFinance.eps,SF_60th_Logo_BW_SecurityFinance.eps,1/1/70,011-0115-3000,\"$523 to $2,215\",Hacemos Declaraciones De Impuestos,Se aceptan pagos con tarjetas de débito,SF 321-116 wo $$$.eps,SF Black wo $$$.eps,Security Finance White wo $$$.eps,Security Finance White Horizontal TAX.eps,Security Finance White Stacked TAX.eps,Security Finance Black Horizontal TAX.eps,Security Finance Black Stacked TAX.eps,Security Finance 286 TAX.eps,Security Finance 321-116 Horizontal.eps,SF Black wo BOX.eps,SF 116 wo BOX.eps,SF White wo BOX.eps,SF 321 wo BOX.eps,No,SF 116 wo BOX white type.eps,SF 321-116 wo BOX.eps,9am - 6pm,9am - 5pm,9am - 5pm,9am - 5pm,9am - 6pm,N/A,Y,,,\"Security Finance of Georgia, LLC ? NMLS #2029421\",,GA NMLS# 2029421";
            string str5 = "\"\"\"\",\"\"\",\"";
            string str6 = "\"\"\"\",\",\"\"\"";
            string str7 = "1943,Dallas,TX,,Security Finance,2707 S Buckner Blvd,75227,6904,2707 S Buckner Blvd,Dallas,75227,6904,(469) 480-5889,(214) 275-2578,www.securityfinance.com,\"$463 to $1,456\",\"$463 to $1,456\",securityfinance.eps,,T,R,G,,\"November 12, 2018\",03,SFCTX,tx_1943@sfcbranches.com,Anthony Smith,securityfinance.eps,,We Do Taxes,Ask about our Referral Program,SF 321-116.eps,Frances Gomez,852 Secretary Dr,Arlington,TX,76015,1640,852 Secretary Dr,Arlington,TX,76015,1640,,,(214) 542-9659,frances.gomez@security-finance.com,SF 321-116 wo $$$.eps,Chris Carroll,980 N Walnut Creek Dr Ste 112,Mansfield,TX,76063,8020,909 Masquerade Dr,Mdlothian,TX,76065,8882,(817) 275-8400,(864) 237-6835,(817) 914-6354,chris.carroll@security-finance.com,SF 321-116 wo $$$.eps,,,Sheila Westendorf,1614 Merritt Park,San Antonio,TX,78253,6418,1614 Merritt Park,San Antonio,TX,78253,6418,(210) 525-8369,(864) 237-6922,(210) 383-9468,sheila.westendorf@security-finance.com,SF 321-116 wo $$$.eps,(864) 237-6344+CD805:CE805,,,,,,,,,,,,,,,,K:\\Admin\\Postalsoft\\Advertising\\Print Ads\\2010 Logos\\Black Logos\\securityfinance.eps,K:\\Admin\\Postalsoft\\Advertising\\Print Ads\\2010 Logos\\Color Logos\\SF 321-116.eps,K:\\Admin\\Postalsoft\\Advertising\\Print Ads\\2010 Logos\\White Logos\\securityfinance_white.eps,,D,Debit card payments accepted,securityfinance_white.eps,,B,R,R,,Branch Manager 1943,tx_1943@sfcbranches.com,500,1000,1000,Frances Gomez,frances.gomez@security-finance.com,(469) 480-5889,Block Items only,Y,,,,,,Y,,Y,,\"*All loans are subject to credit limitations and our underwriting policies, including verifiable ability to repay. Actual loan proceeds may vary based upon loan terms. Dollar amounts are rounded to the lowest whole dollar and are subject to change.\",\"*Todos los préstamos están sujetos a límites de crédito y a nuestras políticas de aprobación, incluyendo comprobación de capacidad de pago. El monto del préstamo a recibir puede variar en base a los términos del préstamo. Las cantidades en dólares están redondeadas al dólar entero inferior y están sujetas a cambio.\",$30.00 ,$10.00 ,,,,,,,11/12/18,003-1943-3000,\"$386 to $1,473\",Hacemos Declaraciones De Impuestos,Se aceptan pagos con tarjetas de débito,SF 321-116 wo $$$.eps,SF Black wo $$$.eps,Security Finance White wo $$$.eps,Security Finance White Horizontal TAX.eps,Security Finance White Stacked TAX.eps,Security Finance Black Horizontal TAX.eps,Security Finance Black Stacked TAX.eps,Security Finance 286 TAX.eps,Security Finance 321-116 Horizontal.eps,SF Black wo BOX.eps,SF 116 wo BOX.eps,SF White wo BOX.eps,SF 321 wo BOX.eps,No,SF 116 wo BOX white type.eps,SF 321-116 wo BOX.eps,8:30am - 5:30pm,8:30am - 5:30pm,8:30am - 5:30pm,8:30am - 5:30pm,8:30am - 5:30pm,N/A,Y,,,,,\r\n".Trim();
            foreach (var itm in new string[] { /*str, str1, str2, str3, str4, str5, str6*/ str7 })
            {
                var array = itm.ToCSVArray();
                string strResult = string.Join(",", array);

                var isEqual = strResult.Equals(itm);
            }
        }
    }
}
