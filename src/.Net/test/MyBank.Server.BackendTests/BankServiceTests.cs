using MyBank.Interfaces;
using MyBank.Nameservice;
using MyBank.Nameservice.Exceptions;
using MyBank.Server.Backend;
using MyBank.Server.Backend.Model;
using MyBank.Server.Backend.Repository;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity;

namespace MyBank.Server.BackendTests
{
    [TestFixture]
    public class BankServiceTests
    {
        UnityContainer Container = new UnityContainer();
        UserRepository UserRepository;
        AccountRepository AccountRepository;
        BankService BankService;

        [SetUp]
        public void Setup()
        {
            Container = new UnityContainer();
            ServiceRegistry.Register(Container);
            UserRepository = Container.Resolve<UserRepository>();
            AccountRepository = Container.Resolve<AccountRepository>();
            BankService = Container.Resolve<BankService>();
        }


        [Test]
        public void User_Can_Login()
        {
            var user = new User()
            {
                Username = "a",
                Password = "a",
                Privilege = Privileges.User,

            };

            UserRepository.Entities.TryAdd(user.GetMappingKey(), user);

            Assert.DoesNotThrow(() =>
            {
                var token = BankService.Login(user.Username, user.Password);
                BankService.Bye(token);
            });
        }

        [Test]
        public void Session_Gets_Innvalidated_When_User_Logsin_Again()
        {
            var user = new User()
            {
                Username = "a",
                Password = "a",
                Privilege = Privileges.User,

            };

            UserRepository.Entities.TryAdd(user.GetMappingKey(), user);


            var oldtoken = BankService.Login(user.Username, user.Password);
            var newToken = BankService.Login(user.Username, user.Password);

            Assert.Multiple(() =>
            {
                Assert.DoesNotThrow(() =>
                {
                    BankService.ListAccounts(newToken);
                });
                Assert.Throws<AuthenticationException>(() =>
                {
                    BankService.ListAccounts(oldtoken);
                });
            });

        }

        [Test]
        public void Functions_Can_Only_Be_Accessed_By_Authenticated_User()
        {
            var RandomToken = $"{Guid.NewGuid().ToString("N")}{((int)Privileges.User)}";
            Assert.Throws<AuthenticationException>(() =>
            {
                BankService.ListAccounts(RandomToken);
            });
        }

        [Test]
        public void User_Cant_Access_Admin_Functions()
        {
            var user = new User()
            {
                Username = "a",
                Password = "a",
                Privilege = Privileges.User,

            };

            UserRepository.Entities.TryAdd(user.GetMappingKey(), user);

            Assert.Throws<AuthenticationException>(() =>
            {
                var token = BankService.Login(user.Username, user.Password);
                BankService.NewUser(token, "a", "a");
                BankService.Bye(token);
            });
        }


        [Test]
        public void Admin_Can_Access_User_Functions()
        {
            var user = new User()
            {
                Username = "a",
                Password = "a",
                Privilege = Privileges.Admin,

            };

            UserRepository.Entities.TryAdd(user.GetMappingKey(), user);

            Assert.DoesNotThrow(() =>
            {
                var token = BankService.Login(user.Username, user.Password);
                BankService.ListAccounts(token);
                BankService.Bye(token);
            });
        }

        [Test]
        public void Admin_Can_Create_User()
        {
            var admin = new User()
            {
                Username = "admin",
                Password = "1234",
                Privilege = Privileges.Admin,

            };

            UserRepository.Entities.TryAdd(admin.GetMappingKey(), admin);

            var token = BankService.Login(admin.Username, admin.Password);
            BankService.NewUser(token, "a", "a");
            BankService.Bye(token);

            Assert.IsTrue(UserRepository.Entities.ContainsKey("a"));

        }



        [Test]
        public void Create_User_Throws_When_User_Already_Exists()
        {
            var admin = new User()
            {
                Username = "admin",
                Password = "1234",
                Privilege = Privileges.Admin,

            };

            UserRepository.Entities.TryAdd(admin.GetMappingKey(), admin);

            Assert.Throws<ArgumentException>(() =>
            {
                var token = BankService.Login(admin.Username, admin.Password);
                BankService.NewUser(token, "admin", "a");
                BankService.Bye(token);
            });
        }

