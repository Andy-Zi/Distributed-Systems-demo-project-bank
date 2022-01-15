#pragma once
#include <nlohmann/json.hpp>
#include "Statement.h"
#include "Accountdesc.h"
#include <string>
#include <iostream>
#include <ctime>
#include "JsonUtilityFunctions.cpp"

list<Accountdesc> DeserializeAccountDescriptions(string jsonString);
list<Statement> DeserializeStatements(string jsonString);
void PrettyPrintAccountDescriptions(list<Accountdesc> accounts);
void PrettyPrintStatements(list<Statement> statements);