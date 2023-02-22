using BussinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IMemberRepository
    {
        public List<MemberObject> GetListAllMember(bool desc = false);
        public MemberObject getMemberByID(int id, List<MemberObject> list);
        public MemberObject getMemberByEmailAndPassword(string email, string password);
        public void InsertMember(MemberObject member);
        public void UpdateMember(MemberObject UpdateMember);
        public void DeleteMember(int id);
        public List<MemberObject> searchMemberByID(int id, List<MemberObject> list);
        public List<MemberObject> searchMemberByName(string name, List<MemberObject> list);
        public List<MemberObject> filterByCity(string city, List<MemberObject> list);
        public List<MemberObject> filterByCountry(string country, List<MemberObject> list);

    }
}
