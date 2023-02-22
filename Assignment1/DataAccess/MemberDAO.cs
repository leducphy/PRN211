using BussinessObject;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;

namespace DataAccess
{
    public class MemberDAO
    {
        private static List<MemberObject> MemberList = new List<MemberObject>()
        {
            new MemberObject(1,"NYC phi le","concho", "123", "Hoa BInh","Viet Nam"),
            new MemberObject(2,"Phi Lê","leducphi", "123", "Ha Noi","Viet Nam"),
            new MemberObject(3,"Đức Anh","ducanh", "123", "LosA","US"),
            new MemberObject(4,"Vương Thành Công","cong", "123", "Qiaotou","US"),
            new MemberObject(5,"Nguyễn Tuấn Linh","linh", "123", "Anār","US"),
            new MemberObject(6,"Nguyễn Văn Dũng","dung", "123", "Ha Noi","Viet Nam"),
            new MemberObject(7,"Phạm Thị Ngọc Mai","mai", "123", "Kapsanŭp","US"),
            new MemberObject(8,"Karlene Humburton","khumburton7@cbsnews.com", "123", "Kapsanŭp","China"),
            new MemberObject(9,"Barr Lanceley","blanceley8@xinhuanet.com", "123", "Kapsanŭp","Russia"),
            new MemberObject(10,"Nicko Szymanek","nszymanek9@behance.net", "123", "Fulin","China"),
        };
        private static MemberDAO instance = null;
        private static readonly object insctanceLock = new object();
        private MemberDAO() {
            IConfigurationRoot configurationRoot = new ConfigurationBuilder().AddJsonFile("appsetting.json").Build();
            string adminEmail = configurationRoot["admin:email"];
            string adminPassword = configurationRoot["admin:password"];
            MemberList.Add(new MemberObject(11, "Admin", adminEmail, adminPassword, "Ha Noi", "Viet Nam"));
        }

        public static MemberDAO Instance
        {
            get
            {
                lock (insctanceLock)
                {
                    if (instance == null)
                    {
                        instance = new MemberDAO();
                    }
                    return instance;
                }
            }
        }

        public List<MemberObject> GetListAllMember(bool desc = false)
        {
            if(desc)
            {
               return MemberList.OrderByDescending(mem => mem.MemberName).ToList();
            }
            else
            {
                return MemberList.OrderBy(mem => mem.MemberName).ToList();
            }
        }

        public MemberObject getMemberByID(int id, List<MemberObject> list)
        {
            list ??= MemberList;
            return MemberList.SingleOrDefault(member => member.MemberID== id);
        }

        public MemberObject getMemberByEmailAndPassword(string email, string password)
        {
            return MemberList.SingleOrDefault
                (
                member => member.Email.Equals(email) && member.Password.Equals(password)
                );
        }

        public void InsertMember(MemberObject member)
        {
            if(getMemberByID(member.MemberID, null) != null)
            {
                throw new Exception("Member already exists");
            }
            MemberList.Add(member);
        }

        public void UpdateMember(MemberObject UpdateMember)
        {
            MemberObject member = getMemberByID(UpdateMember.MemberID, null);
            if( member == null)
            {
                throw new Exception("Member does not elready exists");
            }
            member.MemberName = UpdateMember.MemberName;
            member.Email = UpdateMember.Email;
            member.Password = UpdateMember.Password;
            member.City = UpdateMember.City;
            member.Country = UpdateMember.Country;

        }

        public void DeleteMember(int id)
        {
            MemberObject member = getMemberByID(id, null);
            if (member == null)
            {
                throw new Exception("Member does not elready exists");
            }
            MemberList.Remove(member);
        }

        

        public List<MemberObject> searchMemberByID(int id, List<MemberObject> list)
        {
            list ??= MemberList;
            return MemberList.Where(member => member.MemberID.ToString().Contains(id.ToString())).ToList();
        }

        public List<MemberObject> searchMemberByName(string name, List<MemberObject> list)
        {
            list ??= MemberList;
            return MemberList.Where(member => member.MemberName.ToLower().Contains(name.ToLower())).ToList();
        }

        public List<MemberObject> filterByCity(string city, List<MemberObject> list)
        {
            list ??= MemberList;
            return MemberList.Where(member => member.City.Equals(city)).ToList();
        }

        public List<MemberObject> filterByCountry(string country, List<MemberObject> list)
        {
            list ??= MemberList;
            return MemberList.Where(member => member.Country.Equals(country)).ToList();
        }

        
    }
}
