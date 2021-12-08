#include "MyBankClientConsoleApp.h"

void client::run()
{
    IMyBankFunctions bank;
    _start(bank);
    cout << "Bank wird verbunden\n";
    _login(bank);
    _loop(bank);
}

void client::_start(IMyBankFunctions& MyBank) {
    IMyBankFunctions& bank = MyBank;
    string netaddr;
    string port;
    cout << "Willkommen bei MyBank!\n" << "Bitte geben sie die Serveradresse ein:\n";
    cin >> netaddr;
    cout << "Bitte geben sie den Netzwerkport an:\n";
    cin >> port;
    bank.connect_c(netaddr + ":" + port);
}

void client::_login(IMyBankFunctions& MyBank) {
    IMyBankFunctions& bank = MyBank;
    string username;
    string password;
    cout << "Geben sie ihren Benutzernamen ein:\n";
    cin >> username;
    cout << "Geben sie ihr Passwort ein:\n";
    cin >> password;
    cout << bank.login_c(username, password);
}
void client::_loop(IMyBankFunctions& MyBank){
    IMyBankFunctions& bank = MyBank;
    string func;
    bool active = true;
    while (active)
    {
        cout << "Wie kann ich ihnen helfen?\n";
        cin >> func;
        if (func == "newaccount")
        {
            string username;
            string description;
            cout << "Für wen möchten Sie einen neuen Account anlegen?\n";
            cin >> username;
            cout << "Geben Sie eine Beschreibung für den Account ein.\n";
            cin >> description;
            cout << bank.newaccount_c(username, description);
        }
        else if (func == "newuser")
        {
            string username;
            string password;
            cout << "Wie soll der neue Nutzer heißen?\n";
            cin >> username;
            cout << "Geben Sie ein Passwort für den neuen Nutzer ein\n";
            cin >> password;
            cout << bank.newuser_c(username,password);
        }
        else if (func == "listaccounts")
        {
            cout << "Hier sind alle ihre Konten:\n";
            cout << bank.listaccounts_c();
        }
        else if (func == "transfer")
        {
            long to_account_number;
            float amount;
            string comment;
            cout << "Auf welches Konto möchten Sie überweisen?\n";
            cin >> to_account_number;
            cout << "Wie viel möchten Sie überweisen?\n";
            cin >> amount;
            cout << bank.transfer_c((long)to_account_number,(float)amount,comment);
        }
        else if (func == "statement")
        {
            long account_number;
            long detailed;
            cout << "Wollen Sie ein bestimmtes Konto sehen oder alle(geben sie 0 ein)?";
            cin >> account_number;
            cout << "Wollen Sie eine detailierte Ansicht ihrer Konten?";
            cin >> detailed;
            cout << bank.statement_c(account_number,detailed);
        }
        else if (func == "bye")
        {
            cout << bank.bye_c();
            active = false;
        }
    } 
}



