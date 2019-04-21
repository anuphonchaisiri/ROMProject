using COMLibrary.Model.Company;
using COMUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMLibrary
{
    public class CompanyService
    {
        private DBService db = DBService.getInstance();
        private static CompanyService _instance;
        public static CompanyService getInstance()
        {
            if (_instance == null)
            {
                _instance = new CompanyService();
            }
            return _instance;
        }

        public List<CompanyInfoModel> searchCompamyInfo(string userid, string sid, string companycode)
        {
            string sql = @"
                SELECT a.sid,a.name,a.description,a.remark
                ,a.company_code,a.created_by,a.created_on,a.updated_by,a.updated_on
                FROM COM_MASTER_COMPANY a with (nolock)
                inner join COM_MASTER_EMPLOYEE_MAPPING b with (nolock) on a.sid = b.sid
                where a.active = 1  and b.active = 1 and b.userid = '" + userid + @"' ";
            List<CompanyInfoModel> listCom = db.selectCenterData<CompanyInfoModel>(sql);
            return listCom;
        }

        public List<CompanyInfoModel> searchCompamyInfoRefSystem(string sid,string sidname)
        {
            string sql = @"
                SELECT a.sid,a.name,a.description,a.remark
                ,a.sid,a.company_code,a.created_by,a.created_on,a.updated_by,a.updated_on
                FROM COM_MASTER_COMPANY a with (nolock)
                where a.active = 1 ";
            if (!string.IsNullOrEmpty(sid))
            {
                sql += " and a.sid = '" + sid + @"' ";
            }

            if (!string.IsNullOrEmpty(sidname))
            {
                sql += " and a.name = '" + sidname + @"' ";
            }
            List<CompanyInfoModel> listCom = db.selectCenterData<CompanyInfoModel>(sql);
            return listCom;
        }
    }
}
