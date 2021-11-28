# Api Dokumentation

## a) login(in username, in password, out token)
<br>
Username: The Username of the User [string]
<br>
Password: The Password of the User [string]
<br>
Token: The Token of the User [Hex-String]
(Last Digit represents the User-Type)
0: User
1: Admin
<br>


## b 1) (Admin only) newaccount(in token, in username, in description, out account_number) 
<br>
Token: The Token of the User making the request[Hex-String]
<br>
Username: The Name of the User to Create a new Account for [string]
<br>
Description: The Description of the Account [string]
<br>
Account_Number: The Account Number of the new Account [string (GUID)]
<br>

## b 2) (Admin only) newuser(in token, in username, in password) 
<br>
Token: The Token of the User making the request[Hex-String]
<br>
Username: The Name of the User to Create [string]
<br>
Password: The Password of the User to Create [string]
<br>

## c) listaccounts(in token, out (Account_Number,Description))
<br>
Token: The Token of the User making the request[Hex-String]
<br>
Account_Number,Description: A List of all Account_Numbers and Desciptions accessible by this User [(string,string)[]]
<br>
## d) (Admin only) payinto(in token, in account_number, in amount)
<br>
Token: The Token of the User making the request[Hex-String]
<br>
Account_Number: The Account Number of the Account to pay into [string (GUID)]
<br>
Amount: The Amount to pay into the Account [float]
<br>

## e) transfer(in token, in from_account_number, in to_account_number, in amount, in comment)
<br>
Token: The Token of the User making the request[Hex-String]
<br>
From_Account_Number: The Account Number of the Account to pay from [string (GUID)]
<br>
To_Account_Number: The Account Number of the Account to pay into [string (GUID)]
<br>
Amount: The Amount to pay into the Account [float]
<br>
Comment: The Comment to add to the Transaction [string]
<br>

## f) statement (in token, in [optional] account_number,in [optional] detailed , out accounts)
<br>
Token: The Token of the User making the request[Hex-String]
<br>
Account_Number: The Account Number of the Account to get the Statement for [string (GUID)]
<br>
Detailed: If set to true, the Statement will include all Transactions, otherwise none [boolean]
<br>
Accounts: A List of all Accounts or the given Account accessible by this User [Account-Object[]]
<br>

## g) bye(in token)
<br>
Token: The Token of the User making the request[Hex-String]

