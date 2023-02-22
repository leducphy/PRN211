using BussinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class MemberRepository : IMemberRepository
    {
        void IMemberRepository.DeleteMember(int id) => MemberDAO.Instance.DeleteMember(id);


        List<MemberObject> IMemberRepository.filterByCity(string city, List<MemberObject> list) => MemberDAO.Instance.filterByCity(city, list);


        List<MemberObject> IMemberRepository.filterByCountry(string country, List<MemberObject> list) => MemberDAO.Instance.filterByCountry(country, list);


        List<MemberObject> IMemberRepository.GetListAllMember(bool desc = false) => MemberDAO.Instance.GetListAllMember(desc);
        

        MemberObject IMemberRepository.getMemberByEmailAndPassword(string email, string password) => MemberDAO.Instance.getMemberByEmailAndPassword(email, password);

        MemberObject IMemberRepository.getMemberByID(int id, List<MemberObject> list) => MemberDAO.Instance.getMemberByID(id, list);


        void IMemberRepository.InsertMember(MemberObject member) => MemberDAO.Instance.InsertMember(member);


        List<MemberObject> IMemberRepository.searchMemberByName(string name, List<MemberObject> list) => MemberDAO.Instance.searchMemberByName(name, list);

        List<MemberObject> IMemberRepository.searchMemberByID(int id, List<MemberObject> list) => MemberDAO.Instance.searchMemberByID(id , list);
        void IMemberRepository.UpdateMember(MemberObject UpdateMember) => MemberDAO.Instance.UpdateMember(UpdateMember);

    }
}
