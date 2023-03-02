using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SRC_Express_API.Models;
using SRC_Express_API.ModelsInfo;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SRC_Express_API.Services
{

    public class AccountServiceImpl : AccountService
    {
        private DatabaseContext db;
        private IWebHostEnvironment webHostEnvironment;
        private IHttpContextAccessor httpContextAccessor;
        public AccountServiceImpl(DatabaseContext _db, IWebHostEnvironment _webHostEnvironment, IHttpContextAccessor _httpContextAccessor)
        {
            db = _db;
            webHostEnvironment = _webHostEnvironment;
            httpContextAccessor = _httpContextAccessor;
        }


        public List<AccountInfo> FindAllAccountByAdmin()
        {
            return db.Accounts.Where(a => a.IdroleNavigation.Name.Equals("Admin") || a.IdroleNavigation.Name.Equals("Employee") || a.IdroleNavigation.Name.Equals("Customer")).Select(a => new AccountInfo
            {
                Id = a.Id,
                Username = a.Username,
                FullName = a.Customers.SingleOrDefault(c => c.Idaccount.Equals(a.Id)).FullName,
                Email = a.Customers.SingleOrDefault(c => c.Idaccount.Equals(a.Id)).Email,
                Dob = a.Customers.SingleOrDefault(c => c.Idaccount.Equals(a.Id)).Dob,
                Photo = a.Customers.SingleOrDefault(c => c.Idaccount.Equals(a.Id)).Photo,
                NameRole = a.IdroleNavigation.Name
            }).ToList();
        }

        public List<AccountInfo> FindAllAccountBySA()
        {
            return db.Accounts.Select(a => new AccountInfo
            {
                Id = a.Id,
                Username = a.Username,
                FullName = a.Customers.SingleOrDefault(c => c.Idaccount.Equals(a.Id)).FullName,
                Email = a.Customers.SingleOrDefault(c => c.Idaccount.Equals(a.Id)).Email,
                Dob = a.Customers.SingleOrDefault(c => c.Idaccount.Equals(a.Id)).Dob,
                Photo = a.Customers.SingleOrDefault(c => c.Idaccount.Equals(a.Id)).Photo,
                NameRole = a.IdroleNavigation.Name
            }).ToList();
        }

        public AccountInfo FindIDAccountBySA(string idacc)
        {
            var acc = db.Accounts.SingleOrDefault(a => a.Id.Equals(idacc));
            return new AccountInfo
            {
                Id = acc.Id,
                Username = acc.Username,
                FullName = acc.Customers.SingleOrDefault(c => c.Idaccount.Equals(acc.Id)).FullName,
                Email = acc.Customers.SingleOrDefault(c => c.Idaccount.Equals(acc.Id)).Email,
                Dob = acc.Customers.SingleOrDefault(c => c.Idaccount.Equals(acc.Id)).Dob,
                Photo = acc.Customers.SingleOrDefault(c => c.Idaccount.Equals(acc.Id)).Photo,
                NameRole = acc.IdroleNavigation.Name
            };
        }
        public Boolean UpdateCustomer(Customer cus)
        {
            Debug.WriteLine("aa");
            Debug.WriteLine(cus.Id);
            Debug.WriteLine(cus.FullName);
            Debug.WriteLine(cus.Email);
            Debug.WriteLine(cus.Dob);
            Debug.WriteLine(cus.Idaccount);
            Debug.WriteLine(cus.Photo);
            db.Entry(cus).State = EntityState.Modified;
            db.SaveChanges();
            return true;
        }
        public Boolean UpdateAccount(Account acc)
        {

            db.Entry(acc).State = EntityState.Modified;
            db.SaveChanges();
            return true;
        }
        public AccountInfo UpdateAccount(AccountInfo accInfo)
        {
            Debug.WriteLine(accInfo.Id);
            var accRegister = db.Accounts.SingleOrDefault(a => a.Username.Equals(accInfo.Username));

            if (accInfo != null)
            {
                var pass = BCrypt.Net.BCrypt.HashString(accInfo.Password);
                var idRole = db.Roles.SingleOrDefault(r => r.Name.Equals(accInfo.NameRole)).Id;

                var Newacc = db.Accounts.SingleOrDefault(a => a.Id.Equals(accInfo.Id));

                Newacc.Username = accInfo.Username;
                Newacc.Password = pass;
                Newacc.Code = "non";
                Newacc.ExpCode = DateTime.Now.AddMinutes(3);
                Newacc.Idrole = idRole;
                Newacc.Status = 1;


                var customer = db.Customers.SingleOrDefault(c => c.Idaccount.Equals(Newacc.Id));

                customer.FullName = accInfo.FullName;
                customer.Email = accInfo.Email;
                customer.Dob = accInfo.Dob;
                customer.Photo = accInfo.Photo;

                UpdateAccount(Newacc);
                UpdateCustomer(customer);

                return new AccountInfo
                {
                    Id = Newacc.Id,
                    Username = Newacc.Username,
                    FullName = Newacc.Customers.SingleOrDefault(c => c.Idaccount.Equals(Newacc.Id)).FullName,
                    Email = Newacc.Customers.SingleOrDefault(c => c.Idaccount.Equals(Newacc.Id)).Email,
                    Dob = Newacc.Customers.SingleOrDefault(c => c.Idaccount.Equals(Newacc.Id)).Dob,
                    Photo = Newacc.Customers.SingleOrDefault(c => c.Idaccount.Equals(Newacc.Id)).Photo,
                    NameRole = Newacc.IdroleNavigation.Name
                };


            }
            else
            {
                return null;
            }
        }

        public bool DeleteAccount(string idacc)
        {
            try
            {
                Debug.WriteLine(idacc);
                db.Customers.Remove(db.Customers.SingleOrDefault(c => c.Idaccount.Equals(idacc)));
                db.SaveChanges();
                db.Accounts.Remove(db.Accounts.Find(idacc));
                return db.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }
        }

        public bool DeletePhoto(string idacc)
        {
            try
            {
                var tripdelete = db.Customers.SingleOrDefault(t => t.Idaccount.Equals(idacc));
                if (tripdelete.Photo.Equals("http://localhost:57771/uploads/baal.jpg"))
                {
                    return true;
                }
                else
                {
                    var photodelete = tripdelete.Photo.Replace("http://localhost:57771/uploads/", "");
                    var path = Path.Combine(webHostEnvironment.WebRootPath, "uploads", photodelete);
                    if (System.IO.File.Exists(path))
                    {
                        System.IO.File.Delete(path);
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

            }
            catch
            {
                return false;
            }
        }
    }
}
