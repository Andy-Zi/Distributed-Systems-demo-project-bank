#include "MyBank.h"

int MyBank::Login(string username, string password)
{
    for(auto it = this->LoggedinUsers.begin(); it != this->LoggedinUsers.end(); it++)
    {
        if ((*it)->getName() == username)
        {
            throw std::invalid_argument("already logged in");
        }
    }
    for (auto it = this->KnownUsers.begin(); it != this->KnownUsers.end(); it++)
    {
        if ((*it).getName() == username)
        {
            if ((*it).checkPassword(password))
            {
                //Generate Token > 0 with appended Privilege
                int token = LoggedinUsers.size()+1;
                auto privilege = (*it).getPriviliges();
                token = (token * 10) + (int)privilege;

                (*it).login(token);
                LoggedinUsers.push_back(&(*it));
                return token;
            }
            throw std::invalid_argument("Wrong password");
        }     
    }
    throw std::invalid_argument("unknown User");
    return -1;
}

void MyBank::Logout(int token)
{
    for (auto it = this->LoggedinUsers.begin(); it != this->LoggedinUsers.end(); it++)
    {
        if ((**it).getToken() == token)
        {
            (**it).logout();
            LoggedinUsers.erase(it);
            break;
        }
    }
}

void MyBank::NewUser(string username, string password, Priviliges privilige)
{
    for (auto it = this->KnownUsers.begin(); it != this->KnownUsers.end(); it++)
    {
        if ((*it).getName() == username)
        {
            throw std::invalid_argument("Username already in use");
        }
    }
    this->KnownUsers.push_back(User(privilige, username, password, this->KnownUsers.size()));
}

int MyBank::NewAccount(int ownerID, string description)
{
    int accnr = this->Accounts.size();
    this->Accounts.push_back(Account(ownerID,description, accnr));
    return accnr;
}

list<Account> MyBank::ListAccounts(int Token)
{
    Priviliges priv = getPriviligesbyToken(Token);
    list<Account> out = {};
    switch (priv)
    {
    case Priviliges::user:
        for (auto it = this->Accounts.begin(); it != this->Accounts.end(); it++)
        {
            if ((*it).getOwnerID() == (*getUserByToken(Token)).getId())
            {
                out.push_back(*it);
            }
        }
        break;
    case Priviliges::admin:
        out = Accounts;
        break;
    default:
        break;
    }
    return out;
}

int MyBank::PayInto(int Accountnumber, float amount)
{
    return this->Transfer(-1, Accountnumber, amount, "Bareinzahlung");
}

int MyBank::Transfer(int from_accountnumber, int to_accountnumber, float amount, string comment)
{
    int transid = this->Transactions.size();
    Transaction t(from_accountnumber, to_accountnumber, amount, comment, transid);
    for (auto it = this->Accounts.begin(); it != this->Accounts.end(); it++)
    {
        try
        {
            if ((*it).getAccountnumber() == from_accountnumber && from_accountnumber != -1)
            {
                (*it).addTransaction(t);
            }
            if ((*it).getAccountnumber() == to_accountnumber)
            {
                (*it).addTransaction(t);
            }
        }
        catch (const std::exception&)
        {
            throw std::invalid_argument("not enough funds");
        }   
    }
    Transactions.push_back(t);
    return transid;
}

Priviliges MyBank::getPriviligesbyToken(int Token)
{
    User usr = *getUserByToken(Token);
    return usr.getPriviliges();
    
}

Priviliges MyBank::getPriviligesbyID(int ID)
{
    User usr = *getUserByID(ID);
    return usr.getPriviliges();
}

Priviliges MyBank::getPriviligesbyUsername(string username)
{
    User usr = *getUserByName(username);
    return usr.getPriviliges();
}

User* MyBank::getUserByToken(int Token)
{
    for (auto it = this->LoggedinUsers.begin(); it != this->LoggedinUsers.end(); it++)
    {
        if ((**it).getToken() == Token)
        {
            return &(**it);
        }
    }
    throw std::invalid_argument("unknown Token");
    return nullptr;
}

User* MyBank::getUserByID(int ID)
{
    for (auto it = this->LoggedinUsers.begin(); it != this->LoggedinUsers.end(); it++)
    {
        if ((**it).getId() == ID)
        {
            return &(**it);
        }
    }
    throw std::invalid_argument("unknown ID");
    return nullptr;
}

User* MyBank::getUserByName(string username)
{
    for (auto it = this->KnownUsers.begin(); it != this->KnownUsers.end(); it++)
    {
        if ((*it).getName() == username)
        {
            return &(*it);
        }
    }
    throw std::invalid_argument("unknown User");
    return nullptr;
}

Transaction MyBank::getTransactionbyID(int transac_id)
{
    for (auto it = this->Transactions.begin(); it != this->Transactions.end(); it++)
    {
        if ((*it).getId() == transac_id)
        {
            return *it;
        }
    }
    throw std::invalid_argument("unknown transac_id");
}

list<Transaction> MyBank::getTransactionsForAccount(int acc_id)
{
    list<Transaction> out = {};
    for (auto it = this->Transactions.begin(); it != this->Transactions.end(); it++)
    {
        if ((*it).getEndaccID() == acc_id || (*it).getStartaccID() == acc_id)
        {
            out.push_back(*it);
        }
    }
    return out;
}

Account MyBank::getAccountByID(int accountID)
{
    for (auto it = this->Accounts.begin(); it != this->Accounts.end(); it++)
    {
        if ((*it).getAccountnumber() == accountID)
        {
            return (*it);
        }
    }
    throw std::invalid_argument("Account not found");
}

Statement MyBank::generate_Statement(int accountID)
{
    Account acc = getAccountByID(accountID);
    list<Transaction> trans;
    list<int> transid = acc.listTransactions();
    for (auto it_transID = transid.begin(); it_transID != transid.end(); it_transID++)
    {
        for (auto it_Transactions = this->Transactions.begin(); it_Transactions != this->Transactions.end(); it_Transactions++)
        {
            if (*it_transID == (*it_Transactions).getId())
            {
                trans.push_back(*it_Transactions);
            }
        }
    } 
    return Statement(acc,trans);
}

list<User> MyBank::getKnownUsers()
{
    return this->KnownUsers;
}

list<User*> MyBank::getLoggedinUsers()
{
    return LoggedinUsers;
}
