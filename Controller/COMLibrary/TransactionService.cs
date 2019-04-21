using COMUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMLibrary
{
    public class TransactionService
    {
        private DBService db = DBService.getInstance();
        private TransactionService() { }
        private static TransactionService _instance;
        public static TransactionService getInstance()
        {
            if (_instance == null)
            {
                _instance = new TransactionService();
            }
            return _instance;
        }

        
    }
}
