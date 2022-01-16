#include "MyBankConsoleClient.h"
#include <stdio.h>

void MyBankConsoleClient::run(string username_from_cmd, string password_from_cmd)
{
    bool logged_in = false;
    bool exit = false;

    while (!exit)
    {
        while (!logged_in)
        {
            logged_in = _login(username_from_cmd, password_from_cmd);
        }
        exit = _loop(&logged_in);
    }
}

bool MyBankConsoleClient::start(string netwAddr_from_cmd, string endpoint_from_cmd) {
    string in;
    string delimiter = ":";
    size_t pos = 0;

    string netwAddr = netwAddr_from_cmd;
    string endpoint = endpoint_from_cmd;
    long connected = 0;
    if (netwAddr == "" || (endpoint == "" && port_need))
    {
        cout << "Welcomme at MyBank!\n" << "Please enter the serveraddress:\n";
        getline(cin, in);
        if ((pos = in.find(delimiter)) != string::npos)
        {
            netwAddr = in.substr(0, pos);
            in.erase(0, pos + delimiter.length());
            endpoint = in;
        }
        else
        {
            netwAddr = in;
            if(port_need)
            {
                cout << "Please enter the port:\n";
                getline(cin, endpoint);
            }
        }
    }
    cout << "Bank will be connected\n";
    cout << bank.connect_c(netwAddr, endpoint, connected);
    return connected;
}

