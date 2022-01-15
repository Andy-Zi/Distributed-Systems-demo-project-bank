#pragma once
#include <nlohmann/json.hpp>
#include <string>
#include "Statement.h"

template<typename T>
json SerilaizeList(list<T> list);

template<typename T>
list<T> DeserilaizeList(json jsonArray);