        [Test]
        public void Admin_Can_Create_Accont()
        {
            var admin = new User()
            {
                Username = "admin",
                Password = "1234",
                Privilege = Privileges.Admin,

            };

            UserRepository.Entities.TryAdd(admin.GetMappingKey(), admin);

            var token = BankService.Login(admin.Username, admin.Password);
            var account_number = BankService.NewAccount(token, "admin", "A new account");
            BankService.Bye(token);
            Assert.Multiple(()=>{
                Assert.IsTrue(AccountRepository.Entities.ContainsKey(account_number));
                var account = AccountRepository.Entities[account_number];
                Assert.AreEqual(0.0f,account.Value);
                Assert.AreEqual("admin", account.Owner);
                Assert.AreEqual("A new account", account.Description);

            });
        }

        [Test]
        public void Create_Accont_Throws_When_Username_Doesnt_Exist()
        {
            var admin = new User()
            {
                Username = "admin",
                Password = "1234",
                Privilege = Privileges.Admin,

            };

            UserRepository.Entities.TryAdd(admin.GetMappingKey(), admin);

            var token = BankService.Login(admin.Username, admin.Password);
            Assert.Throws<ArgumentException>(() =>
            {
                BankService.NewAccount(token, "asdadasdasdadasdasdasd", "A new account");
            });
        }

        [Test]
        public void Admin_Can_PayInto_Account()
        {
            var admin = new User()
            {
                Username = "admin",
                Password = "1234",
                Privilege = Privileges.Admin,

            };

            UserRepository.Entities.TryAdd(admin.GetMappingKey(), admin);

            var ac1 = new Account()
            {
                Owner = "a",
                Description = "AC1"
            };
            AccountRepository.Entities.TryAdd(ac1.GetMappingKey(), ac1);

            var token = BankService.Login(admin.Username, admin.Password);
            BankService.PayInto(token, ac1.AccountNumber,100.0f);
            Assert.AreEqual(100.0f, ac1.Value);
        }


        [Test]
        public void PayInto_Throws_When_Account_Doesnt_Exist()
        {
            var admin = new User()
            {
                Username = "admin",
                Password = "1234",
                Privilege = Privileges.Admin,

            };

            UserRepository.Entities.TryAdd(admin.GetMappingKey(), admin);

            Assert.Throws<ArgumentException>(() =>
            {
                var token = BankService.Login(admin.Username, admin.Password);
                BankService.PayInto(token, "asdasdasdasdasd", 100.0f);
            });
        }





        [Test]
        public void ListAccounts_Should_List_Accounts()
        {
            var user = new User()
            {
                Username = "a",
                Password = "1234",
                Privilege = Privileges.User,

            };
            UserRepository.Entities.TryAdd(user.GetMappingKey(), user);

            var ac1 = new Account()
            {
                Owner = "a",
                Description = "AC1"
            };

            var ac2 = new Account()
            {
                Owner = "a",
                Description = "AC2"
            };

            var ac3 = new Account()
            {
                Owner = "b",
                Description = "AC3"
            };

            AccountRepository.Entities.TryAdd(ac1.GetMappingKey(), ac1);
            AccountRepository.Entities.TryAdd(ac2.GetMappingKey(), ac2);
            AccountRepository.Entities.TryAdd(ac3.GetMappingKey(), ac3);

            var token = BankService.Login(user.Username, user.Password);
            var result = BankService.ListAccounts(token);

            Assert.Multiple(() =>
            {
                Assert.AreEqual(2, result.Count);
                Assert.Contains("AC2",result.Select(x=>x.Description).ToList());
                Assert.Contains("AC1", result.Select(x => x.Description).ToList());
            });
        }

