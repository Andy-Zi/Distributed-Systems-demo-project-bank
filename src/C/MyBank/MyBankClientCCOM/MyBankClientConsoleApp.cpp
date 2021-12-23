#include "MyBankClientConsoleApp.h"

void run(IMyBankFunctions& MyBank)
{
    IMyBankFunctions& bank = MyBank;
    _login(bank);
    if (bank.isLoggedIn) {
        _loop(bank);
    }
}

void start(IMyBankFunctions& MyBank) {
    IMyBankFunctions& bank = MyBank;
    string netwAddr;
    cout << "Willkommen bei MyBank!\n" << "Bitte geben sie die Serveradresse ein:\n";
    cin >> netwAddr;
    cout << "Bank wird verbunden...\n";
    cout << bank.connect_c(netwAddr);
}

void _login(IMyBankFunctions& MyBank) {
    IMyBankFunctions& bank = MyBank;
    string username;
    string password;
    cout << "Geben sie ihren Benutzernamen ein:\n";
    cin >> username;
    cout << "Geben sie ihr Passwort ein:\n";
    cin >> password;
    cout << bank.login_c(username, password);
}
void _loop(IMyBankFunctions& MyBank){
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
            cout << "F�r wen m�chten Sie einen neuen Account anlegen?\n";
            cin >> username;
            cout << "Geben Sie eine Beschreibung f�r den Account ein.\n";
            cin >> description;
            cout << bank.newaccount_c(username, description);
        }
        else if (func == "newuser")
        {
            string username;
            string password;
            cout << "Wie soll der neue Nutzer hei�en?\n";
            cin >> username;
            cout << "Geben Sie ein Passwort f�r den neuen Nutzer ein\n";
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
            long from_account_number;
            float amount;
            string comment;
            cout << "Auf welches Konto m�chten Sie �berweisen?\n";
            cin >> to_account_number;
            cout << "Von welchem konto m�chten Sie �berweisen?\n";
            cin >> from_account_number;
            cout << "Wie viel m�chten Sie �berweisen?\n";
            cin >> amount;
            cout << bank.transfer_c(from_account_number,to_account_number,amount,comment);
        }
        else if (func == "statement")
        {
            long account_number;
            long detailed;
            cout << "Wollen Sie ein bestimmtes Konto sehen oder alle?(-1 f�r alle)\n";
            cin >> account_number;
            cout << "Wollen Sie eine detailierte Ansicht ihrer Konten? (1 = detailiert / 0 = normal)\n";
            cin >> detailed;
            cout << bank.statement_c(account_number,detailed);
        }
        else if (func == "bye")
        {
            cout << bank.bye_c();
            active = false;
        }
        else {
            cout << "Unbekanntes Command '" << func << "'\n";
        }
    } 
}



