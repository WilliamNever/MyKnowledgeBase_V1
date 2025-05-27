using Net6Test.AttributesModels;
using Net6Test.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net6Test.Models
{
    public class ExtClassModel : Base2
    {
        public ExtClassModel():base()
        {
            
        }
        public string? HiFiInfor { get; set; }
        public string Name { get; set; } = "ExtClassModel";
    }

    public class BaseOX
    {
        [CustomInList]
        public string BaseOX_Name { get; set; }
        [CustomInList(false)]
        public string? BaseOX_HiFiInfor { get; set; }
        public string? OutterInfor { get; set; }
        public virtual int Rec { get; set; }
        public List<Base1> b1List { get; set; }
        public Tuple<string, string> Union { get; set; }

        public static BaseOX operator +(BaseOX ah, BaseOX ls)
        {
            var union = new Tuple<string, string>(ah.BaseOX_Name, ls.BaseOX_Name);
            ah.Union = union;
            ls.Union = union;
            return new BaseOX() { Union = union };
        }
        public static implicit operator string(BaseOX bx)
        {
            return $"{bx.Union.Item1}@@{bx.Union.Item2}";
        }
        public override string ToString()
        {
            return (string)this;
        }
        public static implicit operator BaseOXSep1(BaseOX bx)
        {
            var bs = new BaseOXSep1();
            bs.BaseOXSep1_Name = bx.BaseOX_Name;
            bs.BaseOXSep1_HiFiInfor = bx.BaseOX_HiFiInfor;
            bs.Rec = bx.Rec;
            return bs;
        }
        //public static explicit operator BaseOXSep1(BaseOX bx)
        //{
        //    var bs = new BaseOXSep1();
        //    bs.BaseOXSep1_Name = bx.BaseOX_Name;
        //    bs.BaseOXSep1_HiFiInfor = bx.BaseOX_HiFiInfor;
        //    bs.Rec = bx.Rec;
        //    return bs;
        //}
    }
    public class BaseOXSep1
    {
        public string BaseOXSep1_Name { get; set; }
        public string? BaseOXSep1_HiFiInfor { get; set; }
        public virtual int Rec { get; set; }
        public List<Base1> b1List { get; set; }
    }
    public class Base0
    {
        public Guid id { get; set; } = Guid.NewGuid();
        public string Base0_Name { get; set; }
        public string? Base0_HiFiInfor { get; set; }
        public virtual int Rec { get; set; }
        public List<Base1> b1List { get; set; }

        public virtual void ShowId(string str)
        {
            Console.WriteLine($"Base0 - {id} - {str}");
        }
    }

    public class Base1 : Base0
    {
        public int Acg { get; set; }
        public string Acgx { get; set; }
        public List<Base2> b2List { get; set; }
        public override void ShowId(string str)
        {
            base.ShowId($"Base1 - {Acg} - {str}");
        }
    }

    public class Base2 : Base1
    {
        public Base2()
        {
            
        }
        public override int Rec { get; set; }
        public int Acx { get; set; }
    }

    public class Bxx<T> where T : Base0
    {
        public T T_Field { get; set; }
    }
    public class Bxx1 : Bxx<Base1>, INew<Bxx1>
    {
        public Bxx1()
        {

        }
        public Bxx1(string str)
        {
            T_Field.Base0_Name = str;
        }

        public Bxx1 Create(string str)
        {
            return new Bxx1(str);
        }

        public class UserException : Exception, INew<UserException>, IDotTests
        {
            public Guid CID { get; set; } = Guid.NewGuid();
            public UserException()
            {

            }
            public UserException(string str) : base(str)
            {

            }

            public UserException Create(string str)
            {
                return new UserException(str);
            }

            int IDotTests.GetId(string num)
            {
                return 32;
            }

            string IDotTests.GetIdx(int num)
            {
                return num.ToString();
            }
        }
    }
}