        [Test]
        public void User_Can_Transfere_Money()
        {
            var user = new User()
            {
                Username = "a",
                Password = "1234",
                Privilege = Privileges.User,

            };
            UserRepository.Entities.TryAdd(user.GetMappingKey(), user);

            var sender = new Account()
            {
                Owner = "a",
                Description = "AC1"
            };

            var reciever = new Account()
            {
                Owner = "b",
                Description = "AC2"
            };

            AccountRepository.Entities.TryAdd(sender.GetMappingKey(), sender);
            AccountRepository.Entities.TryAdd(reciever.GetMappingKey(), reciever);

            var token = BankService.Login(user.Username, user.Password);
            BankService.Transfere(token, sender.AccountNumber, reciever.AccountNumber,100.0f,"Test Transaction");

            Assert.Multiple(() =>
            {
                Assert.AreEqual(-100.0f, sender.Value);
                Assert.AreEqual(100.0f, reciever.Value);

                Assert.AreEqual(100.0f, reciever.Transactions[0].Amount);
                Assert.AreEqual("Test Transaction", reciever.Transactions[0].Comment);
                Assert.AreEqual(TransactionStyle.Recieved, reciever.Transactions[0].Style);

                Assert.AreEqual(100.0f, sender.Transactions[0].Amount);
                Assert.AreEqual("Test Transaction", sender.Transactions[0].Comment);
                Assert.AreEqual(TransactionStyle.Send, sender.Transactions[0].Style);

                Assert.AreEqual(sender.Transactions[0].Date, reciever.Transactions[0].Date);
            }); 
        }

        [Test]
        public void Transfere_Throws_When_Sender_Account_Doesnt_Exist()
        {
            var user = new User()
            {
                Username = "a",
                Password = "1234",
                Privilege = Privileges.User,

            };
            UserRepository.Entities.TryAdd(user.GetMappingKey(), user);

            var a = new Account()
            {
                Owner = "a",
                Description = "AC1"
            };

            AccountRepository.Entities.TryAdd(a.GetMappingKey(), a);

            Assert.Throws<ArgumentException>(() =>
            {
                var token = BankService.Login(user.Username, user.Password);
                BankService.Transfere(token, "asdasdasdasdasd", a.AccountNumber, 100.0f, "Test Transaction");
            });
        }

        [Test]
        public void Transfere_Throws_When_Reciever_Account_Doesnt_Exist()
        {
            var user = new User()
            {
                Username = "a",
                Password = "1234",
                Privilege = Privileges.User,

            };
            UserRepository.Entities.TryAdd(user.GetMappingKey(), user);

            var a = new Account()
            {
                Owner = "a",
                Description = "AC1"
            };

            AccountRepository.Entities.TryAdd(a.GetMappingKey(), a);

            Assert.Throws<ArgumentException>(() =>
            {
                var token = BankService.Login(user.Username, user.Password);
                BankService.Transfere(token,a.AccountNumber, "asdasdasdasdasd", 100.0f, "Test Transaction");
            });
        }

        [Test]
        public void Transfere_Throws_When_User_Doesnt_Own_Sender_Account()
        {
            var user = new User()
            {
                Username = "a",
                Password = "1234",
                Privilege = Privileges.User,

            };
            UserRepository.Entities.TryAdd(user.GetMappingKey(), user);

            var a = new Account()
            {
                Owner = "b",
                Description = "AC1"
            };

            var b = new Account()
            {
                Owner = "b",
                Description = "AC1"
            };

            AccountRepository.Entities.TryAdd(a.GetMappingKey(), a);
            AccountRepository.Entities.TryAdd(b.GetMappingKey(), b);

            Assert.Throws<AuthenticationException>(() =>
            {
                var token = BankService.Login(user.Username, user.Password);
                BankService.Transfere(token, a.AccountNumber, b.AccountNumber, 100.0f, "Test Transaction");
            });
        }

        protected void CheckAccount(IAccount a, IAccount b)
        {
            Assert.AreEqual(a.Owner, b.Owner);
            Assert.AreEqual(a.AccountNumber, b.AccountNumber);
            Assert.AreEqual(a.Value, b.Value);
            Assert.AreEqual(a.Description, b.Description);

        }

        protected void CheckTransaction(ITransaction a, ITransaction b)
        {
            Assert.AreEqual(a.Style, b.Style);
            Assert.AreEqual(a.Amount, b.Amount);
            Assert.AreEqual(a.Date, b.Date);
            Assert.AreEqual(a.Comment, b.Comment);
        }