bool MyBankConsoleClient::_login(string username_from_cmd, string password_from_cmd) {
    bool logged_in = false;
    string in;
    string delimiter = " ";
    size_t pos = 0;

    string username = username_from_cmd;
    string password = password_from_cmd;

    if (username == "" || password == "")
    {
        cout << "Please log in:\n";
        getline(cin, in);
        if ((pos = in.find(delimiter)) != string::npos)
        {
            if (in.substr(0, pos) == "user")
            {
                in.erase(0, pos + delimiter.length());
                if ((pos = in.find(delimiter)) != string::npos)
                {
                    username = in.substr(0, pos);
                    in.erase(0, pos + delimiter.length());
                    password = in;
                }
                else
                {
                    cout << "unknown function";
                    return false;
                }
            }
            else
            {
                cout << "unknown function";
                return false;
            }
        }
        else
        {
            username = in;
            cout << "Enter your password:\n";
            getline(cin, password);
        }
    }
    cout << bank.login_c(username, password, logged_in);
    return logged_in;
}
void MyBankConsoleClient::_parseSingleCommand(string func, bool* exit, bool* active, bool* logged_in)
{
    if (func == "newaccount")
    {
        string username;
        string description;
        cout << "Which user should get a new account?\n";
        getline(cin, username);
        cout << "Enter a description for the new account.\n";
        getline(cin, description);
        cout << bank.newaccount_c(username, description);
    }
    else if (func == "newuser")
    {
        string username;
        string password;
        cout << "Enter the name of the new user.\n";
        getline(cin, username);
        cout << "Enter a password for the new user.\n";
        getline(cin, password);
        cout << bank.newuser_c(username, password);
    }
    else if (func == "listaccounts")
    {
        cout << "Here are all your accounts:\n";
        cout << bank.listaccounts_c();
    }
    else if (func == "transfer")
    {
        string to_account_number;
        string from_account_number;
        string amount;
        string comment;
        cout << "Which account to send from?\n";
        getline(cin, to_account_number);
        cout << "Who is the recipient?\n";
        getline(cin, from_account_number);
        cout << "How much to transfer?\n";
        getline(cin, amount);
        cout << "Enter a comment\n";
        getline(cin, comment);
        cout << bank.transfer_c(stod(from_account_number), stod(to_account_number), stof(amount), comment);
    }
    else if (func == "statement")
    {
        string detailed = string("0");
        string account_number = string("-1");
        cout << bank.statement_c(stod(account_number), stod(detailed));
    }
    else if (func == "payinto")
    {
        string account_number;
        string amount;
        cout << "Payinto which account?\n";
        getline(cin, account_number);
        cout << "How much?\n";
        getline(cin, amount);
        cout << bank.payinto_c(stod(account_number), stof(amount));
    }
    else if (func == "logout")
    {
        cout << bank.bye_c();
        *logged_in = false;
        *active = false;
    }
    else if (func == "bye")
    {
        cout << bank.bye_c();
        *logged_in = false;
        *active = false;
        *exit = true;
    }
    else if (func == "help") {
        cout << "available functions:" << "\n";
        cout    << "newaccount <username> \"<description>\" (admin only)\n" 
                << "newuser <Benutzername> <Passwort> (admin only)\n" 
                << "payinto <Kontonummer> <Geldbetrag>  (admin only)\n" 
                << "listaccounts\n" 
                << "transfer <KtnrVon> <KtnrEmpf> <NameEmpf> <Betrag> \"<Text>\"\n"
                << "statement [-detail] [<Kontonummer>]\n" 
                << "logout\n" 
                << "bye\n";
    }
    else {
        cout << "unknown command: " << func << "'\n";
    }
}
void MyBankConsoleClient::_parseFullCommadn(string func, bool* exit, bool* active, bool* logged_in)
{
    size_t pos = 0;
    string substr;
    string delimiter_space = " ";
    string delimiter_stringescape = "\"";
    string delimiter_dash = "-";
    if ((pos = func.find(delimiter_space)) != string::npos)
    {
        substr = func.substr(0, pos);
        func.erase(0, pos + delimiter_space.length());
        if (substr == "newaccount")
        {
            string username;
            string description;
            if ((pos = func.find(delimiter_space)) != string::npos)
            {
                username = func.substr(0, pos);
                func.erase(0, pos + delimiter_space.length());
                if ((pos = func.find(delimiter_stringescape)) != string::npos)
                {
                    func.erase(0, pos + delimiter_stringescape.length());
                    if ((pos = func.find(delimiter_stringescape)) != string::npos)
                    {
                        description = func.substr(0, pos);
                        func.erase(0, pos + delimiter_stringescape.length());
                        if (func == "")
                        {
                            cout << bank.newaccount_c(username, description);
                            return;
                        }
                    }
                }
            }
            cout << "wrong parameter for newaccount try using 'help'";
        }
        else if (substr == "newuser")
        {
            string username;
            string password;
            if ((pos = func.find(delimiter_space)) != string::npos)
            {
                username = func.substr(0, pos);
                func.erase(0, pos + delimiter_space.length());
                if ((pos = func.find(delimiter_space)) != string::npos)
                {
                    password = func.substr(0, pos);
                    func.erase(0, pos + delimiter_space.length());
                    if (func == "")
                    {
                        cout << bank.newuser_c(username, password);
                        return;
                    }
                }
            }
            cout << "wrong parameter for newuser try using 'help'";
        }
        else if (substr == "transfer")
        {
            string to_account_number;
            string from_account_number;
            string amount;
            string comment;
            if ((pos = func.find(delimiter_space)) != string::npos)
            {
                to_account_number = func.substr(0, pos);
                func.erase(0, pos + delimiter_space.length());
                if ((pos = func.find(delimiter_space)) != string::npos)
                {
                    from_account_number = func.substr(0, pos);
                    func.erase(0, pos + delimiter_space.length());
                    if ((pos = func.find(delimiter_space)) != string::npos)
                    {
                        amount = func.substr(0, pos);
                        func.erase(0, pos + delimiter_space.length());
                        if ((pos = func.find(delimiter_stringescape)) != string::npos)
                        {
                            func.erase(0, pos + delimiter_stringescape.length());
                            if ((pos = func.find(delimiter_stringescape)) != string::npos)
                            {
                                comment = func.substr(0, pos);
                                func.erase(0, pos + delimiter_stringescape.length());
                                if (func == "")
                                cout << bank.transfer_c(stod(from_account_number), stod(to_account_number), stof(amount), comment);
                                return;
                            }
                        }
                    }
                }
            }
            cout << "wrong parameter for transfer try using 'help'";
        }
        else if (substr == "statement")
        {
            string detailed = string("0");
            string account_number = string("-1");
            if ((pos = func.find(delimiter_dash)) != string::npos)
            {
                if(func.substr(0, pos) == "detail")
                {
                    detailed = string("1");
                    func.erase(0, pos + delimiter_space.length());
                }                
            }
            if ((pos = func.find(delimiter_space)) != string::npos)
            {
                account_number = func.substr(0, pos);
                func.erase(0, pos + delimiter_space.length());
            }
            if (func == "")
            {
                cout << bank.statement_c(stod(account_number), stod(detailed));
                return;
            }
            cout << "wrong parameter for statement try using 'help'";
        }
        else if (substr == "payinto")
        {
            string account_number;
            string amount;
            if ((pos = func.find(delimiter_space)) != string::npos)
            {
                account_number = func.substr(0, pos);
                func.erase(0, pos + delimiter_space.length());
                if ((pos = func.find(delimiter_space)) != string::npos)
                {
                    amount = func.substr(0, pos);
                    func.erase(0, pos + delimiter_space.length());
                    if (func == "")
                    {
                        cout << bank.payinto_c(stod(account_number), stof(amount));
                        return;
                    }
                }
            }
            cout << "wrong parameter for newuser try using 'help'";
        }
        else {
            cout << "unknown function" << func << "'\n";
        }
    }
    else
    {
        cout << "unknown error while entering the command\n";
    }

    
}
bool MyBankConsoleClient::_loop(bool* logged_in) {
    string func;
    bool exit = false;
    bool active = true;
    while (active)
    {
        cout << "How can I help?\n";
        getline(cin, func);
        
        if (func.find(" ") == string::npos)
            _parseSingleCommand(func, &exit, &active, logged_in);
        else
            _parseFullCommadn(func, &exit, &active, logged_in);
    }
    return exit;
}



