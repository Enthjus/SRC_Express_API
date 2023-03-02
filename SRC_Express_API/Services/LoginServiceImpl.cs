using SRC_Express_API.Models;
using SRC_Express_API.ModelsInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Web.Http.ModelBinding;
using System.Diagnostics;

namespace SRC_Express_API.Services
{
    public class LoginServiceImpl : LoginService
    {
        private DatabaseContext db;
        public LoginServiceImpl(DatabaseContext _db)
        {
            db = _db;
        }
        public AccountInfo Login(AccountInfo acc)
        {
            var user = db.Accounts.FirstOrDefault(a => a.Username.Equals(acc.Username) && a.Status == 1);
            if (user != null && BCrypt.Net.BCrypt.Verify(acc.Password, user.Password))
            {
                Debug.WriteLine("yyyy");
                return new AccountInfo
                {
                    Id = user.Id,
                    Username = user.Username,
                    FullName = user.Customers.SingleOrDefault(c => c.Idaccount.Equals(user.Id)).FullName,
                    Email = user.Customers.SingleOrDefault(c => c.Idaccount.Equals(user.Id)).Email,
                    Dob = user.Customers.SingleOrDefault(c => c.Idaccount.Equals(user.Id)).Dob,
                    Photo = user.Customers.SingleOrDefault(c => c.Idaccount.Equals(user.Id)).Photo,
                    NameRole = db.Roles.Find(user.Idrole).Name
                };
            }
            else
            {
                return null;
            }
        }

        public bool Register(AccountInfo accInfo)
        {
            Debug.WriteLine(accInfo.Username);
            var accRegister = db.Accounts.SingleOrDefault(a => a.Username.Equals(accInfo.Username));

            if (accInfo != null && accRegister == null)
            {
                Debug.WriteLine(accInfo.Id);
                var IDAcc = getTrueRandomID();
                var pass = BCrypt.Net.BCrypt.HashString(accInfo.Password);
                var code = RandomNumber(5);
                var idRole = db.Roles.SingleOrDefault(r => r.Name.Equals(accInfo.NameRole)).Id;
                var Newacc = new Account
                {
                    Id = IDAcc,
                    Username = accInfo.Username,
                    Password = pass,
                    Code = code,
                    ExpCode = DateTime.Now.AddMinutes(3),
                    Idrole = idRole,
                    Status = 0,
                };
                var Newcustomer = new Customer
                {
                    FullName = accInfo.FullName,
                    Email = accInfo.Email,
                    Dob = accInfo.Dob,
                    Idaccount = Newacc.Id,
                    Photo = accInfo.Photo
                };

                db.Accounts.Add(Newacc);
                db.Customers.Add(Newcustomer);
                db.SaveChanges();

                return true;
            }
            else
            {
                return false;
            }
        }

        public string getTrueRandomID()
        {
            var randomNumID = "SRC" + RandomNumber(6);
            var checkid = db.Accounts.SingleOrDefault(a => a.Id.Equals(randomNumID));
            if (checkid == null)
            {
                return randomNumID;
            }
            else
            {
                getTrueRandomID();
                return "";
            }
        }

        public static string RandomNumber(int numberRD)
        {
            string randomStr = "";
            try
            {

                int[] myIntArray = new int[numberRD];
                int x;
                //that is to create the random # and add it to uour string
                Random autoRand = new Random();
                for (x = 0; x < numberRD; x++)
                {
                    myIntArray[x] = System.Convert.ToInt32(autoRand.Next(0, 9));
                    randomStr += (myIntArray[x].ToString());
                }
            }
            catch (Exception ex)
            {
                randomStr = "error";
            }
            return randomStr;
        }



    }
}