        [Test]
        public void User_Can_Create_Detailed_Statement()
        {
            var user = new User()
            {
                Username = "a",
                Password = "1234",
                Privilege = Privileges.User,
            };

            UserRepository.Entities.TryAdd(user.GetMappingKey(), user);

            var a = new Account()
            {
                Owner = "a",
                Description = "AC1",
                Transactions = new List<ITransaction>()
                {
                    new Transaction() { Comment = "T1"},
                    new Transaction() { Comment = "T2"}
                }
            };

            var b = new Account()
            {
                Owner = "a",
                Description = "AC2"
            };

            AccountRepository.Entities.TryAdd(a.GetMappingKey(), a);
            AccountRepository.Entities.TryAdd(b.GetMappingKey(), b);

            var token = BankService.Login(user.Username, user.Password);
            var result = BankService.Statement(token,detailed:true);
            Assert.Multiple(() =>
            {
                Assert.AreEqual(2, result.Count);

                var r_a = result.First(x=>x.Description == "AC1");
                var r_b = result.First(x => x.Description == "AC2");

                CheckAccount(a, r_a);
                for (int i = 0; i < a.Transactions.Count; i++)
                {
                    CheckTransaction(a.Transactions[i], r_a.Transactions[i]);
                }
                CheckAccount(b, r_b);
                for (int i = 0; i < b.Transactions.Count; i++)
                {
                    CheckTransaction(b.Transactions[i], r_b.Transactions[i]);
                }
            });
        }

        [Test]
        public void User_Can_Create_Statement()
        {
            var user = new User()
            {
                Username = "a",
                Password = "1234",
                Privilege = Privileges.User,
            };

            UserRepository.Entities.TryAdd(user.GetMappingKey(), user);

            var a = new Account()
            {
                Owner = "a",
                Description = "AC1"
            };

            var b = new Account()
            {
                Owner = "a",
                Description = "AC2"
            };

            AccountRepository.Entities.TryAdd(a.GetMappingKey(), a);
            AccountRepository.Entities.TryAdd(b.GetMappingKey(), b);

            var token = BankService.Login(user.Username, user.Password);
            var result = BankService.Statement(token, detailed: true);
            Assert.Multiple(() =>
            {
                Assert.AreEqual(2, result.Count);

                var r_a = result.First(x => x.Description == "AC1");
                var r_b = result.First(x => x.Description == "AC2");

                CheckAccount(a, r_a);
                CheckAccount(b, r_b);
            });
        }

        [Test]
        public void Statement_Should_Get_Correct_Account()
        {
            var user = new User()
            {
                Username = "a",
                Password = "1234",
                Privilege = Privileges.User,
            };

            UserRepository.Entities.TryAdd(user.GetMappingKey(), user);

            var a = new Account()
            {
                Owner = "a",
                Description = "AC1"
            };

            var b = new Account()
            {
                Owner = "a",
                Description = "AC2"
            };

            AccountRepository.Entities.TryAdd(a.GetMappingKey(), a);
            AccountRepository.Entities.TryAdd(b.GetMappingKey(), b);

            var token = BankService.Login(user.Username, user.Password);
            var result = BankService.Statement(token,account_number: b.AccountNumber, detailed: true);
            Assert.Multiple(() =>
            {
                Assert.AreEqual(1, result.Count);

                CheckAccount(b, result[0]);
               
            });
        }

        [Test]
        public void Statement_Should_Throw_When_Account_Doesnt_Exist()
        {
            var user = new User()
            {
                Username = "a",
                Password = "1234",
                Privilege = Privileges.User,
            };

            UserRepository.Entities.TryAdd(user.GetMappingKey(), user);

            Assert.Throws<ArgumentException>(() =>
            {
                var token = BankService.Login(user.Username, user.Password);
                var result = BankService.Statement(token, account_number: "asdasdasdasdas", detailed: true);
            });
        }

        [Test]
        public void Statement_Should_Throw_When_User_Doesnt_Own_Account()
        {
            var user = new User()
            {
                Username = "a",
                Password = "1234",
                Privilege = Privileges.User,
            };

            UserRepository.Entities.TryAdd(user.GetMappingKey(), user);

            var a = new Account()
            {
                Owner = "x",
                Description = "AC1"
            };

            AccountRepository.Entities.TryAdd(a.GetMappingKey(), a);

            Assert.Throws<AuthenticationException>(() =>
            {
                var token = BankService.Login(user.Username, user.Password);
                var result = BankService.Statement(token, account_number: a.AccountNumber, detailed: true);
            });
        }

    }
}
